using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHealthChanger : MonoBehaviour
{
    [SerializeField] private bool _isHeallerButton;
    [SerializeField] private float _healthPointPerClick = 10f;

    [SerializeField] private Health _characterHealth;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(ApplyDamage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ApplyDamage);
    }

    private void ApplyDamage()
    {
        if (_isHeallerButton)
        {
            _characterHealth.Heal(_healthPointPerClick);
        }
        else
        {
            _characterHealth.TakeDamage(_healthPointPerClick);
        }
    }
}
