using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _healthSlider;

    private void Awake()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetHealthPoint(float healthPoint, float minHealt, float maxHealt)
    {
        _healthSlider.value = healthPoint / maxHealt;

        if (_healthSlider.value == minHealt)
        {
            _healthSlider.gameObject.SetActive(false);
        }
    }
}