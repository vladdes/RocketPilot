using Assets.Scripts.Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        string scene = SceneManager.GetActiveScene().name;
        var newScene = EnumHandler.ConverToEnum<Scenes>(scene) + 1;
        switch (collision.gameObject.tag)
        {
            case Constants.friendly:
                print("friendly");
                break;
            case Constants.landingPad:
                SceneManager.LoadScene(EnumHandler.ConvertToString(newScene));
                break;
            default:
                SceneManager.LoadScene(EnumHandler.ConvertToString(Scenes.Scene1));
                break;
        }
    }

}
