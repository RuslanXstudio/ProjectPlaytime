using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableRenderer : MonoBehaviour
{
    public GameObject targetObject;  // The target object you want to draw the line to
    public Material lineMaterial;    // Material for the line
    public float lineWidth = 0.1f;   // Width of the line
    public float curvehieght = 0.5f;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get or add a LineRenderer component to the object this script is attached to
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Initialize the line renderer properties
        lineRenderer.positionCount = 5;  // Line consists of two points
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Set the material if provided
        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            Vector3 startPosition = transform.position;

            Vector3 endPosition = targetObject.transform.position;

            Vector3 middlePosition = (startPosition + endPosition) / 2;
            middlePosition.y -= curvehieght * 1.5f;

            Vector3 secondmid = (middlePosition + endPosition) / 2;
            secondmid.y -= curvehieght * 0.5f;


            Vector3 firstmid = (startPosition + middlePosition) / 2;
            firstmid.y -= curvehieght * 0.5f;

            // Set the start and end points of the line (from this object to the target object)
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, firstmid);
            lineRenderer.SetPosition(2, middlePosition);
            lineRenderer.SetPosition(3, secondmid);
            lineRenderer.SetPosition(4, endPosition);

        }
    }
}