using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPower : MonoBehaviour
{
    public GrabPack grabpack;
    public Animator DoorToActivate;
    public GameObject BoostPadToActivate;

    public Animator greenelectrcity;

    public AudioSource audiosource;
    public AudioClip ElectricityDischarge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabpack.greenhandout == true && grabpack.HasGreenPower == true)
        {
            if (transform.childCount > 1)
            {
                // The GameObject has Power
                Debug.Log("Power");
                if (DoorToActivate != null)
                {
                    DoorToActivate.SetTrigger("Open");
                }

                if (BoostPadToActivate != null)
                {
                    BoostPadToActivate.SetActive(true);
                }

                greenelectrcity.SetBool("Start", true);

            }
            else
            {
                // The GameObject has no Power
                Debug.Log("No Power");
            }
        }

    }

    public void EndElectricity()
    {
        greenelectrcity.SetBool("Start", false);
        BoostPadToActivate.SetActive(false);
        DoorToActivate.ResetTrigger("Open");
        audiosource.PlayOneShot(ElectricityDischarge, 1f);
    }

}
