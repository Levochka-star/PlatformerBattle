using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZoneRenderer : MonoBehaviour
{
    [SerializeField] private AbilityVampirism _abilityVampirism;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.enabled = false;

        _abilityVampirism.AbilityEnabled += ToggleVisibility;
    }

    private void OnDestroy()
    {
        _abilityVampirism.AbilityEnabled -= ToggleVisibility;
    }

    private void ToggleVisibility()
    {
        if (_sprite.enabled)
        {
            _sprite.enabled = false;
        }
        else
        {
            _sprite.enabled = true;
        }
    }
}
