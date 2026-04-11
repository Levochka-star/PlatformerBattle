using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _ofset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _slideSpeed;

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector2 followPosition = _target.transform.position + _ofset;
            Vector2 targetPosition = Vector2.Lerp(transform.position, followPosition, _slideSpeed * Time.deltaTime);

            transform.position = targetPosition;
        }
    }
}