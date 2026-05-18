using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealable, IDamageble
{
    [SerializeField] private float _currentHealth = 100;
    [Tooltip("If the value is not changed, the maximum health points will be assigned the value of the current health points.")]
    [SerializeField] private float _maxHealth = 0;

    public event Action<float> ChangedHealthPoint;

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
        if (amout >= 0)
            _currentHealth = Mathf.Clamp(_currentHealth + amout, _minHealth, _maxHealth);

        ChangedHealthPoint?.Invoke(_currentHealth);
    }

    public bool NeedsHealing()
    {
        return _currentHealth < _maxHealth;
    }

    public void TakeDamage(float amout)
    {
        if (amout >= 0)
            _currentHealth = Mathf.Clamp(_currentHealth - amout, _minHealth, _maxHealth);

        ChangedHealthPoint?.Invoke(_currentHealth);
    }
}
