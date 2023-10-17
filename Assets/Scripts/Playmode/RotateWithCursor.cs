using UnityEngine;

namespace Playmode
{
    public class RotateWithCursor : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        private bool _cursorIsConfined;

        private void Update()
        {
            HandleMouseLock();

            // Only rotate the object if the cursor is confined
            if (_cursorIsConfined)
            {
                RotateTowardsCursor();
            }
        }

        private void HandleMouseLock()
        {
            // If the user presses the left mouse button, confine the cursor to the game window
            if (Input.GetMouseButtonDown(0) && !_cursorIsConfined)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;  // Ensure the cursor remains visible
                _cursorIsConfined = true;
            }

            // If the user presses the Escape key, release the cursor
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                _cursorIsConfined = false;
            }
        }

        private void RotateTowardsCursor()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, transform.position);

            if (!plane.Raycast(ray, out var enter)) return;

            var hitPoint = ray.GetPoint(enter);
            var direction = hitPoint - transform.position;

            // Make sure to not tilt in the X and Z axis
            direction.y = 0;

            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}