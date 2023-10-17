using UnityEngine;

namespace Runtime
{
    public class StartAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();

            if (animator)
            {
                // Play the default animation.
                // If you want to play a specific animation, you can use:
                // _animator.Play("YourAnimationStateName");
                animator.Play(0);
            }
        }
    }
}