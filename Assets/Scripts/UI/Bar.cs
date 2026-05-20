using UnityEngine;
using UnityEngine.UI;

public class Bar : BaseBar
{
    [SerializeField] private Slider _slider;

    protected override void UpdateValue(float current)
    {
        _currentFillPercent = current / _maxFillPoint;

        if (_slider != null)
            _slider.value = _currentFillPercent;
    }
}