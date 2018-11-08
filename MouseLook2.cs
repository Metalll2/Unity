using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook2 : MonoBehaviour {
    //вид от третьего лица
    [SerializeField] private Transform target;
    private CharacterController _characterController;
    public float sensivX = 1.0f;
    public float sensivY = 1.0f;
    public bool invertY = false;
    public LayerMask obstacles;
    public float minY = -45.0f;
    public float maxY = 45.0f;
    private float hor;
    private float vert;
    private float _maxDistance; //то же _distance;
    private Vector3 _localPosition;
    private Vector3 _offset;// то же _postition, через set, get; 
   


    void Start () {
        
        _characterController = GetComponent<CharacterController>();
        _offset = target.position - transform.position;// сохраняем начальное смещение между камерой и целью;
        vert = transform.eulerAngles.y; // сохраняем угол поворота в горизонтальной плоскости;keep the angle of rotation in the horizontal plane;
        _maxDistance = Vector3.Distance(transform.position, target.position);
    }
	
	// Update is called once per frame
	void LateUpdate () {

        vert  += Input.GetAxis("Mouse X") * sensivX;
        if (invertY == true)
        {
            hor += Input.GetAxis("Mouse Y") * sensivY;
        }
        else
        {
            hor -= Input.GetAxis("Mouse Y") * sensivY;
        }
       
        hor = Mathf.Clamp(hor, minY, maxY);// фиксируем угол по вертикали;  fix the angle vertically;
       
        Quaternion rotation = Quaternion.Euler(hor, vert, 0); //угол поворота преобразован в квартенион;
        transform.position = target.position - (rotation * _offset);// поддерживаем начальное смещение, сдвигаемое в соответствии с поворотом камеры;
        reactObstacle();
        transform.LookAt(target);
        


    }
    void reactObstacle() // реакция камеры на препятствия;
    { 
        var _distance = Vector3.Distance(transform.position, target.position);
        RaycastHit hit;
        
        if(Physics.Raycast(target.position, transform.position-target.position, out hit,_maxDistance, obstacles))
        {
            transform.position = hit.point;
            Debug.Log(" postion, offset " + transform.position + " , " + _offset + " distance "+ _distance  +", maxDistance " + _maxDistance + " target and transform " + target.position + " and " + transform.position);
        }
        else if (_distance < _maxDistance && !Physics.Raycast(transform.position, -transform.forward, .5f, obstacles))
        {
           
            Debug.Log(" postion else, offset " + transform.position + " , "+_offset + " distance else " + _distance +", maxDistance " + _maxDistance + " target and transform  else " + target.position + " and " + transform.position);
        }
    }
    
}
