using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(GroundDetector))]
public class Zombe : MonoBehaviour
{
    private Mover _mover;
    private Rotator _rotator;
    private Patroller _patroller;
    private GroundDetector _groundDetector;

    private float _vectorX;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _patroller = GetComponent<Patroller>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void Start()
    {
        _vectorX = -1f;
    }

    private void Update()
    {
        if (_vectorX == -1f)
        {
            if (SetTaget(_patroller.GetTargetStart()))
            {
                _rotator.LeftRotation();
                return;
            }

            OnMove(_patroller.GetTargetStart(), _vectorX, false);
        }
        else if (_vectorX == 1f)
        {
            if (SetTaget(_patroller.GetTargetEnd()))
            {
                _rotator.RightRotation();
                return;
            }

            OnMove(_patroller.GetTargetEnd(), _vectorX, true);
        }
    }

    private void OnMove(Transform target, float vectorX, bool isArrived)
    {
        bool isRun = false;

        _mover.Move(isRun, vectorX, _groundDetector.IsGrounded());
    }

    private bool SetTaget(Transform target)
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
}