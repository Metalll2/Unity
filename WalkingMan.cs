using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMan : MonoBehaviour {

    [SerializeField] private Transform target;
    private CharacterController _characterController;
    public float speed = 5.0f;
    public float rotspeed = 10.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelosity = -10.0f;
    public float minFall = -1.5f;
    public float _vertSpeed;
    public float runspeed = 15.0f;
    public float pushForse = 5.0f;
    private ControllerColliderHit _contact;
	
    // Use this for initialization
	void Start () {
        _characterController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moving = Vector3.zero; //начинаем с вектора(0.0.0), непрерывно добавляя компненты движения;
        var forward = Input.GetAxis("Horizontal");
        var left = Input.GetAxis("Vertical"); 
        if (forward != 0 || left != 0)
        {
            moving.x = forward * speed; // движение по оси X;
            moving.z = left * speed;// движение по оси Z;
            moving = Vector3.ClampMagnitude(moving, speed);// ограничиваем движение по диагонали;

            Quaternion tmp = target.rotation;// сохраняем начальную ориентацию;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);//преобразование поворота, чтобы оно совершалось относительно оси Y;
            moving = target.TransformDirection(moving);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(moving);// вычисляем квартенион, смотрящий в этом направлении;
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotspeed * Time.deltaTime);// плавный поворот из текущего положения в целевое;
        }
        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall*3;
            }
        }
        else
        {
            _vertSpeed += gravity *10* Time.deltaTime;
            if (_vertSpeed < terminalVelosity)
            {
                _vertSpeed = terminalVelosity;
            }
        }
        moving.y = _vertSpeed;
        moving *= Time.deltaTime;
        
        _characterController.Move(moving);
        


    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForse;
            Debug.Log("pushForse");
            
        }


    }
}
