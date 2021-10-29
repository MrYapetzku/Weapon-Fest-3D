using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _defaultLifetime;
    [SerializeField] private float _finalShotSpeed;
    [SerializeField] private float _finalShotLifetime;

    private Rigidbody _rigidbody;
    private float _lifeTime;
    private int _duplicates;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SetDefaultSettings();
    }

    public void SetDuplicates(int count)
    {
        _duplicates = count;
    }

    public void SetFinalShotSettings()
    {
        _lifeTime = _finalShotLifetime;
        _rigidbody.velocity = Vector3.forward * _finalShotSpeed;
    }

    private void SetDefaultSettings()
    {
        _lifeTime = _defaultLifetime;
        _rigidbody.velocity = Vector3.forward * _defaultSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        var finalObstacle = other.GetComponentInParent<FinalObstacle>();

        if (balloon)
        {
            balloon.TakeBulletHit();
            _duplicates--;
            CheckDublicates();
        }

        if (finalObstacle)
        {
            for (int i = 0; i < finalObstacle.Durability; i++)
            {
                finalObstacle.TakeBulletHit();
                _duplicates--;
                CheckDublicates();
            }
        }
    }

    private void CheckDublicates()
    {
        if (_duplicates < 1)
            gameObject.SetActive(false);
    }
}
