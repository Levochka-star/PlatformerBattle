using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private float _maxDeltaSpeed = 0.3f;

    private Slider _smoothHealthSlider;

    private Coroutine _waitingMoveToward;

    private void Awake()
    {
        float maxValue = 1;
        _smoothHealthSlider = GetComponent<Slider>();
        _smoothHealthSlider.value = maxValue;
    }

    private void OnDisable()
    {
        StopCoroutine(_waitingMoveToward);
        _waitingMoveToward = null;
    }

    private void StartMoveSlider(float healthPoint)
    {
        if (_waitingMoveToward != null)
        {
            StopCoroutine(_waitingMoveToward);
            _waitingMoveToward = null;
        }

        _waitingMoveToward = StartCoroutine(WaitMoveToward(healthPoint));
    }

    public void SetHealthPoint(float healthPoint, float maxHealt)
    {
        StartMoveSlider(healthPoint / maxHealt);
    }

    private IEnumerator WaitMoveToward(float healthPoint)
    {
        float threshold = 0.01f;
        float deltaSpeed = _maxDeltaSpeed * Time.deltaTime;

        while (!Mathf.Approximately(_smoothHealthSlider.value, healthPoint))
        {
            _smoothHealthSlider.value = Mathf.MoveTowards(_smoothHealthSlider.value, healthPoint, deltaSpeed);

            yield return null;
        }

        if (_smoothHealthSlider.value < threshold)
        {
            _smoothHealthSlider.gameObject.SetActive(false);
        }
    }
}
