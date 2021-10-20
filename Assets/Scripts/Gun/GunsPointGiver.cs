using System.Collections.Generic;
using UnityEngine;

public class GunsPointGiver : MonoBehaviour
{
    [SerializeField] private GunsPointsContainer _gunsPointsContainer;
    [SerializeField] private GunPoint _gunPointTemplate;
    [SerializeField] private Player _player;

    private List<GunPoint> _gunPoints;
    private int _pointIndex;

    private void Awake()
    {
        _gunPoints = new List<GunPoint>();
        _pointIndex = 0;
        GenerateGunsPoints();
        _player.enabled = true;
    }

    private void OnDisable()
    {
        _player.enabled = false;
    }

    public GunPoint GetPoint()
    {
        GunPoint point = _gunPoints[_pointIndex];
        _pointIndex += 1;
        if (_pointIndex == _gunPoints.Count)
            _pointIndex = 0;
        return point;
    }

    public void ReleasePoint()
    {
        _pointIndex -= 1;
        if (_pointIndex < 0)
            _pointIndex = _gunPoints.Count - 1;
    }

    private void GenerateGunsPoints()
    {
        Vector3 vector = Vector3.zero;

        GunPoint point = Instantiate(_gunPointTemplate, _gunsPointsContainer.transform);
        point.transform.position = vector;
        _gunPoints.Add(point);

    }
}
