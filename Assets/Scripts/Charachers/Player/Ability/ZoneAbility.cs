using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZoneAbility : MonoBehaviour
{
    private SpriteRenderer _sprite;

    private bool _isActive = false;

    private List<Collider2D> _collidersEnemy;

    private float _valueDamagedEnemy = 0f;

    private void Awake()
    {
        _collidersEnemy = new List<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (_sprite.enabled != false)
            _sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_collidersEnemy.Contains(collision))
        {
            _collidersEnemy.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collidersEnemy.Contains(collision))
        {
            _collidersEnemy.Remove(collision);
        }
    }

    public void ApplyDamage(float point)
    {
        foreach (Collider2D enemy in _collidersEnemy)
        {
            if(enemy!= null && enemy.TryGetComponent(out Health health))
            {
                health.TakeDamage(point);

                _valueDamagedEnemy ++;
            }
        }
    }

    public float GetDamagedTargetsCount()
    {
        float value = _valueDamagedEnemy;

        _valueDamagedEnemy = 0f;

        return value;
    }

    public void ToggleVisibility()
    {
        if (_isActive)
        {
            _sprite.enabled = false;
            _isActive = false;
        }
        else
        {
            _sprite.enabled = true;
            _isActive = true;
        }
    }
}
