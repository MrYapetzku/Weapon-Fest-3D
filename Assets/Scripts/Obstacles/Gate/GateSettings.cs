using UnityEngine;

[CreateAssetMenu(fileName = "NewGateSettings", menuName = "Obstacle Resources/Gate Settings")]
public class GateSettings : ScriptableObject
{
    [SerializeField] private Gate.OperationType _type;
    [SerializeField][Min(1)] private int _value;

    public Gate.OperationType Type => _type;
    public int Value => _value;

    private void OnValidate()
    {
        if (_type == Gate.OperationType.RandomFromSettings)
            _type = Gate.OperationType.Add;
    }
}
