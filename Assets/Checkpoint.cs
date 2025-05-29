using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public int checkpointNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer selfrender;
        selfrender = gameObject.GetComponent<MeshRenderer>();

        selfrender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            CheckpointManager.lastcheckpoint = checkpointNumber;
        }
    }
}
