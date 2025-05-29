using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwitcher : MonoBehaviour
{
    public GameObject Hand_1;
    public GameObject Hand_2;
    public GameObject Hand_3;

    public Animator GrabPackanimator;
    public float switchdelay = 0.9f;
    public bool isswitching = false;
    public int handtoswitchto;
    public float postdelay = 0f;
    public bool startpostdelay;

    public GrabPack grabpack;

    public AudioSource audiosource;
    public AudioClip HandSwitchSound;

    void Start()
    {
        DisableAllObjects();

        Hand_1.SetActive(true);
    }

    void Update()
    {



        if (grabpack.rightHandGrabbed == false)
        {

            if (grabpack.holdingBattery == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (postdelay <= 0)
                    {

                        GrabPackanimator.SetTrigger("switch");
                        isswitching = true;
                        handtoswitchto = 1;
                        audiosource.PlayOneShot(HandSwitchSound, 2.5f);
                    }

                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (postdelay <= 0)
                    {

                        GrabPackanimator.SetTrigger("switch");
                        isswitching = true;
                        handtoswitchto = 2;
                        audiosource.PlayOneShot(HandSwitchSound, 2.5f);

                    }




                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (postdelay <= 0)
                    {

                        GrabPackanimator.SetTrigger("switch");
                        isswitching = true;
                        handtoswitchto = 3;
                        audiosource.PlayOneShot(HandSwitchSound, 2.5f);


                    }



                }


                if (isswitching)
                {
                    postdelay = 1f;

                    startpostdelay = true;

                    switchdelay -= Time.deltaTime;

                    if (switchdelay <= 0)
                    {
                        if (handtoswitchto == 1)
                        {
                            StartCoroutine(SwitchObjectsAndMove(Hand_1));
                        }
                        if (handtoswitchto == 2)
                        {
                            StartCoroutine(SwitchObjectsAndMove(Hand_2));
                        }
                        if (handtoswitchto == 3)
                        {
                            StartCoroutine(SwitchObjectsAndMove(Hand_3));
                        }

                        switchdelay = 0.9f;

                        isswitching = false;
                    }


                }
            }
            
        }





        if (startpostdelay)
        {
            postdelay -= Time.deltaTime;

            if (postdelay <= 0)
            {
                startpostdelay = false;
            }
        }

    }

    IEnumerator SwitchObjectsAndMove(GameObject activeObject)
    {



        // Disable all objects
        DisableAllObjects();

        // Enable the specified object


        activeObject.SetActive(true);



        yield return null;



    }

    void DisableAllObjects()
    {
        Hand_1.SetActive(false);
        Hand_2.SetActive(false);
        Hand_3.SetActive(false);
    }
}