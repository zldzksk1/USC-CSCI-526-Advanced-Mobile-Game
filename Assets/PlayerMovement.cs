using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementState
    {
        WALK,
        SWING,
        GRAPPLE
    }

    public MovementState movementState;

    [Header("Movement")]
    public float moveSpeed; //please keep this as public, it is using on 
    public float walkSpeed;
    public float grappleSpeed;
    public float swingSpeed;


    public bool movePlayer; // prevent user from using keyboard during tutorials

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    //Made it public to prevent character from moving out after unfreeze input
    public float horizontalInput;
    public float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public float launchHoldTimer;
    public float launchHoldLimit = 2.0f;

    [SerializeField] private Collider _collider;
    private GameObject _mainCamera;
    private Manager _manager;
    private Transform playerObj;
    [SerializeField] private float rotationSpeed = 9.0f;

    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerObj = transform.Find("Capsule");

        readyToJump = true;
        launchHoldTimer = 0.0f;
        _manager = GameObject.Find("Game Manager").GetComponent<Manager>();
        capsuleCollider = transform.Find("Capsule").GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        Debug.DrawLine(capsuleCollider.bounds.center, transform.position + (Vector3.down * (playerHeight * 0.5f + 0.3f)));
        //Debug.LogWarning($"grounded debug: {grounded}");
        // handle drag
        /*if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 2;*/
        /*if (!grounded)
        {
            _collider.material.dynamicFriction = 10.0f;
        }
        else
        {
            _collider.material.dynamicFriction = 0.1f;
        }*/

        CheckState();
        MyInput();
        SpeedControl();

    }

    private void CheckState()
    {

        switch(movementState)
        {
            case MovementState.WALK:
                {
                    if (grounded)
                        rb.drag = groundDrag;
                    else
                        rb.drag = 1;
                    _collider.material.dynamicFriction = 10.0f;
                    moveSpeed = walkSpeed;
                    break;
                }
            case MovementState.GRAPPLE:
                {
                    rb.drag = 0;
                    _collider.material.dynamicFriction = 0.1f;
                    moveSpeed = grappleSpeed;
                    break;
                }
            case MovementState.SWING:
                {
                    rb.drag = 0;
                    _collider.material.dynamicFriction = 0.1f;
                    moveSpeed = swingSpeed;
                    break;
                }
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        if(!Manager.Instance.isPaused && Manager.Instance.canStart && movePlayer)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if(inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }

            /*if (!hasLaunched)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    launchHoldTimer += Time.deltaTime;
                    launchHoldTimer = Mathf.Clamp(launchHoldTimer, 0.0f, launchHoldLimit);
                    _manager.UpdateChargeText(launchHoldTimer);
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Vector3 launchForce = _mainCamera.transform.forward * launchHoldTimer * 10.0f;
                    Debug.Log("Launching: " + launchForce);
                    rb.AddForce(launchForce, ForceMode.Impulse);
                    SwingController swingController = GetComponent<SwingController>();
                    swingController.justLaunched = true;
                    swingController.launchVelocity = launchForce;
                    hasLaunched = true;
                    Manager.Instance.instructionText.text = "Left Click: Swing\r\nRight Click: Boost";
                }
            }*/

            /*if(hasLaunched)
            {
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    Vector3 launchForce = _mainCamera.transform.forward * 5.0f;
                    rb.AddForce(launchForce, ForceMode.Impulse);
                }
            }*/

            // when to jump
            if (Input.GetKey(jumpKey) && readyToJump && grounded && !Handicap.HasHandicapStatic(jumpKey))
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground

        switch(movementState)
        {
            case MovementState.SWING:
                {
                    rb.AddForce(moveDirection.normalized * moveSpeed * 1.5f, ForceMode.Force);
                    break;
                }
            case MovementState.WALK:
                {
                    rb.AddForce(moveDirection.normalized * moveSpeed * 2.15f, ForceMode.Force);
                    break;
                }
        }
        /*if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 2.15f, ForceMode.Force);*/

        // in air
        /*else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 2.15f *//* * airMultiplier*//*, ForceMode.Force);
        }*/
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}