using System.Collections;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damageForHit;
    [SerializeField, Range(1f, 100f)] private float _hitForSecond = 1;

    private Coroutine _WaitReload = null;
    private float _delayHit;

    private void Awake()
    {
        float second = 1f;

        _delayHit = second/_hitForSecond;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageble damageble))
        {
            if (_WaitReload == null)
            {
                damageble.TakeDamage(_damageForHit);
                StartWaitDamage();
            }
        }
    }

    private void StartWaitDamage()
    {
        if (_WaitReload != null)
        {
            StopCoroutine(_WaitReload);
        }

        _WaitReload = StartCoroutine(WaitSecond());
    }

    private void StopWaitDamage()
    {
        if (_WaitReload != null)
        {
            StopCoroutine(_WaitReload);

            _WaitReload = null;
        }
    }

    private IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(_delayHit);

        StopWaitDamage();
    }
}