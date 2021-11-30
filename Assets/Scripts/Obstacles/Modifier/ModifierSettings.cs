using UnityEngine;

[CreateAssetMenu(fileName = "NewModifierSettings", menuName = "Obstacle Resources/Modifier Settings")]
public class ModifierSettings : ScriptableObject
{
    [SerializeField] private Modifier.OperationType _type;
    [SerializeField][Min(1)] private int _value;

    public Modifier.OperationType Type => _type;
    public int Value => _value;

    private void OnValidate()
    {
        if (_type == Modifier.OperationType.RandomFromSettings)
            _type = Modifier.OperationType.Add;
    }
}
