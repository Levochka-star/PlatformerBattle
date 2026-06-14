using UnityEngine;

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(AnimationZombeSwitch))]
public class Zombe : Enemy
{
    [SerializeField] private PursuitZone _pursuitZone;
    [SerializeField] private DetectionArea _detectionArea;
    [SerializeField] private ObstacleDetector _obstacleDetector;

    private Patroller _patroller;
    private AnimationZombeSwitch _animationZombeSwitch;

    private Transform _targetPursuit;
    private bool _isHaunting = false;
    private bool _isPatrol = true;

    private float _vectorX;
    private float _vectorXRight = 1f;
    private float _vectorXLeft = -1f;

    private void Awake()
    {
        Mover = GetComponent<Mover>();
        Jumper = GetComponent<Jumper>();
        Rotator = GetComponent<Rotator>();
        _patroller = GetComponent<Patroller>();
        GroundDetector = GetComponent<GroundDetector>();
        _animationZombeSwitch = GetComponent<AnimationZombeSwitch>();
        CharaterDetector = GetComponent<CharacherDetector>();
        Damager = GetComponent<Damager>();
    }

    private void OnEnable()
    {
        _pursuitZone.ZombePursiting += SetStalkStatus;
        _pursuitZone.PositionChanged += SetTargetPursuit;
        _detectionArea.PlayerDetected += SetHauntStatus;
        _obstacleDetector.OnStucked += OnJump;
        CharaterDetector.CollisionDetected += TryAttack;
    }

    private void OnDisable()
    {
        _pursuitZone.ZombePursiting -= SetStalkStatus;
        _pursuitZone.PositionChanged -= SetTargetPursuit;
        _detectionArea.PlayerDetected -= SetHauntStatus;
        _obstacleDetector.OnStucked -= OnJump;
        CharaterDetector.CollisionDetected -= TryAttack;
    }

    private void Start()
    {
        _vectorX = -1f;
    }

    private void Update()
    {
        if (_isHaunting == false && _isPatrol == false)
        {
            if (_patroller.GetTargetStart().position.x < transform.position.x)
            {
                _vectorX = -1f;
            }
            else if (_patroller.GetTargetStart().position.x > transform.position.x)
            {
                _vectorX = _vectorXRight;
            }

            _animationZombeSwitch.OffPlayerRun();
            _isPatrol = true;
        }
        else if (_targetPursuit != null && _isHaunting)
        {
            _animationZombeSwitch.OnPlayerRun();
            _isPatrol = false;

            if (_targetPursuit.position.x < transform.position.x)
            {
                Rotator.RightRotation();
                OnMove(_targetPursuit, _vectorXLeft, true);
            }
            else if (_targetPursuit.position.x > transform.position.x)
            {
                Rotator.LeftRotation();
                OnMove(_targetPursuit, _vectorXRight, true);
            }
        }

        if (_vectorX == _vectorXLeft && _isPatrol)
        {
            Rotator.RightRotation();

            if (TryFinishPath(_patroller.GetTargetStart()))
            {
                Rotator.LeftRotation();
                return;
            }

            OnMove(_patroller.GetTargetStart(), _vectorX);
        }
        else if (_vectorX == _vectorXRight && _isPatrol)
        {
            Rotator.LeftRotation();

            if (TryFinishPath(_patroller.GetTargetEnd()))
            {
                Rotator.RightRotation();
                return;
            }

            OnMove(_patroller.GetTargetEnd(), _vectorX);
        }
    }

    private void OnMove(Transform target, float vectorX, bool isRun = false)
    {
        Mover.Move(isRun, vectorX, GroundDetector.IsGrounded());
    }

    private bool TryFinishPath(Transform target)
    {
        if ((transform.position - target.position).sqrMagnitude < 1f)
        {
            if (_vectorX == -1f)
            {
                _vectorX = 1f;
            }
            else
            {
                _vectorX = -1f;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetHauntStatus()
    {
        if (_isPatrol)
        {
            _isHaunting = true;
        }
    }

    private void SetStalkStatus(bool status)
    {
        if (status == false)
        {
            _isHaunting = false;
            _targetPursuit = null;
        }
    }

    private void SetTargetPursuit(Transform target)
    {
        _targetPursuit = target;
    }

    protected override void TryAttack(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Zombe zombe) && collision.gameObject.TryGetComponent(out IDamageble damageble))
        {
            Damager.Attack(damageble);
        }
    }
}