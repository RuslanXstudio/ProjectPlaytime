using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMorePuzzlePoles : MonoBehaviour
{
    public GameObject pointToAdd; // Reference to the GameObject you want to add to the linePoints list
    public LineRendererScript lineRendererScript; // Reference to the LineRendererScript component
    public int insertionIndex; // Index at which to insert the new point

    void Start()
    {
        // Assuming you've already assigned the LineRendererScript component to lineRendererScript in the Inspector
       // lineRendererScript.AddPoint(pointToAdd, insertionIndex);
    }

    void OnEnable()
    {
        int listLength = lineRendererScript.linePoints.Count;

        insertionIndex = listLength - 1;

        lineRendererScript.AddPoint(pointToAdd, insertionIndex);
        Debug.Log("Add");
    }

    void Update()
    {

    }
}