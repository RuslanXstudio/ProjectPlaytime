using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GrabPack : MonoBehaviour
{
    public Animator grabpackanims;
    public SkinnedMeshRenderer GreenHandObject;
    public AudioSource audioPlayer;
    public GameObject greenhandpickup;
    public float throwforce = 9.0f;
    public GameObject batteryPreab; // Reference to the prefab you want to instantiate
    public bool pullingPlayer = false;
    bool isscanningblue;

    public LineRendererManager LRM;
    public List<GameObject> BlueHandScanners;

    public GameObject Linerenderer1;
    public GameObject Linerenderer2;
    public GameObject Linerenderer3;
    public GameObject Linerenderer4;
    public AudioClip drag;
    public GameObject RightBatteryLocation;
    public GameObject LeftBatteryLocation;

    public AudioClip handgrab;
    public AudioClip handretract;
    public AudioClip boostsound;

    public Transform ogposright;
    public Transform ogposleft;

    public float delayDrag = 0.4f;
    public Transform leftHand;
    public Transform rightHand;
    public LayerMask grabbableLayer;
    public LayerMask purplehandscanner;
    public LayerMask handle;
    public LayerMask greenSupply;
    public LayerMask greenReceiver;
    public LayerMask battery;
    public LayerMask bluehandscanner;
    public LayerMask Default;
    public LayerMask powerSource;
    public LayerMask powerReciever;
    public LayerMask DashHandle;
    public LayerMask cutout;
    public LayerMask doorlayer;
    public LayerMask BatteryDisplay;

    public bool canPickupBattery = true;

    public bool hasbeengrabbed = false;

    public float upforce = 0.7f;

    public bool releasedOnce = false; // Add this boolean variable


    public Rigidbody rigidbodyObject;
    public float upwardForce = 5f;

    public float delayBeforeRetract = 0.4f;
    public float retractTimer = 0f;

    public float maxDistance = 5f;
    public float dragSpeed = 5f; // Adjust the speed as needed
    public float dragSpeed1 = 12f; // Adjust the speed as needed
    public float dashspeed = 18f; // Adjust the speed as needed

    private GameObject leftGrabbedObject;
    private GameObject rightGrabbedObject;

    public Transform leftHandPlaceholder;
    public Transform rightHandPlaceholder;

    public Transform leftRetractParent;
    public Transform rightRetractParent;

    public LineRenderer leftCableRenderer;
    public LineRenderer rightCableRenderer;

    public LineRenderer PurpleLineRenderer;
    public LineRenderer GreenLineRenderer;



    public Transform leftCableHolder;
    public Transform rightCableHolder;

    public bool retractRightHand = false;
    public bool retractLeftHand = false;

    public GameObject PurpleHand;

    public GameObject PurpleHandParent;

    public GameObject GreenHand;

    public GameObject GreenHandParent;

    public GameObject EMPTY;
    public LineRenderer EMPTYrenderer;

    public bool righthandable = true;

    public bool canboost = false;

    public bool purplehandout = false;

    public bool greenhandout = true;

    public float greenpowerTime = 10f;

    public bool HasGreenPower;
    public GameObject Greenpowereffect;
    public GameObject GreenPowerLight;
    public Material GreenGlow;
    public Material normalgreen;

    public GameObject greenhandmeshrenderer;

    public GameObject FlareProjectile;

    public bool FlareHandOut;
    public GameObject Barrel;
    public float FlareForce = 0.45f;
    private float cooldownTimer = 0f;
    public float cooldownDuration = 1.0f; // Set the cooldown duration in seconds
    public float flareLifetime = 10f; // Adjust the lifetime as needed
    public AudioClip flaresound;

    public AudioClip grabElectricity;
    public AudioClip electricbuzz;

    public float reloadTimer = 2.5f;
    public float flareammo = 5;
    public TextMesh textMesh;

    public GameObject reloading;

    private float smoothMoveSpeed = 10f; // Adjust the speed as needed

    public float smoothMoveDuration = 0.36f; // You can adjust the duration as needed

    public bool leftHandGrabbed = false;
    public bool rightHandGrabbed = false;

    public bool isDragging;

    public bool holdingBattery = false;

    public float batterytimer;

    public bool canrealesebattery = false;

    public bool powerpuzzle;

    public string powerpuzzlehand;
    public GameObject draggingsounds;

    public bool StartWithGreenHand = true;

    public bool isretracting = false;

    public GameObject LeftSounds;
    public GameObject RightSounds;

    public AudioClip creakingswing;

    void Start()
    {
        UpdateText();
        if (!StartWithGreenHand)
        {
            greenhandpickup.SetActive(true);
        }

        if (!StartWithGreenHand)
        {
            rightCableRenderer.enabled = false;


            retractRightHand = true;

            rightCableRenderer = EMPTYrenderer;

            rightHand = EMPTY.transform;

            rightRetractParent = transform;


            righthandable = false;

            purplehandout = false;

            greenhandout = true;

            FlareHandOut = false;

            rightHandGrabbed = false;
            
            GreenHandObject.enabled = false;
        }
    }

    public void GreenHandPickUp()
    {
        rightCableRenderer.enabled = false;
        rightHandGrabbed = false;

        retractRightHand = true;
        rightHand = GreenHand.transform;

        rightRetractParent = GreenHandParent.transform;

        rightCableRenderer = GreenLineRenderer;

        righthandable = true;

        purplehandout = false;

        greenhandout = true;

        FlareHandOut = false;
    }
    void LateUpdate()
    {
        grabpackanims.ResetTrigger("Retract");

    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            LeftSounds.SetActive(false);
        }
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            RightSounds.SetActive(false);
        }



        if (holdingBattery == true)
        {
            batterytimer -= Time.deltaTime;

            if (batterytimer <= 0)
            {
                canrealesebattery = true;
            }
        }

        if (!leftHandGrabbed && !rightHandGrabbed)
        {
            if (isretracting)
            {
                grabpackanims.SetTrigger("Retract");
                isretracting = false;


            }
        }

        if (leftHandGrabbed == false)
        {
            if (powerpuzzlehand == ("Left"))
            {
                powerpuzzle = false;

                if (!powerpuzzle)
                {
                    LRM.RemovePointsSinceStart();

                }

            }


            leftHand.transform.position = ogposleft.transform.position;
            leftHand.transform.rotation = ogposleft.transform.rotation;


        }

        if (leftHandGrabbed || rightHandGrabbed)
        {
            isretracting = true;

        }
        if (rightHandGrabbed == false)
        {

            if (powerpuzzlehand == ("Right"))
            {
                powerpuzzle = false;
                if (!powerpuzzle)
                {
                    LRM.RemovePointsSinceStart();

                }


            }


            rightHand.transform.position = ogposright.transform.position;
            rightHand.transform.rotation = ogposright.transform.rotation;

        }

        if (rightHandGrabbed == false && leftHandGrabbed == false)
        {
            isDragging = false;
            pullingPlayer = false;

        }


        if (HasGreenPower == true)
        {
            Greenpowereffect.SetActive(true);
            GreenPowerLight.SetActive(true);
            greenhandmeshrenderer.GetComponent<Renderer>().material = GreenGlow;

            greenpowerTime -= Time.deltaTime;

            if (greenpowerTime <= 0)
            {
                HasGreenPower = false;
                Greenpowereffect.SetActive(false);
                GreenPowerLight.SetActive(false);
                greenhandmeshrenderer.GetComponent<Renderer>().material = normalgreen;
            }
        }

        if (HasGreenPower == false)
        {
            HasGreenPower = false;
            Greenpowereffect.SetActive(false);
            GreenPowerLight.SetActive(false);
            greenhandmeshrenderer.GetComponent<Renderer>().material = normalgreen;
        }

        HandleGrabInput(leftHand, ref leftGrabbedObject, leftRetractParent, KeyCode.Mouse0, leftCableRenderer, leftCableHolder);
        HandleGrabInput(rightHand, ref rightGrabbedObject, rightRetractParent, KeyCode.Mouse1, rightCableRenderer, rightCableHolder);

        // Update the retract timer
        if (retractTimer >= 0)
        {
            if (canboost == true)
            {
                retractTimer -= Time.deltaTime;
            }
        }

        // Check if the timer has reached 0
        if (retractTimer <= 0)
        {
            // Set the flag to retract the right hand after the delay
            retractRightHand = true;
            canboost = false;
            retractLeftHand = true;
            rightHandGrabbed = false;
            leftHandGrabbed = false;


            retractTimer = 0.2f;




        }



        if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            dragSpeed1 = 12;
        }
        if (!Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            dragSpeed1 = 12;
        }

        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            dragSpeed1 = 4;
        }


        UpdateText();

        if (retractRightHand == true)
        {
            rightHand.position = rightHandPlaceholder.position;
            rightHand.rotation = rightHandPlaceholder.rotation;
            rightHand.parent = rightRetractParent;

            retractRightHand = false;

        }

        if (retractLeftHand == true)
        {
            leftHand.position = leftHandPlaceholder.position;
            leftHand.rotation = leftHandPlaceholder.rotation;
            leftHand.parent = leftRetractParent;

            retractLeftHand = false;
        }




        if (holdingBattery == false)
        {
            if (rightHandGrabbed == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (StartWithGreenHand)
                    {


                        GreenHandPickUp();
                    }

                    if (!StartWithGreenHand)
                    {
                        rightCableRenderer.enabled = false;


                        retractRightHand = true;

                        rightCableRenderer = EMPTYrenderer;

                        rightHand = EMPTY.transform;

                        rightRetractParent = transform;


                        righthandable = false;

                        purplehandout = false;

                        greenhandout = true;

                        FlareHandOut = false;

                        rightHandGrabbed = false;

                        GreenHandObject.enabled = false;
                    }



                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    rightCableRenderer.enabled = false;


                    retractRightHand = true;

                    rightHand = PurpleHand.transform;

                    rightRetractParent = PurpleHandParent.transform;

                    rightCableRenderer = PurpleLineRenderer;

                    righthandable = true;

                    purplehandout = true;

                    greenhandout = false;

                    FlareHandOut = false;

                    rightHandGrabbed = false;

                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    rightCableRenderer.enabled = false;


                    retractRightHand = true;

                    rightCableRenderer = EMPTYrenderer;

                    rightHand = EMPTY.transform;

                    rightRetractParent = transform;


                    righthandable = false;

                    purplehandout = false;

                    greenhandout = false;

                    FlareHandOut = true;

                    rightHandGrabbed = false;

                }
            }



        }



        if (FlareHandOut == true)
        {


            cooldownTimer -= Time.deltaTime;

            if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0f)
            {
                if (flareammo >= 0.9f)
                {
                    if (Barrel != null)
                    {
                        Vector3 spawnPosition = Barrel.transform.position;
                        Quaternion spawnRotation = Barrel.transform.rotation;

                        GameObject instantiatedFlare = Instantiate(FlareProjectile, spawnPosition, spawnRotation);

                        Rigidbody prefabRigidbody = instantiatedFlare.GetComponent<Rigidbody>();
                        if (prefabRigidbody != null)
                        {
                            prefabRigidbody.AddForce(instantiatedFlare.transform.forward * FlareForce, ForceMode.Impulse);
                            audioPlayer.PlayOneShot(flaresound, 1.0f);
                            flareammo -= 1;

                        }
                        else
                        {
                            Debug.LogWarning("Rigidbody component not found on the instantiated prefab!");
                        }

                        // Destroy the instantiated flare projectile after flareLifetime seconds
                        Destroy(instantiatedFlare, flareLifetime);

                        cooldownTimer = cooldownDuration;
                    }
                    else
                    {
                        Debug.LogWarning("Barrel object is not assigned!");
                    }
                }

            }

        }

        if (flareammo <= 0.9f)
        {
            reloading.SetActive(true);
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0f)
            {
                reloadTimer = 3.8f;
                flareammo = 5;
            }

        }
        if (flareammo >= 0.9)
        {
            reloading.SetActive(false);

        }


    }

    void UpdateText()
    {
        // Set the text to display the float value
        textMesh.text = flareammo.ToString("F0"); // "F2" specifies two decimal places
    }

    void HandleGrabInput(Transform hand, ref GameObject grabbedObject, Transform retractParent, KeyCode grabKey, LineRenderer cableRenderer, Transform cableHolder)
    {
        if (Input.GetKeyDown(grabKey))
        {
            if (grabbedObject == null)
            {
                TryGrab(hand, ref grabbedObject);

                delayDrag = 0.45f;
            }
            else
            {
                ReleaseObject(hand, ref grabbedObject, retractParent, cableRenderer, cableHolder);
                hasbeengrabbed = false;
            }
        }

        if (Input.GetKey(grabKey))
        {

            if (isDragging == false)
            {
                delayDrag = 0.6f;
                isDragging = true;
            }


            DragObject(hand, grabbedObject);
        }

        if (grabbedObject != null)
        {
            MoveHandToHitPosition(hand, grabbedObject);
            UpdateCable(hand, cableRenderer, cableHolder);
        }
        else
        {
            HideCable(cableRenderer);
        }

        if (retractTimer <= 0.05f)
        {
            grabbedObject = null;

        }

    }

    void UpdateCable(Transform hand, LineRenderer cableRenderer, Transform cableHolder)
    {
        // Set the cable's start position to the hand's position
        cableRenderer.SetPosition(0, hand.position);

        // Set the cable's end position to the cable holder's position
        cableRenderer.SetPosition(1, cableHolder.position);

        // Show the cable renderer
        cableRenderer.enabled = true;
    }

    void HideCable(LineRenderer cableRenderer)
    {
        // Hide the cable renderer when not grabbing
        cableRenderer.enabled = false;
    }

     public void TryGrab(Transform hand, ref GameObject grabbedObject)
     {
        if (!StartWithGreenHand)
        {
            if (hand == rightHand)
            {
                if (greenhandout)
                {
                    return;
                }
            }
            
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, grabbableLayer))
        {
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;

            Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

            if (FlareHandOut)
            {
                if (hand == leftHand)
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }
            }
            else
            {
                audioPlayer.PlayOneShot(handgrab, 0.7f);
            }
        }


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, purplehandscanner))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;

            Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

            if (FlareHandOut)
            {
                if (hand == leftHand)
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }
            }
            else
            {
                audioPlayer.PlayOneShot(handgrab, 0.7f);
            }
            // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
            if (hand == rightHand && grabbedObject.layer == LayerMask.NameToLayer("PurpleHandScanner"))
            {
                if (purplehandout == true)
                {
                    Debug.Log("boost");
                    canboost = true;

                    rigidbodyObject.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);

                    audioPlayer.PlayOneShot(boostsound, 1.1f);

                    // Start the timer for the delay before retracting
                    retractTimer = delayBeforeRetract;

                    audioPlayer.PlayOneShot(boostsound, 1.3f);
                }


            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, bluehandscanner))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                grabbedObject = hit.collider.gameObject;


                leftHandPlaceholder = hand;
                leftHandGrabbed = true;

                // Set the hand's position and rotation to the hit point and object's rotation smoothly
                StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

                // Make the hand a child of the grabbed object
                hand.parent = grabbedObject.transform;

                Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

                audioPlayer.PlayOneShot(handgrab, 0.7f);


                Animator progressbar = grabbedObject.GetComponent<Animator>();

                progressbar.SetBool("IsScanning", true);

                isscanningblue = true;

            }




        }


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, battery))
        {

            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            if (FlareHandOut == true && hand == rightHand)
            {
                canPickupBattery = false;

            }
            else
            {
                canPickupBattery = true;

            }

            if (canPickupBattery == true)
            {
                grabbedObject = hit.collider.gameObject;

                batterytimer = 0.1f;
                holdingBattery = true;
                // Store the original position and rotation of the hand
                if (hand == leftHand)
                {
                    leftHandPlaceholder = hand;
                    leftHandGrabbed = true;
                }
                else if (hand == rightHand)
                {
                    rightHandPlaceholder = hand;
                    rightHandGrabbed = true;
                }

                // Set the hand's position and rotation to the hit point and object's rotation smoothly
                StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

                // Make the hand a child of the grabbed object

                grabbedObject.transform.parent = hand.transform;

                Collider grabbedObjectCollider = grabbedObject.GetComponent<Collider>();


                if (hand == rightHand)
                {

                    grabbedObject.transform.position = RightBatteryLocation.transform.position;
                    grabbedObject.transform.rotation = RightBatteryLocation.transform.rotation;
                }

                if (hand == leftHand)
                {
                    grabbedObject.transform.position = LeftBatteryLocation.transform.position;
                    grabbedObject.transform.rotation = LeftBatteryLocation.transform.rotation;
                }


                Rigidbody grabbedObjectRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedObjectRigidbody != null)
                {
                    grabbedObjectRigidbody.isKinematic = true;
                }

                Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

                Debug.Log("boost");
                canboost = true;



                // Start the timer for the delay before retracting
                retractTimer = delayBeforeRetract;

                if (FlareHandOut)
                {
                    if (hand == leftHand)
                    {
                        audioPlayer.PlayOneShot(handgrab, 0.7f);
                    }
                }
                else
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }

                if (holdingBattery == true)
                {
                    Debug.Log("letgo");
                    if (canrealesebattery == true)
                    {
                        grabbedObject.transform.parent = null;
                        grabbedObjectRigidbody.isKinematic = false;
                        grabbedObjectCollider.enabled = true;
                        holdingBattery = false;
                        grabbedObjectRigidbody.AddForce(hand.forward * throwforce, ForceMode.Impulse);

                        canrealesebattery = false;
                    }

                }
            }
            




        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, handle))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            pullingPlayer = true;

            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                audioPlayer.PlayOneShot(creakingswing, 0.6f);

            }
            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;

            Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

            if (FlareHandOut)
            {
                if (hand == leftHand)
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }
            }
            else
            {
                audioPlayer.PlayOneShot(handgrab, 0.7f);
            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, DashHandle))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            grabbedObject = hit.collider.gameObject;
            releasedOnce = true;
            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;

                LeftSounds.SetActive(true);
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;

                RightSounds.SetActive(true);

            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;

            Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

            if (FlareHandOut)
            {
                if (hand == leftHand)
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }
            }
            else
            {
                audioPlayer.PlayOneShot(handgrab, 0.7f);
            }
        }


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, greenSupply))
        {
            if (StartWithGreenHand)
            {

                grabbedObject = hit.collider.gameObject;

                // Store the original position and rotation of the hand
                if (hand == leftHand)
                {
                    leftHandPlaceholder = hand;
                    leftHandGrabbed = true;
                }
                else if (hand == rightHand)
                {
                    rightHandPlaceholder = hand;
                    rightHandGrabbed = true;
                }

                // Set the hand's position and rotation to the hit point and object's rotation smoothly
                StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

                // Make the hand a child of the grabbed object
                hand.parent = grabbedObject.transform;

                Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

                if (FlareHandOut)
                {
                    if (hand == leftHand)
                    {
                        audioPlayer.PlayOneShot(handgrab, 0.7f);
                    }
                }
                else
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }
                // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
                if (hand == rightHand && grabbedObject.layer == LayerMask.NameToLayer("GreenPowerSupply"))
                {
                    if (greenhandout == true)
                    {
                        if (hand == rightHand)
                        {
                            greenpowerTime = 10f;
                            HasGreenPower = true;
                            audioPlayer.PlayOneShot(grabElectricity, 1.1f);
                            canboost = true;

                            retractTimer = delayBeforeRetract;
                        }


                    }


                }
            }


        }


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, greenReceiver))
        {
            if (StartWithGreenHand)
            {
                grabbedObject = hit.collider.gameObject;

                // Store the original position and rotation of the hand
                if (hand == leftHand)
                {

                    return;
                }
                else if (hand == rightHand)
                {
                    rightHandPlaceholder = hand;
                    rightHandGrabbed = true;
                }

                // Set the hand's position and rotation to the hit point and object's rotation smoothly
                StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

                // Make the hand a child of the grabbed object
                hand.parent = grabbedObject.transform;

                Debug.Log("Layer Name of grabbedObject: " + LayerMask.LayerToName(grabbedObject.layer));

                if (FlareHandOut)
                {
                    if (hand == leftHand)
                    {
                        audioPlayer.PlayOneShot(handgrab, 0.7f);
                    }
                }
                else
                {
                    audioPlayer.PlayOneShot(handgrab, 0.7f);
                }


                // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
                if (hand == rightHand && grabbedObject.layer == LayerMask.NameToLayer("GreenPowerReciever"))
                {


                    if (greenhandout == true)
                    {
                        if (hand == rightHand)
                        {
                            canboost = true;

                            greenpowerTime = 0.2f;
                            audioPlayer.PlayOneShot(electricbuzz, 1.1f);
                            retractTimer = delayBeforeRetract;
                        }

                    }

                }
            }

        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, cutout))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;
            canboost = true;

            retractTimer = delayBeforeRetract;

            voiceLine VoiceLine1 = grabbedObject.GetComponent<voiceLine>();
            VoiceLine1.PlayAudio();

        }
        if (Physics.Raycast(hand.position, hand.forward, out hit, maxDistance, doorlayer))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }
            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;
            canboost = true;

            retractTimer = delayBeforeRetract;

            Door door = grabbedObject.GetComponent<Door>();
            door.Interact();

        }
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, BatteryDisplay))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }

            if (hand == rightHand && FlareHandOut)
            {
                return;
            }

            grabbedObject = hit.collider.gameObject;

            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;
            canboost = true;

            retractTimer = delayBeforeRetract;
            grabbedObject.SetActive(false);

            // Get the position of the target GameObject
            Vector3 targetPosition = RightBatteryLocation.transform.position;
            Quaternion targetRotation = RightBatteryLocation.transform.rotation;

            // Instantiate the prefab at the position of the target GameObject
            Instantiate(batteryPreab, targetPosition, targetRotation);

        }
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, Default))
        {
            if (hand == rightHand && FlareHandOut)
            {
                return;
            }

            grabbedObject = hit.collider.gameObject;



            audioPlayer.PlayOneShot(handgrab, 0.7f);


            // Store the original position and rotation of the hand
            if (hand == leftHand)
            {
                leftHandPlaceholder = hand;
                leftHandGrabbed = true;
            }
            else if (hand == rightHand && !FlareHandOut)
            {
                rightHandPlaceholder = hand;
                rightHandGrabbed = true;
            }

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;




            // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
            if (hand == rightHand && grabbedObject.layer == LayerMask.NameToLayer("Default") && !FlareHandOut)
            {

                if (hand == rightHand)
                {
                    canboost = true;

                    retractTimer = delayBeforeRetract;
                }

            }

            // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
            if (hand == leftHand && grabbedObject.layer == LayerMask.NameToLayer("Default"))
            {
                if (hand == leftHand)
                {
                    canboost = true;

                    retractTimer = delayBeforeRetract;
                }


            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, powerSource))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (FlareHandOut)
            {
                return;
            }

            grabbedObject = hit.collider.gameObject;

            audioPlayer.PlayOneShot(handgrab, 0.7f);

            // Store the original position and rotation of the hand
            if (!FlareHandOut)
            {
                if (hand == leftHand)
                {
                    leftHandPlaceholder = hand;
                    leftHandGrabbed = true;
                    powerpuzzlehand = "Left";
                }
                else if (hand == rightHand)
                {
                    rightHandPlaceholder = hand;
                    rightHandGrabbed = true;
                    powerpuzzlehand = "Right";

                }
            }


            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;

            powerpuzzle = true;


        }

        if (Physics.Raycast(hand.position, hand.forward, out hit, maxDistance, powerReciever))
        {
            if (!StartWithGreenHand)
            {
                if (hand == rightHand)
                {
                    if (greenhandout)
                    {
                        return;
                    }
                }

            }
            if (FlareHandOut)
            {
                if (hand == rightHand)
                {
                    return;
                }
            }

            grabbedObject = hit.collider.gameObject;

            audioPlayer.PlayOneShot(handgrab, 0.7f);

            PowerPuzzle power;
            power = grabbedObject.GetComponent<PowerPuzzle>();

            power.opendoor();

            // Set the hand's position and rotation to the hit point and object's rotation smoothly
            StartCoroutine(SmoothMove(hand, hit.point, hit.collider.transform.rotation));

            // Make the hand a child of the grabbed object
            hand.parent = grabbedObject.transform;


            // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
            if (hand == rightHand && grabbedObject.layer == LayerMask.NameToLayer("PowerReciever"))
            {

                if (hand == rightHand)
                {
                    canboost = true;

                    retractTimer = delayBeforeRetract;

                    powerpuzzle = false;
                }

            }

            // Check if the object is on the "PurplehandScanner" layer and if the right hand grabbed it
            if (hand == leftHand && grabbedObject.layer == LayerMask.NameToLayer("PowerReciever"))
            {
                if (hand == leftHand)
                {
                    canboost = true;

                    retractTimer = delayBeforeRetract;

                    powerpuzzle = false;

                }


            }
        }

    }


    void UnparentHand()
    {
        if (leftHandGrabbed)
        {
            leftHand.parent = null;
            leftHandGrabbed = false;


            powerpuzzle = false;

        }

        if (rightHandGrabbed)
        {
            rightHand.parent = null;
            rightHandGrabbed = false;


            powerpuzzle = false;

        }
    }


    void ReleaseObject(Transform hand, ref GameObject grabbedObject, Transform retractParent, LineRenderer cableRenderer, Transform cableHolder)
    {
        // Unparent the hand immediately
        hand.parent = retractParent;
        audioPlayer.PlayOneShot(handretract, 0.5f);




        grabbedObject = null;

        if (hand == leftHand)
        {
            leftHandGrabbed = false;

            if (isscanningblue == true)
            {
                isscanningblue = false;


                foreach (GameObject obj in BlueHandScanners)
                {
                    if (obj != null) // Check if the object is not null
                    {
                        obj.SetActive(false); // Deactivate the object
                        obj.SetActive(true); // Deactivate the object

                    }
                }

            }
        }
        else if (hand == rightHand)
        {
            rightHandGrabbed = false;
        }
    }

    IEnumerator SmoothMoveAndParentBack(Transform hand, Vector3 targetPosition, Quaternion targetRotation, Transform retractParent)
    {
        float elapsedTime = 0f;

        while (elapsedTime < smoothMoveDuration)
        {
            hand.position = Vector3.Lerp(hand.position, targetPosition, elapsedTime / smoothMoveDuration);
            hand.rotation = Quaternion.Slerp(hand.rotation, targetRotation, elapsedTime / smoothMoveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position and rotation are correct
        hand.position = targetPosition;
        hand.rotation = targetRotation;

        // Parent the hand back after the smooth move
        hand.parent = retractParent;
    }



    private IEnumerator SmoothMove(Transform hand, Vector3 targetPosition, Quaternion targetRotation)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(hand.position, targetPosition);

        while (Time.time < startTime + smoothMoveDuration)
        {
            float fraction = Mathf.Clamp01((Time.time - startTime) / smoothMoveDuration);
            hand.position = Vector3.Lerp(hand.position, targetPosition, fraction);
            //hand.rotation = Quaternion.Slerp(hand.rotation, targetRotation, fraction);
            yield return null;
        }

        hand.position = targetPosition;
       // hand.rotation = targetRotation;
    }

    void DragObject(Transform hand, GameObject grabbedObject)
    {
        delayDrag -= Time.deltaTime;

        if (delayDrag <= 0)
        {
            if (grabbedObject != null)
            {
                if (grabbedObject.layer != LayerMask.NameToLayer("PurpleHandScanner") && grabbedObject.layer != LayerMask.NameToLayer("Handle") && grabbedObject.layer != LayerMask.NameToLayer("Default") && grabbedObject.layer != LayerMask.NameToLayer("GreenPowerSupply") && grabbedObject.layer != LayerMask.NameToLayer("GreenPowerReciever") && grabbedObject.layer != LayerMask.NameToLayer("BlueHandScanner") && grabbedObject.layer != LayerMask.NameToLayer("PowerSource") && grabbedObject.layer != LayerMask.NameToLayer("DashHandle") && grabbedObject.layer != LayerMask.NameToLayer("Cutout") && grabbedObject.layer != LayerMask.NameToLayer("Door") && grabbedObject.layer != LayerMask.NameToLayer("PowerReciever"))
                {
                    if (hand == leftHand)
                    {
                        // Calculate the direction from the object to the player's position
                        Vector3 directionToPlayer = (transform.position - grabbedObject.transform.position).normalized;

                        // Move the object towards the player
                        grabbedObject.transform.position += directionToPlayer * dragSpeed * Time.deltaTime;

                        hasbeengrabbed = true;
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            LeftSounds.SetActive(true);

                        }
  
                    }


                    if (hand == rightHand)
                    {
                        if (righthandable == true)
                        {
                            // Calculate the direction from the object to the player's position
                            Vector3 directionToPlayer = (transform.position - grabbedObject.transform.position).normalized;

                            // Move the object towards the player
                            grabbedObject.transform.position += directionToPlayer * dragSpeed * Time.deltaTime;

                            hasbeengrabbed = true;


                            if (Input.GetKey(KeyCode.Mouse1))
                            {
                                RightSounds.SetActive(true);

                            }
                            

                        }
                    }
                }


                if (Input.GetKey(KeyCode.Mouse0) && grabbedObject.layer == LayerMask.NameToLayer("Handle"))
                {

                    if (greenhandout == true || purplehandout == true || FlareHandOut == true)
                    {
                        Vector3 playerToHandle = (grabbedObject.transform.position - transform.position).normalized;

                        // Check if the player is below the handle

                        


                        if (hasbeengrabbed == false)
                        {
                            rigidbodyObject.MovePosition(rigidbodyObject.position + playerToHandle * dragSpeed1 * Time.deltaTime);

                        }
                        else
                        {
                            pullingPlayer = false;
                        }
                    }


                }

                if (Input.GetKey(KeyCode.Mouse1) && grabbedObject.layer == LayerMask.NameToLayer("Handle"))
                {
                    if (greenhandout == true || purplehandout == true || FlareHandOut == true)
                    {
                        Vector3 playerToHandle = (grabbedObject.transform.position - transform.position).normalized;




                        if (hasbeengrabbed == false)
                        {
                            rigidbodyObject.MovePosition(rigidbodyObject.position + playerToHandle * dragSpeed1 * Time.deltaTime);

                        }
                    }
                }



                //--------------------------------------------------------------------------------------------------------------------------------------------------

                bool usingrhand = false;
                bool usinglhand = false;


                if (Input.GetKey(KeyCode.Mouse0) && grabbedObject.layer == LayerMask.NameToLayer("DashHandle"))
                {

                    if (greenhandout == true || purplehandout == true || FlareHandOut == true)
                    {
                        Vector3 playerToHandle = (grabbedObject.transform.position - transform.position).normalized;
                        usinglhand = true;


                        if (!releasedOnce)
                        {

                            hasbeengrabbed = true;

                            // If the player is above the handle and hasn't been released yet, release the object
                            ReleaseObject(hand, ref grabbedObject, null, null, null);
                            releasedOnce = true;
                            audioPlayer.PlayOneShot(handretract, 1.0f);
                            hasbeengrabbed = false;

                        }


                        if (hasbeengrabbed == false)
                        {
                            if (usinglhand && !usingrhand)
                            {
                                dashspeed = 18;
                            }
                            if (!usinglhand && usingrhand)
                            {
                                dashspeed = 18;
                            }
                            if (usinglhand && usingrhand)
                            {
                                dashspeed = 1;
                            }
                            rigidbodyObject.MovePosition(rigidbodyObject.position + playerToHandle * dashspeed * Time.deltaTime);
                        }
                    }


                }

                if (Input.GetKey(KeyCode.Mouse1) && grabbedObject.layer == LayerMask.NameToLayer("DashHandle"))
                {
                    if (greenhandout == true || purplehandout == true)
                    {
                        Vector3 playerToHandle = (grabbedObject.transform.position - transform.position).normalized;
                        usingrhand = true;


                        if (!releasedOnce)
                        {

                            hasbeengrabbed = true;

                            // If the player is above the handle and hasn't been released yet, release the object
                            ReleaseObject(hand, ref grabbedObject, null, null, null);
                            releasedOnce = true;
                            hasbeengrabbed = false;
                            audioPlayer.PlayOneShot(handretract, 1.0f);
                        }


                        if (hasbeengrabbed == false)
                        {
                            if (usinglhand && !usingrhand)
                            {
                                dashspeed = 18;
                            }
                            if (!usinglhand && usingrhand)
                            {
                                dashspeed = 18;
                            }
                            if (usinglhand && usingrhand)
                            {
                                dashspeed = 1;
                            }
                            rigidbodyObject.MovePosition(rigidbodyObject.position + playerToHandle * dashspeed * Time.deltaTime);
                            draggingsounds.SetActive(true);

                        }
                    }
                }

            }
        }


    }

    void MoveHandToHitPosition(Transform hand, GameObject grabbedObject)
    {
        // No need to move the hand while grabbed
    }


}



















