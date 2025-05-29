using UnityEngine;

public class Ropee : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3[] ropePositions;  // Array to store the positions of the string points
    private Vector3 currentGrapplePosition;

    public Transform startPoint;  // Point start
    public Transform endPoint;    // PoINT Eend

    public int quality = 10;         // Rope quality (number of segments)
    public float waveCount = 1f;     // Rope waviness control
    public float waveHeight = 0.1f;  // Rope corrugation height
    public float speed = 12f;        // Interpolation speed between positions

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        ropePositions = new Vector3[quality + 1];
        lr.positionCount = quality + 1;
    }

    void LateUpdate()
    {
        DrawRope();

    }

    void DrawRope()
    {
        // Update the current position of the string's end point
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, endPoint.position, Time.deltaTime * speed);

        // Update the positions of the string points
        for (int i = 0; i <= quality; i++)
        {
            float delta = (float)i / quality;
            Vector3 offset = Vector3.up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI);
            Vector3 pointAlongRope = Vector3.Lerp(startPoint.position, currentGrapplePosition, delta) + offset;
            ropePositions[i] = pointAlongRope;
        }

        // Defines the calculated positions in Line Renderer
        lr.SetPositions(ropePositions);
    }
}