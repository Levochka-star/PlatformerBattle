using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBar : HealthView
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    protected override void UpdateValue(float current)
    {
        if (_textMeshPro != null)
            _textMeshPro.text = ($"{current}/{_maxFillPoint}");
    }
}
