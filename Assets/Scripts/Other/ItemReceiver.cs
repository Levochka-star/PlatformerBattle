using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ItemReceiver : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Healer healer)&& _health.NeedsHealing())
        {
            _health.Heal(healer.HealPoint);

            healer.Destroy();
        }

        if(collision.TryGetComponent(out Coin coin))
        {
            coin.Destroy();
        }
    }
}
