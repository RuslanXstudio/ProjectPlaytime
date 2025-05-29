using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public static int lastcheckpoint = -1;
    public GameObject playerObject;

    public List<Transform> checkpointsList; // List of teleport points

    // Start is called before the first frame update
    void Start()
    {
        TeleportPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportPlayer()
    {
        if (lastcheckpoint <= -1f)
        {
            return;
        }

        // Check if the index is within the bounds of the list
        if (lastcheckpoint >= 0 && lastcheckpoint < checkpointsList.Count)
        {
            // Teleport the player to the position of the teleport point at the specified index
            playerObject.transform.position = checkpointsList[lastcheckpoint].position;
        }
        else
        {
            Debug.LogWarning("Index out of range!");
        }


    }
}
