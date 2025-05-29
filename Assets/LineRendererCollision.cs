using System.Collections.Generic;
using UnityEngine;

public class LineRendererCollision : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject startPointObject; // Reference to the start point object
    public GameObject endPointObject; // Reference to the end point object
    public LayerMask collisionMask;
    private List<Vector3> collisionPoints = new List<Vector3>();

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (startPointObject == null || endPointObject == null)
        {
            Debug.LogError("Start and End point objects must be assigned.");
        }
    }

    void Update()
    {
        DetectCollisionsAndAddPoints();
        UpdateLineRenderer();
    }

    void DetectCollisionsAndAddPoints()
    {
        collisionPoints.Clear();

        Vector3 startPoint = startPointObject.transform.position;
        Vector3 endPoint = endPointObject.transform.position;

        collisionPoints.Add(startPoint);

        RaycastHit hit;
        if (Physics.Raycast(startPoint, (endPoint - startPoint).normalized, out hit, Vector3.Distance(startPoint, endPoint), collisionMask))
        {
            collisionPoints.Add(hit.point);
        }

        collisionPoints.Add(endPoint);
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = collisionPoints.Count;
        lineRenderer.SetPositions(collisionPoints.ToArray());
    }
}
