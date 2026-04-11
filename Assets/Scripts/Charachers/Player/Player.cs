using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(AnimationPlayerSwitch))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    private Mover _mover;
    private Jumper _jumper;
    private Rotator _rotator;
    private GroundDetector _groundDetector;
    private AnimationPlayerSwitch _animationSwitch;

    private bool _isRun;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _rotator = GetComponent<Rotator>();
        _groundDetector = GetComponent<GroundDetector>();
        _animationSwitch = GetComponent<AnimationPlayerSwitch>();
    }

    private void OnEnable()
    {
        _inputReader.HorizontalIsRunStarted += SetRun;
        _inputReader.HorizontalMovementStarted += OnMove;
        _inputReader.VertiсalMovementStarted += OnJump;
    }

    private void OnDisable()
    {
        _inputReader.HorizontalIsRunStarted -= SetRun;
        _inputReader.HorizontalMovementStarted -= OnMove;
        _inputReader.VertiсalMovementStarted -= OnJump;
    }

    private void OnMove(float vectorX)
    {
        OnAnimation(_isRun, vectorX);

        _rotator.Rotation(vectorX);
        _mover.Move(_isRun, vectorX, _groundDetector.IsGrounded());
    }

    private void OnJump()
    {
        if (_groundDetector.IsGrounded())
        {
            bool isJump = true;
            OnAnimation(isJump);

            _jumper.Jump();
        }
    }

    private void OnAnimation(bool isRun, float vectorX)
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

    private void OnAnimation(bool isJump)
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
}