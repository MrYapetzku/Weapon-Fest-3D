using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _defaultLifetime;

    private int _duplicates;
    private float _timer;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _timer = _defaultLifetime;
    }

    public void SetDuplicates(int count)
    {
        _duplicates = count;
    }

    public void SetLifetime(float lifetime)
    {
        _timer = lifetime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        var finalObstacle = other.GetComponentInParent<FinalObstacle>();

        if (balloon)
        {
            balloon.Hit();
            _duplicates--;
        }

        if (finalObstacle)
        {
            finalObstacle.TakeBulletHit();
            _duplicates--;
        }

        if (_duplicates < 1)
            gameObject.SetActive(false);
    }
}
