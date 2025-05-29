using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform horizontalRotationTarget; 
    public Transform verticalRotationTarget;   

    public float sensitivity = 100f;         
    public float xRotation = 0f;
    public float zRotation = 0f;               
    public GameObject ExitCanvas;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * -sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

    
        zRotation -= mouseX; 
        zRotation = Mathf.Clamp(zRotation, -90f, 90f); 
        horizontalRotationTarget.localRotation = Quaternion.Euler(-90f, 0f, zRotation);
      
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -31f, 35f); 
        verticalRotationTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExitCanvas.SetActive(true);
        }
    }
}
