using UnityEngine;

public class LineBal : MonoBehaviour
{
    public LineRenderer lineRenderer; // Drag LineRenderer here
    public Animator animator; // Drag Animator here
    public string animationTriggerName; // Name of the trigger in the Animator

    private bool wasLineRendererEnabledLastFrame = false;

    private void Start()
    {
        if (lineRenderer == null || animator == null)
        {
            Debug.LogError("LineRenderer or Animator not assigned.");
            return;
        }

        UpdateAnimatorState();
    }

    private void Update()
    {
        // Check if LineRenderer's state has changed
        if (lineRenderer.enabled != wasLineRendererEnabledLastFrame)
        {


            // Update animator state based on LineRenderer's new state
            UpdateAnimatorState();
        }

        // Save LineRenderer state for the next frame
        wasLineRendererEnabledLastFrame = lineRenderer.enabled;
    }

    private void UpdateAnimatorState()
    {


        if (lineRenderer.enabled)
        {
            // Set a trigger to ensure the animation plays/restarts
            animator.SetTrigger(animationTriggerName);

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                animator.ResetTrigger(animationTriggerName);

            }
        }
        // No need to disable or reset the animator itself
    }
}
