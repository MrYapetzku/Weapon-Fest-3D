using System.Collections.Generic;
using UnityEngine;

public class GunPointsContainer : MonoBehaviour
{
    [SerializeField] private GunPoint _gunPointTemplate;
    [SerializeField] [Min(1)] private int _maxGunsLayersInPlayer;
    [SerializeField] [Min(0)] private float _distanceBetweenGuns;
    [SerializeField] [Min(0)] private float _positionFactorZ;

    private List<GunPoint> _gunPoints;
    private List<int> _pointCountsInLayers;
    private int _pointIndex;

    public IReadOnlyList<int> PointCountsInLayers => _pointCountsInLayers;
    public int GunPointsCount => _gunPoints.Count;
    public float DistanceBetweenGuns => _distanceBetweenGuns;

    private void Awake()
    {
        _gunPoints = new List<GunPoint>();
        _pointCountsInLayers = new List<int>();
        _pointIndex = 0;
        GenerateGunPoints();
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

    private void GenerateGunPoints()
    {
        SetPoint(Vector3.zero);

        for (int i = 1; i < _maxGunsLayersInPlayer; i++)
        {
            for (int j = 0; j < 3 * i; j++)
            {
                float positionAngle = (180 * j) / (3 * i) * Mathf.Deg2Rad;
                Vector3 position = new Vector3(Mathf.Cos(positionAngle), Mathf.Sin(positionAngle), 0) * _distanceBetweenGuns * i;
                position += new Vector3(0, 0, Random.Range(-_positionFactorZ, _positionFactorZ));

                SetPoint(position);
                SetPoint(-position);
            }
            _pointCountsInLayers.Add(_gunPoints.Count);
        }
    }

    private void SetPoint(Vector3 position)
    {
        GunPoint point = Instantiate(_gunPointTemplate, transform);
        point.transform.position = position;
        _gunPoints.Add(point);
    }
}
