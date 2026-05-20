using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealable, IDamageble
{
    [SerializeField] private float _currentValue = 100f;
    [Tooltip("If the value is not changed, the maximum health points will be assigned the value of the current health points.")]
    [SerializeField] private float _maxValue = 0f;

    public event Action<float> ChangedHealthPoint;

    public float MaxValue => _maxValue;

    private float _minHealth = 0f;

    private void Awake()
    {
        if (_maxValue <= 0)
        {
            _maxValue = _currentValue;
        }
    }
    private void Start()
    {
        ChangedHealthPoint?.Invoke(_currentValue);
    }

    private void Update()
    {
        TryDead();
    }

    public void Heal(float amout)
    {
        if (amout >= 0)
            _currentValue = Mathf.Clamp(_currentValue + amout, _minHealth, _maxValue);

        ChangedHealthPoint?.Invoke(_currentValue);
    }

    public bool NeedsHealing()
    {
        return _currentValue < _maxValue;
    }

    public void TakeDamage(float amout)
    {
        if (amout >= 0)
            _currentValue = Mathf.Clamp(_currentValue - amout, _minHealth, _maxValue);

        ChangedHealthPoint?.Invoke(_currentValue);
    }

    private void TryDead()
    {
        if (_currentValue == _minHealth)
            gameObject.SetActive(false);
    }
}
