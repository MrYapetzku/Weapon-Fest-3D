using UnityEngine;

public class GunPointTracker : MonoBehaviour
{
    [SerializeField] private float _appearDuration;
    [SerializeField] private float _appearSphereRadius;
    [SerializeField] private float _appearSpherePositionZ;

    private GunsPointGiver _pointGiver;
    private GunPoint _gunPoint;
    private float _timer;

    private void Awake()
    {
        _pointGiver = GetComponentInParent<GunsPointGiver>();
        if (_pointGiver == null)
            throw new System.Exception($"Parent doesn't contain component {typeof(GunsPointGiver)}");
    }

    private void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;

            transform.position = Vector3.Lerp(_gunPoint.transform.position, transform.position, _timer/_appearDuration);
            return;
        }

        transform.position = _gunPoint.transform.position;
    }

    private void OnEnable()
    {
        _gunPoint = _pointGiver.GetPoint();
        _timer = _appearDuration;
        SetStartPosition();
    }

    private void OnDisable()
    {
        _pointGiver.ReleasePoint();
    }

    private void SetStartPosition()
    {
        transform.position = (Random.insideUnitSphere * _appearSphereRadius) + new Vector3(0, 0, _pointGiver.transform.position.z + _appearSpherePositionZ);
    }
}
