using UnityEngine;

public class S_DustVFX : MonoBehaviour
{
    public GameObject player;
    private S_PlayerController pController;
    private ParticleSystem vfx;
    private bool isMoving = false;
    private bool lastIsMoving = true;
    private bool canMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vfx = GetComponent<ParticleSystem>();
        pController = player.GetComponent<S_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        canMove = pController.canMove;

        // Define if the player is moving or not
        if(player.GetComponent<Rigidbody>().linearVelocity == Vector3.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        // Is the player movement has changed, enable or disable VFX accordingly
        if(lastIsMoving != isMoving && isMoving)
        {
            if(canMove)
            {
                EnableVFX();
            }
        }
        else if (lastIsMoving != isMoving && !isMoving || !canMove)
        {
            DisableVFX();
        }
    }

    private void EnableVFX()
    {
        //lastCanDash = pController.canDash;
        lastIsMoving = true;
        vfx.Play();
    }

    private void DisableVFX()
    {
        //lastCanDash = pController.canDash;
        lastIsMoving = false;
        vfx.Stop();
    }
}
