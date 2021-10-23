using UnityEngine;

public class PositionXResetter : MonoBehaviour
{
    [SerializeField] private float _duration;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _timer;

    private void OnEnable()
    {
        _startPosition = transform.position;
        _targetPosition = new Vector3(0, transform.position.y, transform.position.z);
        _timer = _duration;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        transform.position = Vector3.Lerp(_targetPosition, _startPosition, _timer/ _duration);

        if (_timer <= 0)
        {
            _timer = 0;
            this.enabled = false;
        }
    }
}
