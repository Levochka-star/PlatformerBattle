using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _radiusLegs;
    [SerializeField] private LayerMask _layerGround;
    
    public bool IsGrounded()
    {
        float radius = 0.1f;

        return Physics2D.OverlapCircle(_radiusLegs.position, radius, _layerGround);
    }
}