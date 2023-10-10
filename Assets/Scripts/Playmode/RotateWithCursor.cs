using UnityEngine;

namespace Playmode
{
    public class RotateWithCursor : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Update()
        {
            RotateTowardsCursor();
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
