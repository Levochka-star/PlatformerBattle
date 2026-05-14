using UnityEngine;
using UnityEngine.UI;

public class Bar : BaseHealthBar
{
    [SerializeField] private Slider _slider;

    protected override void SetFillBar(float current)
    {
        _currentFillPercent = current / _maxFillPoint;

        if (_slider != null)
            _slider.value = _currentFillPercent;
    }
}