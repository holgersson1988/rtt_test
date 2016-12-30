using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    float rotateSpeed = 5;
	// Use this for initialization
	void Start () {
        rotateSpeed = 50;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);
	}
}
