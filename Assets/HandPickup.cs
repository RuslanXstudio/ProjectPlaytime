using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPickup : MonoBehaviour
{

    public GrabPack grabpack;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup()
    {
        gameObject.SetActive(false);
        grabpack.StartWithGreenHand = true;
        grabpack.GreenHandObject.enabled = true;

        grabpack.GreenHandPickUp();
    }
}
