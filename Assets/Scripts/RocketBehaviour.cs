using System;
using System.Reflection;
using Assets.Scripts.Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketBehaviour : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rotationFactor;
    [SerializeField] float thrustFactor;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip winning;
    [SerializeField] AudioClip dying;
    [SerializeField] ParticleSystem particleDying;
    [SerializeField] ParticleSystem particleWinning;
    State state;


   
    void Start()
    {
        state = State.Alive;
        rigidbody = this.GetComponent<Rigidbody>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        

    }

  
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Applythrust();

        }
        else
        {
            audioSource.Stop();
        }

    }

    private void Applythrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustFactor);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
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
                Won();
                break;
            default:
                Dying();
                break;
        }
    }

    private void Dying()
    {
        particleDying.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(dying);
        state = State.Dying;
        Invoke("Dead", 1);
    }

    private void Won()
    {
        particleWinning.Play();
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(winning);
        Invoke("LoadNextScene", 1);
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
