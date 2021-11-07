using UnityEngine;

[CreateAssetMenu(fileName = "NewGunsCountChangerSettings", menuName = "Obstacle Resources/Guns Count Changer Settings")]
public class GunsCountChangerSettings : ScriptableObject
{
    [SerializeField] private char _operation;
    [SerializeField][Min(1)] private int _value;
    [SerializeField] private Material _material;

    public char Operation => _operation;
    public int Value => _value;
    public Material Material => _material;

    private void OnValidate()
    {
        if (_operation == '*')
            _operation = 'x';

        if (_operation == '/')
            _operation = '÷';

        if (_operation != '+' && _operation != '-' && _operation != 'x' && _operation != '÷')
            _operation = '+';
    }
}
