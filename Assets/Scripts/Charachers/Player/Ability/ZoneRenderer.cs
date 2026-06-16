using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZoneRenderer : MonoBehaviour
{
    [SerializeField] private AbilityVampirism _abilityVampirism;
    [SerializeField] private EnemyDetector _detector;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();

        float diameter = _detector.DetectRadius * 2f;

        SetSize(diameter);

        _sprite.enabled = false;

        _abilityVampirism.AbilityVisiblied += ChangeVisibility;
    }

    private void OnDisable()
    {
        _abilityVampirism.AbilityVisiblied -= ChangeVisibility;
    }

    private void SetSize(float diameter)
    {
       _sprite.transform.localScale = new Vector3(diameter, diameter, 1f);
    }

    private void ChangeVisibility(bool condition)
    {
        _sprite.enabled = condition;
    }
}
