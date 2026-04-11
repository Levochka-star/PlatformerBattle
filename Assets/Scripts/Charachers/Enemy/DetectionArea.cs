using System;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public event Action PlayerDetected;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            PlayerDetected?.Invoke();
    }
}
