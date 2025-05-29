using UnityEngine;
using System.Collections.Generic;

public class LineRendererManager : MonoBehaviour
{
    public LineRendererScript lineRendererScript; // Reference to the LineRendererScript component
    public string objectNameToRemove;



    // Method to remove all points added since the start of the game
    public void RemovePointsSinceStart()
    {
        if (lineRendererScript != null)
        {
            lineRendererScript.RemovePointsSinceStart(objectNameToRemove);

        }
    }
}
