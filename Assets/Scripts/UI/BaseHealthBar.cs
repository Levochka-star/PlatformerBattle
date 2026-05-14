using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected float _maxFillPoint => _health.MaxHealth;
    
    protected float _currentFillPercent;

    private void Awake()
    {
        _health.ChangedHealthPoint += SetFillBar;
    }

    private void OnDestroy()
    {
        _health.ChangedHealthPoint -= SetFillBar;
    }

    protected virtual void SetFillBar(float current)
    {
        _currentFillPercent = current / _maxFillPoint;
    }
}
