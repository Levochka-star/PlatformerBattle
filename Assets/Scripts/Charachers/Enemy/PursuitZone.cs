using System;
using UnityEngine;

public class PursuitZone : MonoBehaviour
{
    public event Action<bool> ZombePursiting;
    public event Action<Transform> PositionChanged;

    private bool _isOutside = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            PositionChanged?.Invoke(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            ZombePursiting?.Invoke(_isOutside);
    }
}

