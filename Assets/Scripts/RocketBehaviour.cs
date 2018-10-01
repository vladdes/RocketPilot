using Assets.Scripts.Globals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {
    
    Rigidbody rigidbody;
    AudioSource boosterSrc;
    [SerializeField] float rotationFactor;
    [SerializeField] float thrustFactor;
    // Use this for initialization
    void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
        boosterSrc = this.gameObject.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustFactor);
            if (!boosterSrc.isPlaying)
            {
                boosterSrc.Play();
            }

        }
        else
        {
            boosterSrc.Stop();
        }
       
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;
        var rotationSpeed = rotationFactor * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
           
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidbody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        switch (collision.gameObject.tag)
        {
            case Constants.friendly:
                print("friendly");
                break;
            default:
                print("dead");
                break;
        }
    }

}
