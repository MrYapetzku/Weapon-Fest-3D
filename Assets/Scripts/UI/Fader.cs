using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private const string FADER_PATH = "Fader/Fader";

    [SerializeField] private Animator _animator;

    private static Fader _instance;

    public static Fader Instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public bool IsFading { get; private set; }

    private Action _fadedInCallback;
    private Action _fadedOutCallback;

    public void FadeIn(Action fadedInCallback)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedInCallback = fadedInCallback;
        _animator.SetBool("Faded", true);
    }

    public void FadeOut(Action fadedOutCallback)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedOutCallback = fadedOutCallback;
        _animator.SetBool("Faded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        IsFading = false;
        _fadedInCallback?.Invoke();
        _fadedInCallback = null;
    }

    private void Handle_FadeOutAnimationOver()
    {
        IsFading = false;
        _fadedOutCallback?.Invoke();
        _fadedOutCallback = null;
    }
}
