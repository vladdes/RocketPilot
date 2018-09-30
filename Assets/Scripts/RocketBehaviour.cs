using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {
    
    public Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        processInput();
	}

    private void processInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
            
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * 20 * Time.deltaTime);
           
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * 20 * Time.deltaTime);
        }
    }
}
