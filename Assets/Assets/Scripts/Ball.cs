using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//  TODO:
//  - Delay not working
public class Ball : MonoBehaviour
{
    // PUBLIC VARIABLES
    public float speed = 7f;    // Movement speed of the ball
    public float bounceSpeedMultiplier = 1.05f; // The ball's velocity will be multiplied with this everytime it bounces
    public float launchDelay = 1.5f; // Seconds before the ball gets launched on the start of every round
    public Vector3 defPosition = new Vector3(0f,0f,0f); // The set position where the ball is placed before launch at the start of the round

    // GAME OBJECT
    public GameObject ghost; // This object will be instantiated multiple times to produce a ghosting trail like effect 

    // PRIVATE VARIABLES
    private Vector2 prevVelocity; // Used for determining if velocity changed

    // COMPONENTS
    [HideInInspector] Rigidbody2D rb;
    [HideInInspector] new Light2D light;
    [HideInInspector] AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        // COMPONENTS
        rb = GetComponent<Rigidbody2D>();
        light = GetComponentInChildren<Light2D>();
        sound = GetComponent<AudioSource>();

        RoundStart();
    }
    
    void FixedUpdate()
    {
        // Runs when the ball bounces, which is determined when the velocity has changed
        if (rb.velocity != prevVelocity)
        {
            light.intensity = 1; // Just some effects and flair
            rb.velocity = new Vector2(rb.velocity.x * bounceSpeedMultiplier, rb.velocity.y * bounceSpeedMultiplier); // The velocity gets multiplied
            sound.Play(); // Bounce sound effect
        }  

        // Making the light intensity decay
        if (light.intensity > 0)
        {
            light.intensity -= 0.05f;
        }

        prevVelocity = rb.velocity; // Storing the current velocity

        Instantiate(ghost, transform.position, transform.rotation); // Instantiating the ghost game object at the ball's position and default rotation
        
    }

    // Setup code that should be run for whenever a new round starts
    public void RoundStart()
    {
        light.intensity = 1;
        transform.position = defPosition;
        Invoke("Launch", launchDelay);
    }

    // Launching the ball
    private void Launch()
    {
        // Determining between 4 random directions for the ball to launch with
        float x = Random.value > 0.5f ? 1 : -1;
        float y = Random.value > 0.5f ? 1 : -1;

        rb.velocity = new Vector2(speed * x,speed * y); // The actual launching
    }

}
