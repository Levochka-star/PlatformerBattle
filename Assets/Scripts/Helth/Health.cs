using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealable, IDamageble
{
    [SerializeField] private bool _canHealling = false;
    [SerializeField] private float _currentHealth = 100;
    [Tooltip("If the value is not changed, the maximum health points will be assigned the value of the current health points.")]
    [SerializeField] private float _maxHealth = 0;

    public event Action<float> ChangedHealthPoint;

    public bool CanHealling => _canHealling;

    public float MaxHealth => _maxHealth;

    private float _minHealth = 0;

    private void Awake()
    {
        if (_maxHealth <= 0)
        {
            _maxHealth = _currentHealth;
        }
    }
    private void Start()
    {
        ChangedHealthPoint?.Invoke(_currentHealth);
    }

    public void Heal(float amout)
    {
        if (_canHealling)
            ApplyHeal(amout);
    }

    public bool TryHealling()
    {
        return CanHealling;
    }

    public void TakeDamage(float amout)
    {
        ApplyHeal(amout, true);
    }

    private void ApplyHeal(float amout, bool isDamage = false)
    {
        float health = _currentHealth;

        if (isDamage)
        {
            health -= amout;
        }
        else
        {
            health += amout;
        }

        if (isDamage)
        {
            if (health <= _minHealth)
            {
                _currentHealth = _minHealth;
            }
            else if (health > _minHealth)
            {
                _currentHealth = health;
            }
        }
        else
        {
            if (health <= _maxHealth)
            {
                _currentHealth = health;
            }
            else if (health > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        ChangedHealthPoint?.Invoke(_currentHealth);
    }
}
