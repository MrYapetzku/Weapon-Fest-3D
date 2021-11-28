using System.Collections;
using UnityEngine;

public class LookAtBossAnimation : MonoBehaviour
{
    [SerializeField] private float _reducedTimeScale;
    [SerializeField][Min(0.1f)] private float _duration;

    private void ReduceTimeScale()
    {
        Time.timeScale = _reducedTimeScale;
    }

    private IEnumerator ReturnTimeScale()
    {
        float timer = _duration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            Time.timeScale = Mathf.Lerp(1, Time.timeScale, timer / _duration);
            yield return new WaitForEndOfFrame();
        }
    }
}
