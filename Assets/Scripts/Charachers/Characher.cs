using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(CharacherDetector))]
[RequireComponent(typeof(Damager))]
public class Characher : MonoBehaviour
{
    protected Mover Mover;
    protected Jumper Jumper;
    protected Rotator Rotator;
    protected GroundDetector GroundDetector;
    protected CharacherDetector CharaterDetector;
    protected Damager Damager;

    protected virtual void OnJump()
    {
        Jumper.Jump();
    }

    protected virtual void TryAttack(Collision2D collision) { }
}
