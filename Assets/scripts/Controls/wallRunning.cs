using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float groundDetection;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Exiting")]
    private bool allowToWallJump = false;
    private bool isJumped = false;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    public cameraControl cam;
    private movementControl pm;
    private Rigidbody rb;



    private void Start()
    {
        pm = GetComponent<movementControl>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Inputs();
        CheckWall();
        StateMechine();
        if (pm.wallrunning)
        {
            if(!isJumped)
                WallRunning();
            //if(allowToWallJump)
                WallJump();
        }
    }
    

    private void CheckWall()
    {
        wallLeft = Physics.Raycast(orientation.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        wallRight = Physics.Raycast(orientation.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
    }

    public bool AboveGround() => !Physics.Raycast(transform.position, Vector3.down, groundDetection, whatIsGround);

    private void StateMechine()
    {
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && horizontalInput != 0)
        {
            StartWallRun();
        }
        else
            StopWallRunning();
    }

    private void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void StartWallRun()
    {
        
        pm.wallrunning = true;
        rb.useGravity = false;
    }

    private void WallRunning()
    {
        //allowToWallJump = true;
        Vector3 normalVector = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Transform normalObj = wallRight ? rightWallhit.transform : leftWallhit.transform;
        Vector3 forwardWallVector = Vector3.Cross(transform.up, normalVector);

        if ((orientation.forward - forwardWallVector).magnitude > (orientation.forward - -forwardWallVector).magnitude)
            forwardWallVector = -forwardWallVector;

        rb.velocity = forwardWallVector * wallRunForce * 100 * Time.deltaTime;
        if (Input.GetKey(upwardsRunKey))
        {
            if ((transform.position + Vector3.up * wallClimbSpeed * Time.deltaTime).y < (normalObj.GetChild(1).GetChild(0).transform.position).y)
                rb.MovePosition(transform.position + (Vector3.up * wallClimbSpeed * Time.deltaTime));
            else rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        else if (Input.GetKey(downwardsRunKey))
            rb.MovePosition(transform.position + (Vector3.down * wallClimbSpeed * Time.deltaTime));

        if (wallLeft && horizontalInput > 0 || wallRight && horizontalInput < 0)
        {
            rb.AddForce(normalVector * 3, ForceMode.Impulse);
            isJumped = true;
            Invoke(nameof(resetJump), .08f);
        }
    }
    private void WallJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.velocity /= 3;
            Vector3 normalVector = wallRight ? rightWallhit.normal : leftWallhit.normal;
            rb.AddForce(normalVector * 13f + Vector3.up * 10f, ForceMode.Impulse);
            isJumped = true;
            Invoke(nameof(resetJump), .08f);
            allowToWallJump = false;
            Debug.Log("ÏÐÛÃÍÓË ×ÅËÈÊ");
        }
    }
    private void resetJump() => isJumped = false;
    //private void resetWallJump() => allowToWallJump = false;
    private void StopWallRunning()
    {
        pm.wallrunning = false;
        rb.useGravity = true;
        //allowToWallJump = false;
    }

}
