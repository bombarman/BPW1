using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = GetMouseWorldPosition();

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Ensure that the direction vector is purely horizontal
        direction.y = 0;

        // Create a rotation that looks along the direction vector
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Assign the rotation to the player
        transform.rotation = targetRotation;
    }

    Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Set how far the mouse is into the scene
        mouseScreenPosition.z = Camera.main.transform.position.y;

        // Convert the position to world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }
}
