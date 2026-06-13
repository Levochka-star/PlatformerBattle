using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private Slider _smoothSlider;
    [SerializeField] private AbilityVampirism _vampirism;

    private Coroutine _waitingMoveToward;

    private void Awake()
    {
        _vampirism.ChangedFillPoint += UpdateValue;
    }

    private void OnDisable()
    {
        _vampirism.ChangedFillPoint -= UpdateValue;
        StopCorountine(_waitingMoveToward);
    }

    public void UpdateValue(float value)
    {
        StartMoveSlider(value);
    }

    private void StartMoveSlider(float fillPoint)
    {
        StopCorountine(_waitingMoveToward);

        _waitingMoveToward = StartCoroutine(WaitMoveToward(fillPoint));
    }

    private void StopCorountine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator WaitMoveToward(float fillPersent)
    {
        float currentValue = _smoothSlider.value;
        float slowdownTravel = 0.8f;
        float timer = 0f;

        while (timer<slowdownTravel)
        {
            timer += Time.deltaTime;
            float progress = timer / slowdownTravel;

            _smoothSlider.value = Mathf.Lerp(currentValue, fillPersent, progress);

            yield return null;
        }

        _smoothSlider.value = fillPersent;
        StopCorountine(_waitingMoveToward);
    }
}
