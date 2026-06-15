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

        SetSize(_detector.DetectRadius*2f);

        _sprite.enabled = false;

        _abilityVampirism.AbilityEnabled += ChangeVisibility;
    }

    private void OnDisable()
    {
        _abilityVampirism.AbilityEnabled -= ChangeVisibility;
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
