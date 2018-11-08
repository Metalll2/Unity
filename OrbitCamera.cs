using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;

    public float speed = 1.0f;
    private float _rotX;
    private float _rotY;
    private Vector3 _offset;

    public float minvert = - 30.0f;
    public float maxvert = 30.0f;
	// Use this for initialization
	void Start () {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput!=0)
        {
            _rotY += horInput * speed;
            
        }
        else
        {
            _rotY+= Input.GetAxis("Mouse X") * speed;
            _rotX -= Input.GetAxis("Mouse Y") * speed;
            _rotX = Mathf.Clamp(_rotX, minvert, maxvert);
        }
        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
	}
}
