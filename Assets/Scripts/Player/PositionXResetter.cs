using System.Collections;
using UnityEngine;

public class PositionXResetter : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _duration;

    private void OnEnable()
    {
        StartCoroutine(ResetPosition());
    }

    private IEnumerator ResetPosition()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(0, transform.position.y, transform.position.z);

        float timer = _duration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            transform.position = Vector3.Lerp(targetPosition, startPosition, timer / _duration);

            yield return new WaitForEndOfFrame();
        }
        enabled = false;
    }
}
