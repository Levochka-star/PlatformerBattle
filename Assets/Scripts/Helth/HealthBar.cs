using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;

    private Image _healthBar;

    private float _minHealthPoint = 0f;

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
    }

    public void SetHealthPoint(float healthPoint, float maxHealt)
    {
        _healthBar.fillAmount = healthPoint/maxHealt;

        if(_healthBar.fillAmount == _minHealthPoint)
        {
            _parentCanvas.gameObject.SetActive(false);
        }
    }
}
