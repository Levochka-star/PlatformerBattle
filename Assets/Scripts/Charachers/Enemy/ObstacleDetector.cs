using System;
using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    public event Action OnStucked;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Ground>())
            OnStucked?.Invoke();
    }
}
