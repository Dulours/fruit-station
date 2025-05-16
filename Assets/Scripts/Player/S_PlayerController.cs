using UnityEngine;
using System.Collections;

public class S_PlayerController : MonoBehaviour
{
    // Movement
    public float moveSpeed = 7.0f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float turnSpeed = 1f;
    private float horizontalInput;
    private float speedChange = 0f;
    public bool canMove = true;
    private Vector3 desiredVelocity;
    private Vector3 velocity;    
    private Rigidbody body;

    // Dash
    public AnimationCurve dashVelocityX;
    public float dashCooldown = 3f;
    private float timeElapsed = 0f;
    private bool canDashRight = true;
    private bool canDashLeft = true;
    public bool canDash = true;
    private Vector3 startingDashPos;

    // Health
    public float healthPoints = 3f;
    public GameObject mainCam;
    private S_ScreenShake camShake;
    private GameObject meshBase;
    private GameObject meshMid;
    private GameObject meshLow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        camShake = mainCam.GetComponent<S_ScreenShake>();
        meshBase = gameObject.transform.GetChild(0).gameObject;
        meshMid = gameObject.transform.GetChild(1).gameObject;
        meshLow = gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Clamp the x position value
        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -3.3f, 3.3f);
        transform.position = pos;

        // Stop movement and dash if the player's health is zero
        if(healthPoints <= 0)
        {
            canMove = false;
            canDash = false;
            body.linearVelocity = Vector3.zero;
            desiredVelocity = Vector3.zero;
        }

        // Get the player input
        horizontalInput = Input.GetAxis("Horizontal");

        // Set the desired velocity which is left or right vector (depending on player's input) multiplied by the speed of the character
        desiredVelocity = new Vector3(horizontalInput, 0f, 0f) * moveSpeed;

        // Check the input direction when pressing dash button
        if(canDash && Input.GetButtonDown("Dash"))
        {
            if (horizontalInput > 0 && canDashRight)
            {
                startingDashPos = transform.position;
                StartCoroutine(DashCurve(1f));
            }
            else if (horizontalInput < 0 && canDashLeft)
            {
                startingDashPos = transform.position;
                StartCoroutine(DashCurve(-1f));
            }                       
        }

        // Display base mesh if health points are full (useful after a restart, might want to move that into the Flow manager
        if (healthPoints >= 3)
        {
            meshBase.SetActive(true);
            meshMid.SetActive(false);
            meshLow.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // We switch to FixedUpdate because we are doing physics calculations
        // Check if there is a player input
        if (horizontalInput != 0)
        {
            // Check if the player input is in the same direction as the current character's velocity and setup speedChange accordingly
            // There are three speed :
            // the turn speed if the player's input is in the opposite direction as the current velocity
            // the accel, if the player's input is in the same direction as the velocity
            // the decel if the player do not input anything
            if (Mathf.Sign(horizontalInput) != Mathf.Sign(velocity.x))
            {
                speedChange = turnSpeed * Time.deltaTime;
            }
            else
            {
                speedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            speedChange = deceleration * Time.deltaTime;
        }

        if (canMove == true)
        {
            // Set up the velocity and update it in the rigidbody component
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, speedChange);
            body.linearVelocity = velocity;
        }
    }

    IEnumerator DashCurve(float direction)
    {
        // Prevent player from dashing or moving during a dash
        canDash = false;
        canMove = false;

        // Set up dash direction
        if (direction == 1f) { canDashLeft = false; }
        if (direction == -1f) { canDashRight = false; }

        // Performing a dash
        while (timeElapsed < dashVelocityX[dashVelocityX.length - 1].time)
        {
            timeElapsed += Time.deltaTime;
            body.MovePosition(new Vector3(startingDashPos.x + dashVelocityX.Evaluate(timeElapsed) * direction, startingDashPos.y, startingDashPos.z));
            yield return null;
        }

        // Reset of the timer, player can move again
        canMove = true;
        timeElapsed = 0f;

        while(timeElapsed < dashCooldown)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // End of cooldown, player can dash again
        print("cooldown's over");
        canDash = true;
        canDashRight = true;
        canDashLeft = true;
        timeElapsed = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
            healthPoints -= 1;
            camShake.isShaking = true;

            // Display correct mesh depending on health left
            if (healthPoints == 2)
            {
                meshBase.SetActive(false);
                meshMid.SetActive(true);
                meshLow.SetActive(false);
            }
            else if (healthPoints <= 1)
            {
                meshBase.SetActive(false);
                meshMid.SetActive(false);
                meshLow.SetActive(true);
            }
        }
    }
}
