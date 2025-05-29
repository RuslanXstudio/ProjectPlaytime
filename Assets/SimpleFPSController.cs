using UnityEngine;
using System.Collections;
using System.Linq; 

[RequireComponent(typeof(Rigidbody))]
public class SimpleFPSController : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float crouchSpeed = 1.5f;
    public float mouseSensitivity = 2.0f;
    public float smoothTime = 0.1f;
    public float jumpForce = 5.0f;
    public float crouchHeight = 0.5f;
    public float playerHeight = 2.0f;
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);
    public Vector3 playerCenter = new Vector3(0, 0, 0);
    public bool enableHeadBob = true;
    public float bobSpeed = 0.18f;
    public float bobAmount = 0.2f;

    public Camera playerCamera;
    private Rigidbody rb;
    private float rotationX = 0;
    public bool isGrounded;
    public bool hasheadspace;

    private Vector3 originalControllerCenter;
    private float headBobTimer = 0;
    public bool isCrouching = false;

    private float originalWalkSpeed;
    private float originalBobSpeed;
    private CapsuleCollider playerCollider;

    public Transform weaponTransform; // Assign your weapon object's transform here
    public float weaponBobbingMultiplier = 0.001f;
    public float weaponBobSpeed = 0.001f; // Add this line to declare the weapon bob speed

    public Animator grabpackanims;


    public LayerMask pickupLayer;
    public float interactionDistance = 5f;
    public float crouchTransitionSpeed = 5f;
    private Coroutine crouchCoroutine;

    public LayerMask DoorLayer;

    public Animator cameraanims;
    public AudioSource footstepSource;
    public AudioClip[] footstepSounds;  // One array for all footstep sounds
    private float footstepInterval;
    private float footstepTimer = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        grabpackanims.SetBool("isWalking", false);
        cameraanims.SetBool("walking", false);

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalControllerCenter = playerCamera.transform.localPosition;
        playerCollider = GetComponent<CapsuleCollider>();

        originalWalkSpeed = walkSpeed;
        originalBobSpeed = bobSpeed;
    }

    void Update()
    {
        if (isGrounded)
        {
            HandleJump();
            HandleFootsteps(); 

        }

        CheckGrounded();
        HandleMovement();
        HandleMouseLook();
        HandleHeadBob();
        CheckHeadSpace();

        if (hasheadspace == false)
        {
            HandleCrouch();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Get the main camera
            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found!");
                return;
            }

            // Cast a ray from the center of the main camera's viewport
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, pickupLayer))
            {
                HandPickup pickupObject = hit.collider.GetComponent<HandPickup>();

                pickupObject.Pickup();

                Debug.Log("Picked up");

            }

            if (Physics.Raycast(ray, out hit, interactionDistance, DoorLayer))
            {
                Door door = hit.collider.GetComponent<Door>();

                Debug.Log("Picked up");
                door.Interact();
            }

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("GrabTurret"))
                {
                    FireHand firehand;
                    PassHandLauncherRefernce passreference;
                    passreference = hit.collider.gameObject.GetComponent<PassHandLauncherRefernce>();
                    firehand = passreference.firehand1;

                    if (firehand.isMovingToTarget == false && firehand.isMovingBack == false)
                    {
                        // Find the child named "grabhandcanvas"
                        Transform grabhandTransform = hit.transform.GetComponentsInChildren<Transform>(true)
                                                        .FirstOrDefault(t => t.name == "grabhandcanvas");

                        if (grabhandTransform != null)
                        {
                            GameObject grabhandcanvas = grabhandTransform.gameObject;
                            grabhandcanvas.SetActive(true); // Activate the canvas
                        }
                        else
                        {
                            Debug.LogWarning("grabhandcanvas not found!");
                        }
                    }

                }
            }
        }
    }

    void HandleFootsteps()
    {
        if (!isGrounded || rb.velocity.magnitude < 0.1f)
            return;  // Don't play footsteps if not grounded or not moving

        // Adjust the footstep interval based on movement type
        if (isCrouching)
        {
            footstepInterval = 0.5f;  // Slower interval when crouching

            footstepSource.volume = 0.85f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))  // Sprinting
        {
            footstepInterval = 0.28f;  // Faster interval when sprinting
            footstepSource.volume = 1;

        }
        else  // Walking
        {
            footstepInterval = 0.5f;
            footstepSource.volume = 1;

        }

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            PlayRandomFootstep();
            footstepTimer = footstepInterval;
        }
    }

    void PlayRandomFootstep()
    {
        if (footstepSounds.Length == 0) return;

        int randomIndex = Random.Range(0, footstepSounds.Length);
        footstepSource.PlayOneShot(footstepSounds[randomIndex]);
    }

    void FixedUpdate()
    {

    }

    void CheckGrounded()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 2f);

        // Debug information: Draw the ray
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);

        if (isGrounded)
        {
           // grabpackanims.ResetTrigger("Jump");
        }
    }

    void CheckHeadSpace()
    {
        RaycastHit hit;
        hasheadspace = Physics.Raycast(transform.position, Vector3.up, out hit, 2f);

        // Debug information: Draw the ray
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);

        if (hasheadspace)
        {
            // grabpackanims.ResetTrigger("Jump");
        }
    }

    void HandleMovement()
    {
        float moveSpeed = isCrouching ? crouchSpeed : walkSpeed;

        if (!isGrounded)
            return;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            moveSpeed = sprintSpeed;
            bobSpeed = originalBobSpeed * 2.5f; // Speed up head bobbing during sprinting


        }
        else
        {
            bobSpeed = originalBobSpeed;

        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            grabpackanims.SetBool("isRunning", false);
            grabpackanims.SetBool("isWalking", true);

            cameraanims.SetBool("walking", true);
            cameraanims.SetBool("running", false);

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                if (!isCrouching)
                {
                    grabpackanims.SetBool("isRunning", true);
                    grabpackanims.SetBool("isWalking", false);

                    cameraanims.SetBool("walking", false);
                    cameraanims.SetBool("running", true);
                }
                else
                {
                    grabpackanims.SetBool("isRunning", false);
                    grabpackanims.SetBool("isWalking", true);

                    cameraanims.SetBool("walking", true);
                    cameraanims.SetBool("running", false);
                }

            }
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
        {
            grabpackanims.SetBool("isRunning", false);
            grabpackanims.SetBool("isWalking", false);

            cameraanims.SetBool("walking", false);
            cameraanims.SetBool("running", false);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        Vector3 velocity = transform.TransformDirection(moveDirection) * moveSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
        {
            StandUp();
        }
    }

    void Crouch()
    {
        if (crouchCoroutine != null)
            StopCoroutine(crouchCoroutine);

        crouchCoroutine = StartCoroutine(CrouchTransition(crouchCenter, crouchHeight, crouchSpeed));
    }

    void StandUp()
    {
        if (crouchCoroutine != null)
            StopCoroutine(crouchCoroutine);

        crouchCoroutine = StartCoroutine(CrouchTransition(originalControllerCenter, playerHeight, originalWalkSpeed));
    }

    IEnumerator CrouchTransition(Vector3 targetCenter, float targetHeight, float targetSpeed)
    {
        float elapsedTime = 0f;
        float duration = 1f / crouchTransitionSpeed; // Controls the smoothness of the transition

        Vector3 startingCameraPos = playerCamera.transform.localPosition;
        float startingHeight = playerCollider.height;
        float startingWalkSpeed = walkSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Smoothly interpolate the camera position, collider height, and walk speed
            playerCamera.transform.localPosition = Vector3.Lerp(startingCameraPos, targetCenter, t);
            playerCollider.height = Mathf.Lerp(startingHeight, targetHeight, t);
            walkSpeed = Mathf.Lerp(startingWalkSpeed, targetSpeed, t);

            yield return null;
        }

        // Ensure the final values are set after the transition
        playerCamera.transform.localPosition = targetCenter;
        playerCollider.height = targetHeight;
        walkSpeed = targetSpeed;

        isCrouching = targetHeight == crouchHeight;
    }

    void HandleJump()
    {
        if (isCrouching == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                grabpackanims.SetTrigger("Jump");
                cameraanims.SetTrigger("jump");

            }
        }

    }


    void HandleHeadBob()
    {
        if (!enableHeadBob)
            return;
        if (!isGrounded)
            return;

        float waveslice = 0;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            headBobTimer = 0;
        }
        else
        {
            waveslice = Mathf.Sin(headBobTimer);
            headBobTimer += bobSpeed;
            if (headBobTimer > Mathf.PI * 2)
            {
                headBobTimer = 0;
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobAmount;
            float totalAxes = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            translateChange *= totalAxes;

            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition,
                new Vector3(originalControllerCenter.x, originalControllerCenter.y - translateChange, originalControllerCenter.z),
                Time.deltaTime * smoothTime);

            // Apply weapon bobbing to the weapon's transform
            if (weaponTransform != null)
            {
                float weaponBobbingAmount = waveslice * bobAmount * weaponBobSpeed; // Use the customized weapon bob speed
                weaponTransform.localPosition = new Vector3(weaponTransform.localPosition.x,
                                                             weaponTransform.localPosition.y - weaponBobbingAmount,
                                                             weaponTransform.localPosition.z);
            }
        }
    }
}