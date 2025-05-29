using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerpole : MonoBehaviour
{
    public GameObject endpoint;

    public GrabPack grabpack;

    public GameObject SourceToPole;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (grabpack.powerpuzzle == false)
        {
            endpoint.SetActive(false);
            grabpack.Linerenderer1.SetActive(true);
            grabpack.Linerenderer3.SetActive(true);
            grabpack.Linerenderer2.SetActive(true);
        }

        if (grabpack.powerpuzzle == true)
        {

        }
    }

    void OnTriggerStay(Collider other)
    {


        if (other.tag == ("Player"))
        {
            endpoint.SetActive(true);
            SourceToPole.SetActive(true);
            if (grabpack.powerpuzzlehand == ("Right"))
            {
                grabpack.Linerenderer1.SetActive(false);
                grabpack.Linerenderer3.SetActive(false);

                grabpack.Linerenderer2.SetActive(true);

            }

            if (grabpack.powerpuzzlehand == ("Left"))
            {
                grabpack.Linerenderer2.SetActive(false);


                grabpack.Linerenderer1.SetActive(true);
                grabpack.Linerenderer3.SetActive(true);
            }

        }
    }

}
