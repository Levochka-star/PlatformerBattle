using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthTextBar : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void SetHealthPoint(float healthPoint, float minHealt, float maxHealt)
    {
        string textHealth = $"{healthPoint}/{maxHealt}";

        _textMeshPro.text = textHealth;

        if (healthPoint == minHealt)
        {
            _textMeshPro.gameObject.SetActive(false);
        }
    }
}
