using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour {
    [SerializeField] private Transform target;
    public float rotspeed = 9.0f;
    public float movespeed = 15.0f;

    private CharacterController _characterController;
	// Use this for initialization
	void Start () {
        _characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * movespeed;
            movement.z = vertInput * movespeed;
            movement = Vector3.ClampMagnitude(movement, movespeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotspeed * Time.deltaTime);
        }
        movement *= Time.deltaTime;
        _characterController.Move(movement);

    }
}
