using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(_enemydie());
	}

    private IEnumerator _enemydie()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
