using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCritters : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        SmilingCritterAI ai;
        ai = other.gameObject.GetComponent<SmilingCritterAI>();

        if (other.tag == ("SmilingCritter"))
        {
            ai.scared();
        }
    }
}
