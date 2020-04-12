using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PUBLIC VARIABLES
    public bool isPlayer1 = false;
    public bool isAI = false;

    // GAME OBJECT
    public Transform followObject; // You should probably set this to the ball, but i mean... im not gonna stop you if you set it to follow yourself for mirror movement, its not like i've stopped you any other time you've wanted to play with yourself
    public Color masterColor; // This color will overwrite the color of all the children game components of the player object. Not gonna lie, should've probably just made a sprite instead of grouping multiple game objects
    public float moveSpeed = 10f; // multiplier for the movement speed
    
    // PRIVATE VARIABLES
    Vector3 defPosition;

    // COMPONENTS
    [HideInInspector] Rigidbody2D rb; 

    void Start()
    {
        // COMPONENTS
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();

        // SETUP
        defPosition = isPlayer1 ? new Vector3(-9,0,0) : new Vector3(9,0,0);

        // Changes all the colors of the childern objects into the selected color
        foreach (SpriteRenderer rend in childrenSprites)
        {
            rend.color = masterColor;
        }
        if (masterColor == null) // just patching the possibility of no color having been picked
        {
            masterColor = isPlayer1 ? new Color(0,185,255,255) : new Color(255, 45, 85, 255);
        }

        RoundStart();
    }

    void FixedUpdate()
    {
        // Movement codes
        if (isAI) // Movement code if the player object is AI
        {
            Vector3 displacement = followObject.position - transform.position; // Measures the difference between the distance of the ball and the AI player, only the Y value is needed though

            // The AI moves based on how far it is from the ball, vertically wise
            // Added some padding to the displacement so the AI will somewhat chase where the ball is going and not just where it is
            if (displacement.y * 1.5 > 0) 
            {
                rb.velocity = new Vector2(0, moveSpeed);
            } else if (displacement.y * 1.5 < -0.5) {
                rb.velocity = new Vector2(0, -moveSpeed);
            } else { // A neutral gap, this is soley just for lessening the jankyness of the chase movement
                rb.velocity = new Vector2(0,0);
            }
        } else { // Regular player movements
            rb.velocity = new Vector2(0, Input.GetAxisRaw( isPlayer1 ? "Vertical" : "Vertical2" ) * moveSpeed); // Vertical moveset for player 1, Vertical2 moveset for player 2
        }
    
    }

    // Just reseting the positions of the players
    public void RoundStart()
    {
        transform.position = defPosition;
    }
    
}


// CODE JUNKYARD

// DASHING SYSTEM V2, dashing gameplay is kil                                                                                                                       no
    /*
    public float dashSpeed = 10f;
    // PRIVATE VARIABLES
    private float tapMargin = 0.2f;
    private float tapCooldown;
    private float rawAxisInput;
    private float lastRawAxisInput;
    private int tapCount = 0;
    private bool isNewInput;

    void Update()
    {
        rawAxisInput = Input.GetAxisRaw( isPlayer1 ? "Vertical" : "Vertical2" );

        if (rawAxisInput != 0)
        {
            //isNewInput = lastRawAxisInput == 0;
            if (tapCooldown > 0 && tapCount == 1)
            {
                Debug.Log("DASH!!!");
                rb.velocity *= 1.05f;
            } else {
                rb.velocity = new Vector2(0, rawAxisInput * moveSpeed);
                tapCooldown = tapMargin;
                tapCount += (int)rawAxisInput;
            }
            
        } else {
            //rb.velocity = new Vector2(0, rawAxisInput * moveSpeed);
        }

        if ( tapCooldown > 0f )
        {
            tapCooldown -= Time.deltaTime;
        } else {
            Debug.Log("Tap Reset");
            tapCount = 0;
        }

        lastRawAxisInput = rawAxisInput;
    }
    */

// OLD MOVEMENT SYSTEM WITH DASHING, is very broke
    /*

    public float dashCooldown = 1f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;

    // PRIVATE VARIABLES
    private float doubleTapTimeMargin = 0.2f;
    private float lastTapTime;
    private float lastDashTime;
    private float timeSinceLastDash;
    private float lastRawAxisInput;
    

    void Update()
    {
        float rawAxisInput = Input.GetAxisRaw( isPlayer1 ? "Vertical" : "Vertical2" );

        
        
        Old Dashing Code, is broke yo
        if ( rawAxisInput != 0 )
        {
            float timeSinceLastTap = TimeDifference(lastTapTime);
            //Debug.Log(timeSinceLastTap);
            if (  timeSinceLastTap < doubleTapTimeMargin )
            {
                Debug.Log( "Double Tap Detected" );
                
                if ( lastRawAxisInput == rawAxisInput )
                {
                    Debug.Log( lastRawAxisInput == 1 ? "Going Up" : "Going Down");
                    if ( TimeDifference(lastDashTime) > dashCooldown ) 
                    {
                        Debug.Log( TimeDifference(lastDashTime) );
                        if ( TimeDifference(lastDashTime) > dashDuration )
                        {
                            Debug.Log("DASH!!!");
                            rb.velocity = new Vector2( 0 , rawAxisInput * dashSpeed);
                            lastDashTime = Time.time;
                        }
                    }
                }

            } else {
                rb.velocity = new Vector2( 0 , rawAxisInput ) * moveSpeed;
            }
            lastTapTime = Time.time;
        } else {
            rb.velocity = new Vector2(0,0);
        }

        lastRawAxisInput = rawAxisInput;

    }

    public float TimeDifference(float time) // Returns the amount of seconds (in float form) between current time and the time mark used as the argument.
    {
        return Time.time - time;
    }
    */


// Code Before Gameplay Pivot: the Player paddles were subject to gravity and were supposed to jump to reach the ball
    /*  
    [SerializeField] private LayerMask platformLayerMask;
    [HideInInspector] public BoxCollider2D col2d;
    [HideInInspector] public Rigidbody2D rb;
    public float moveSpeed = 20f; // multiplier for the movement speed
    public float jumpForce = 20f; // multiplier for jumping
    public float dropForce = -2f; // multiplier for forcefully dropping from midair; Must be a negative value for going down the y-axis.
    // Start is called once on startup
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<BoxCollider2D>();
    }

    // Putting all physics code in FixedUpdate because it's more consistent
    void FixedUpdate()
    {
        float xMoveSpeed = moveSpeed * Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xMoveSpeed , rb.velocity.y);
        if (isGrounded() && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        print("Speed: " + rb.velocity.x);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y + dropForce);
        }
    }

    private bool isGrounded ()
    {
        float margin = 0.05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(col2d.bounds.center, Vector2.down, col2d.bounds.extents.y + margin, platformLayerMask);

        bool isHit = raycastHit.collider != null;

        Color raycastColor = isHit ? Color.green : Color.red;

        Debug.DrawRay(col2d.bounds.center, Vector2.down * (col2d.bounds.extents.y + margin), raycastColor);

        print("Grounded: " + raycastHit.collider + " " + isHit);
        return isHit;
        
    }
    */

