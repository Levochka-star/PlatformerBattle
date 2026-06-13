using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(CharacherDetector))]
[RequireComponent(typeof(Damager))]
public class Characher : MonoBehaviour
{
    protected Mover _mover;
    protected Jumper _jumper;
    protected Rotator _rotator;
    protected GroundDetector _groundDetector;
    protected CharacherDetector _charaterDetector;
    protected Damager _damager;

    protected virtual void OnJump()
    {
        _jumper.Jump();
    }

    protected virtual void TryAttack(Collision2D collision) { }
}
