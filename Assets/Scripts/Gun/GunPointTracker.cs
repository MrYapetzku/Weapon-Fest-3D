using System;
using UnityEngine;

public class GunPointTracker : MonoBehaviour
{
    private GunsPointGiver _pointGiver;
    private GunPoint _gunPoint;

    private void Awake()
    {
        _pointGiver = GetComponentInParent<GunsPointGiver>();
        if (_pointGiver == null)
            throw new Exception($"Parent doesn't contain component {typeof(GunsPointGiver)}");
    }

    private void Update()
    {
        transform.position = _gunPoint.transform.position;
    }

    private void OnEnable()
    {
        _gunPoint = _pointGiver.GetPoint();
    }

    private void OnDisable()
    {
        _pointGiver.ReleasePoint();
    }
}
