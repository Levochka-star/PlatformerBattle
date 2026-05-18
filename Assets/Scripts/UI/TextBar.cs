using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBar : BaseHealthBar
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    protected override void HealthView(float current)
    {
        if (_textMeshPro != null)
            _textMeshPro.text = ($"{current}/{_maxFillPoint}");
    }
}
