using UnityEngine;
using System.Collections.Generic;

public class LineRendererScript : MonoBehaviour
{
    public List<GameObject> linePoints = new List<GameObject>(); // List to hold the points of the line
    private LineRenderer lineRenderer;

    // Material for the LineRenderer
    public Material lineMaterial;

    private int initialPointsCount = 0; // Count of points at the start of the game

    public string objectNameToReplace = "ogposleft"; // The name of the object to replace
    public GameObject righthand; // The object you want to use as replacement
    public GrabPack grabpack;

    public string objectNameToReplace1 = "ogposright"; // The name of the object to replace
    public GameObject lefthand; // The object you want to use as replacement

    void Start()
    {
        // Initialize LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f; // Set the start width of the line
        lineRenderer.endWidth = 0.05f; // Set the end width of the line

        // Assign the provided material to the LineRenderer
        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
        else
        {
            Debug.LogError("Please assign a material to the LineRenderer.");
        }

        // Record the initial count of points
        initialPointsCount = linePoints.Count;
    }

    void Update()
    {
        GameObject objectToReplace = GameObject.Find(objectNameToReplace);
        GameObject objectToReplace1 = GameObject.Find(objectNameToReplace1);

        // Update line positions every frame
        UpdateLinePositions();
        if (grabpack.powerpuzzlehand == "Right")
        {
            int index = linePoints.IndexOf(objectToReplace);
            if (index != -1)
            {
                // Replace the object in the list with the replacement object
                linePoints[index] = righthand;
                Debug.Log("Object '" + objectNameToReplace + "' replaced in the list.");
            }
        }

        if (grabpack.powerpuzzlehand == "Left")
        {
            int index = linePoints.IndexOf(objectToReplace1);
            if (index != -1)
            {
                // Replace the object in the list with the replacement object
                linePoints[index] = lefthand;
                Debug.Log("Object '" + objectNameToReplace1 + "' replaced in the list.");
            }
        }
    }

    // Function to update the positions of the line points
    private void UpdateLinePositions()
    {
        lineRenderer.positionCount = linePoints.Count;
        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i].transform.position);
        }
    }

    // Method to add a new point to the linePoints list at a specific index
    public void AddPoint(GameObject point, int index)
    {
        if (index >= 0 && index <= linePoints.Count)
        {
            if (grabpack.powerpuzzle == true)
            {
                linePoints.Insert(index, point);
                UpdateLinePositions(); // Update the line renderer to reflect the change
            }
        }
        else
        {
            Debug.LogError("Index out of range.");
        }
    }

    // Method to remove all points with a specific object name
    public void RemovePointsSinceStart(string objectName)
    {
        for (int i = linePoints.Count - 1; i >= 0; i--)
        {
            if (linePoints[i].name == objectName)
            {
                linePoints.RemoveAt(i);
            }
        }
    }
}
