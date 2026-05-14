using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothBar : BaseHealthBar
{
    [SerializeField] private float _maxDeltaSpeed = 0.3f;

    [SerializeField] private Slider _smoothSlider;

    private Coroutine _waitingMoveToward;

    private void OnDisable()
    {
        if (_waitingMoveToward != null)
        {
            StopCoroutine(_waitingMoveToward);
            _waitingMoveToward = null;
        }
    }

    private void StartMoveSlider(float fillPoint)
    {
        if (_waitingMoveToward != null)
        {
            StopCoroutine(_waitingMoveToward);
            _waitingMoveToward = null;
        }

        _waitingMoveToward = StartCoroutine(WaitMoveToward(fillPoint));
    }

    protected override void SetFillBar(float current)
    {
        StartMoveSlider(current / _maxFillPoint);
    }

    private IEnumerator WaitMoveToward(float fillPoint)
    {
        float deltaSpeed = _maxDeltaSpeed * Time.deltaTime;

        while (!Mathf.Approximately(_smoothSlider.value, fillPoint))
        {
            _smoothSlider.value = Mathf.MoveTowards(_smoothSlider.value, fillPoint, deltaSpeed);

            yield return null;
        }
    }
}
