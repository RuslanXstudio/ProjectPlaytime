using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayerInAir : MonoBehaviour
{
    public float forceAmount = 10f;
    public GrabPack grabpackscript;
    public Rigidbody PlayerRigidBody;

    public BoxCollider SelfCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabpackscript.pullingPlayer == true)
        {
            SelfCollider.enabled = true;
        }
        if (grabpackscript.pullingPlayer == false)
        {
            SelfCollider.enabled = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {
            PlayerRigidBody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);

        }
    }
}
