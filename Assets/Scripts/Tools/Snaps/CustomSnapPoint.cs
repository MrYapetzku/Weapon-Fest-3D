using UnityEngine;

public class CustomSnapPoint : MonoBehaviour
{
    [SerializeField] [Range(0, 0.5f)] private float _gizmoSize = 0.05f;
    [SerializeField] private ConnectionType _type;
    [SerializeField] private Color _FirstTypeColor = Color.red;
    [SerializeField] private Color _SecondTypeColor = Color.green;
    [SerializeField] private Color _ThirdTypeColor = Color.blue;

    public enum ConnectionType
    {
        FirstType,
        SecondType,
        ThirdType
    }

    public ConnectionType Type => _type;

    private void OnDrawGizmos()
    {
        switch (_type)
        {
            case ConnectionType.FirstType:
                Gizmos.color = _FirstTypeColor;
                break;
            case ConnectionType.SecondType:
                Gizmos.color = _SecondTypeColor;
                break;
            case ConnectionType.ThirdType:
                Gizmos.color = _ThirdTypeColor;
                break;
        }
        Gizmos.DrawSphere(transform.position, _gizmoSize);
    }
}
