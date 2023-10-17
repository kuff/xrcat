using UnityEngine;

namespace Playmode
{
    public class RotateWithCursor : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        // Property to expose the cursor state
        public bool CursorIsConfined { get; private set; } = false;

        private void Update()
        {
            HandleMouseLock();

            if (CursorIsConfined)
            {
                RotateTowardsCursor();
            }
        }

        private void HandleMouseLock()
        {
            if (Input.GetMouseButtonDown(0) && !CursorIsConfined)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                CursorIsConfined = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && CursorIsConfined)
            {
                Cursor.lockState = CursorLockMode.None;
                CursorIsConfined = false;
            }
        }

        private void RotateTowardsCursor()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, transform.position);

            if (!plane.Raycast(ray, out var enter)) return;

            var hitPoint = ray.GetPoint(enter);
            var direction = hitPoint - transform.position;
            direction.y = 0;

            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}