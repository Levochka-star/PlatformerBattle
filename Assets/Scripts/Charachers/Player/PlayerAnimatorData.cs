using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Parametrs
    {
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
        public static readonly int IsRuning = Animator.StringToHash(nameof(IsRuning));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    }
}