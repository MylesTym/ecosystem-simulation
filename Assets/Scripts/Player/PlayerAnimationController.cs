using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator on the same object
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed); // This tells the Animator what speed value to use
    }
}
