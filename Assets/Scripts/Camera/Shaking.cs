using System.Collections;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField] private Shooting _playerShooting;
    [SerializeField] [Min(0)] private float _duration;
    [SerializeField] [Min(0)] private float _amplitude;
    [SerializeField] [Min(0)] private float _frequency;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _playerShooting.Fire += OnFire;
    }

    private void OnDisable()
    {
        _playerShooting.Fire -= OnFire;
    }

    private void OnFire(bool isFinalFire)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float currentTime = _duration;
        Vector3 targetOffset = Random.insideUnitCircle.normalized * _amplitude;
        float speed = _amplitude * _frequency;

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetOffset.x, targetOffset.y, transform.position.z), speed * Time.deltaTime);

            if (transform.position == targetOffset)
                targetOffset *= -1;

            yield return new WaitForEndOfFrame();
        }

        transform.position = new Vector3(0, 0, transform.position.z);
        _coroutine = null;
    }
}
