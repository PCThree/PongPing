using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Ghost : MonoBehaviour
{
    // COMPONENTS
    SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        // COMPONENTS
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Makes the ball turn red and lower its opacity gradually to emulate decay
        sr.color = new Vector4( sr.color.r , sr.color.b - 0.02f , sr.color.g - 0.04f , sr.color.a - 0.04f); // Don't ask me how i got these numbers, i just eyeballed them man
        
        // Runs once the ghost object is fully transparent (alpha < 0)
        if (sr.color.a < 0)
        {
            Destroy(gameObject); // We don't need no invisible objects just laying about, taking up our memory! DESTROY!
            Debug.Log("Object Destroyed"); // Just making sure tee hee
        }
    }
    
}
