using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;

    private Rigidbody2D _rigidbody2D;

    private Vector2 _target;
    private float _airSpeedDivider = 4f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, _target, Time.deltaTime);
    }

    public void Move(bool isRun, float vectorX, bool isGroinded)
    {
        float speedMove = GetSpeedMove(isRun);

        if (isGroinded)
        {
            _target = new Vector2(vectorX * speedMove, _rigidbody2D.velocity.y);
        }
        else
        {
            _target = new Vector2((vectorX * speedMove) / _airSpeedDivider, _rigidbody2D.velocity.y);
        }
    }

    private float GetSpeedMove(bool isRun)
    {
        if (isRun)
        {
            return _speedRun;
        }
        else
        {
            return _speedWalk;
        }
    }
}