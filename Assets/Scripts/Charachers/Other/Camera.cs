using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _ofset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _slideSpeed;

    private void FixedUpdate()
    {
        if (_player != null)
        {
            Vector2 cameraPosition = _player.transform.position + _ofset;
            Vector2 targetPosition = Vector2.Lerp(transform.position, cameraPosition, _slideSpeed * Time.deltaTime);

            transform.position = targetPosition;
        }
    }
}