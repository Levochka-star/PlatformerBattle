using UnityEngine;

public class AnimationZombeSwitch : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void OnPlayerRun()
    {
        Run(true);
    }

    public void OffPlayerRun()
    {
        Run(false);
    }

    private void Run(bool isRuning)
    {
        _animator.SetBool(PlayerAnimatorData.Parametrs.IsRuning, isRuning);
    }
}
