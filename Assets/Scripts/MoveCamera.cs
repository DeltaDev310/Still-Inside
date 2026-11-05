using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform CameraPosition;

    private void Update()
    {
        // Move the camera to the specified position
        transform.position = CameraPosition.position;
        
    }
}
