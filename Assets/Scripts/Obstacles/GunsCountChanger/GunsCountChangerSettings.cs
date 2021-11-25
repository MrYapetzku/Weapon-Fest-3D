using UnityEngine;

[CreateAssetMenu(fileName = "NewGunsCountChangerSettings", menuName = "Obstacle Resources/Guns Count Changer Settings")]
public class GunsCountChangerSettings : ScriptableObject
{
    [SerializeField] private GunsCountChanger.OperationType _type;
    [SerializeField][Min(1)] private int _value;

    public GunsCountChanger.OperationType Type => _type;
    public int Value => _value;

    private void OnValidate()
    {
        if (_type == GunsCountChanger.OperationType.RandomFromSettings)
            _type = GunsCountChanger.OperationType.Add;
    }
}
