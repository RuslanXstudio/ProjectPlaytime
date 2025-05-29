using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToDoor : MonoBehaviour
{
    public GameObject Door;
    private Animator dooranimator;

    // Start is called before the first frame update
    void Start()
    {
        dooranimator = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void opendoor()
    {
        dooranimator.SetTrigger("Open");
    }
}
