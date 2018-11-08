using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createobject : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
	
	void Start () {
		
	}
	
	
	void Update () {
        _enemy = Instantiate(enemyPrefab) as GameObject;
        _enemy.transform.position = new Vector3(2, 4, 2);
        float angle = Random.Range(0, 360);
        _enemy.transform.Rotate(angle, angle, angle);
        
	}

}
