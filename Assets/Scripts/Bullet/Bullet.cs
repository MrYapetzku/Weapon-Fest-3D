using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    [SerializeField] private int _duplicates;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        StartCoroutine(LifeRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(LifeRoutine());
    }

    public void SetDuplicates(int count)
    {
        _duplicates = count;
    }

    public void SetLifetime(float lifetime)
    {
        _lifetime = lifetime;
    }


    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        if (balloon)
        {
            balloon.Hit();
            _duplicates--;
            if (_duplicates < 1)
                gameObject.SetActive(false);
        }
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(_lifetime);

        gameObject.SetActive(false);
    }
}
