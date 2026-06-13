using System;
using UnityEngine;

public class CharacherDetector : MonoBehaviour
{
    public Action<Collision2D> CollisionDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageble damageble))
        {
            CollisionDetected?.Invoke(collision);
        }
    }
}
