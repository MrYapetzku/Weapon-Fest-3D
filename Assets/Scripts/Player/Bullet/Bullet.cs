using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _defaultSpeed;
    [SerializeField] [Min(0)] private float _defaultLifetime;
    [SerializeField] [Min(0)] private float _finalShotSpeed;
    [SerializeField] [Min(0)] private float _finalShotLifetime;

    private Rigidbody _rigidbody;
    private float _lifeTime;
    private int _duplicates;

    public float FinalShootSpeed => _finalShotSpeed;
    public bool IsFinalFire { get; private set; }

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
        IsFinalFire = true;
    }

    private void SetDefaultSettings()
    {
        _lifeTime = _defaultLifetime;
        _rigidbody.velocity = Vector3.forward * _defaultSpeed;
        IsFinalFire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponent<Balloon>();
        var finalObstacle = other.GetComponentInParent<FinalObstacle>();

        if (balloon)
        {
            balloon.TakeBulletHit();
            _duplicates--;
            CheckDublicates();
        }

        if (finalObstacle)
        {
            _duplicates = finalObstacle.TakeBulletHitAndReturnExcess(_duplicates);
            CheckDublicates();
        }
    }

    private void CheckDublicates()
    {
        if (_duplicates < 1)
            gameObject.SetActive(false);
    }
}
