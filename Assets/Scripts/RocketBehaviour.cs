using System;
using System.Reflection;
using Assets.Scripts.Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketBehaviour : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource boosterSrc;
    [SerializeField] float rotationFactor;
    [SerializeField] float thrustFactor;
    State state;


    // Use this for initialization
    void Start()
    {
        state = State.Alive;
        rigidbody = this.GetComponent<Rigidbody>();
        boosterSrc = this.gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        else
        {
            if (boosterSrc.isPlaying)
            {
                boosterSrc.Stop();
            }
        }

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

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidbody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case Constants.friendly:
                print("friendly");
                break;
            case Constants.landingPad:
                state = State.Transcending;
                Invoke("LoadNextScene", 1);
                break;
            default:
                state = State.Dying;
                Invoke("Dead", 1);
                break;
        }
    }

    private void Dead()
    {
        SceneManager.LoadScene(EnumHandler.ConvertToString(Scenes.Scene1));
    }

    private void LoadNextScene()
    {
        var newScene = (EnumHandler.ConverToEnum<Scenes>(SceneManager.GetActiveScene().name) + 1);
        SceneManager.LoadScene(EnumHandler.ConvertToString(newScene));


    }
}
