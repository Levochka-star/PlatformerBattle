using UnityEngine;

public class Patroller : MonoBehaviour
{
    [Tooltip("Вставьте сюда объект расположением которого будет правая граница перемещения")]
    [SerializeField] private Transform _targetStart;
    [Tooltip("Вставьте сюда объект расположением которого будет левая граница перемещения")]
    [SerializeField] private Transform _targetEnd;

    public Transform GetTargetStart()
    {
        return _targetStart;
    }

    public Transform GetTargetEnd()
    {
        return _targetEnd;
    }
}