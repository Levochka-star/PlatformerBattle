using UnityEngine;

[RequireComponent(typeof(AnimationPlayerSwitch))]
[RequireComponent(typeof(AbilityVampirism))]
public class Player : Characher
{
    [SerializeField] private InputReader _inputReader;
    
    private AnimationPlayerSwitch _animationSwitch;
    private AbilityVampirism _abilityVampirism;

    private bool _isRun;

    private void Awake()
    {
        Mover = GetComponent<Mover>();
        Jumper = GetComponent<Jumper>();
        Rotator = GetComponent<Rotator>();
        GroundDetector = GetComponent<GroundDetector>();
        _animationSwitch = GetComponent<AnimationPlayerSwitch>();
        _abilityVampirism = GetComponent<AbilityVampirism>();
        CharaterDetector = GetComponent<CharacherDetector>();
        Damager = GetComponent<Damager>();
    }

    private void OnEnable()
    {
        _inputReader.HorizontalIsRunStarted += SetRun;
        _inputReader.HorizontalMovementStarted += OnMove;
        _inputReader.VertiсalMovementStarted += OnJump;
        _inputReader.ChangedVampirAbility += ToggleVampirism;
        CharaterDetector.CollisionDetected += TryAttack;
    }

    private void OnDisable()
    {
        _inputReader.HorizontalIsRunStarted -= SetRun;
        _inputReader.HorizontalMovementStarted -= OnMove;
        _inputReader.VertiсalMovementStarted -= OnJump;
        _inputReader.ChangedVampirAbility -= ToggleVampirism;
        CharaterDetector.CollisionDetected -= TryAttack;
    }

    private void OnMove(float vectorX)
    {
        ChangeHorizontalAnimation(_isRun, vectorX);

        Rotator.Rotation(vectorX);
        Mover.Move(_isRun, vectorX, GroundDetector.IsGrounded());
    }

    protected override void OnJump()
    {
        if (GroundDetector.IsGrounded())
        {
            bool isJump = true;
            ChangeVerticalAnimation(isJump);

            base.OnJump();
        }
    }

    private void ChangeHorizontalAnimation(bool isRun, float vectorX)
    {
        vectorX = Mathf.Abs(vectorX);

        if (vectorX > 0f && isRun)
        {
            _animationSwitch.OnPlayerRun();
        }
        else if(vectorX > 0f && isRun != true)
        {
            _animationSwitch.OnPlayerWalk();
            _animationSwitch.OffPlayerRun();
        }
        else if ( vectorX == 0f)
        {
            _animationSwitch.OffPlayerWalk();
            _animationSwitch.OffPlayerRun();
        }
    }

    private void ChangeVerticalAnimation(bool isJump)
    {
        if (isJump)
        {
            _animationSwitch.OnPlayerJump();
        }
    }

    private void SetRun(bool isRun)
    {
        _isRun = isRun;
    }

    private void ToggleVampirism()
    {
        _abilityVampirism.Work();
    }

    protected override void TryAttack(Collision2D collision)
    {
        if(!collision.gameObject.TryGetComponent(out Player player)&& collision.gameObject.TryGetComponent(out IDamageble damageble))
        {
            Damager.Attack(damageble);
        }
    }
}