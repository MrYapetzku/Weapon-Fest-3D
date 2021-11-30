using UnityEngine;

public class GunPointTracker : MonoBehaviour
{
    [SerializeField] private Gun _gun;

    private GunPointsContainer _gunPointsContainer;
    private GunPoint _gunPoint;
    private float _timer;

    private void Awake()
    {
        _gunPointsContainer = GetComponentInParent<Player>().GunPointsContainer;
        if (_gunPointsContainer == null)
            throw new System.Exception($"Parent doesn't contain component {typeof(Player)}");
    }

    private void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;

            transform.position = Vector3.Lerp(_gunPoint.transform.position, transform.position, _timer/ _gun.AppearDuration);
            return;
        }

        transform.position = _gunPoint.transform.position;
    }

    private void OnEnable()
    {
        _gunPoint = _gunPointsContainer.GetPoint();
        _timer = _gun.AppearDuration;
        SetStartPosition();
    }

    private void OnDisable()
    {
        _gunPointsContainer.ReleasePoint();
    }

    private void SetStartPosition()
    {
        transform.position = (Random.insideUnitSphere * _gun.AppearSphereRadius) + new Vector3(0, 0, _gunPointsContainer.transform.position.z + _gun.AppearSpherePositionZ);
    }
}
