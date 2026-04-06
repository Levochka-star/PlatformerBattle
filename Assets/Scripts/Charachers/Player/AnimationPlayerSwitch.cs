using UnityEngine;

public class AnimationPlayerSwitch : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void OnPlayerWalk()
    {
        Walk(true);
    }

    public void OnPlayerRun()
    {
        Run(true);
    }

    public void OffPlayerWalk()
    {
        Walk(false);
    }

    public void OffPlayerRun()
    {
        Run(false);
    }

    public void OnPlayerJump()
    {
        Jump();
    }

    private void Walk(bool isWalking)
    {
        _animator.SetBool(PlayerAnimatorData.Parametrs.IsWalking, isWalking);
    }

    private void Run(bool isWalking)
    {
        _animator.SetBool(PlayerAnimatorData.Parametrs.IsRuning, isWalking);
    }

    private void Jump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Parametrs.Jump);
    }
}