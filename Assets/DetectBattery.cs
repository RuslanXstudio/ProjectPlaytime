using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBattery : MonoBehaviour
{
    public Animator dooranimator;
    public GameObject batterydisplay;
    public GrabPack grabpack;

    public bool isoccupied = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!batterydisplay.activeSelf) // Check if batterydisplay is not active
        {
            isoccupied = false;
            dooranimator.SetBool("Open", false);

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isoccupied == false)
        {
            if (other.tag == ("Battery"))
            {
                isoccupied = true;
                dooranimator.SetBool("Open", true);

                other.gameObject.SetActive(false);
                batterydisplay.SetActive(true);

                grabpack.holdingBattery = false;
            }

        }

    }
}
