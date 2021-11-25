using TMPro;
using UnityEngine;

public class GunsCountChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _materialBlue;
    [SerializeField] private Material _materialRed;
    [SerializeField] private OperationType _type;
    [SerializeField] [Min(1)] private int _value;

    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        RandomFromSettings
    }

    public OperationType Type => _type;
    public int Value => _value;

    private void Awake()
    {
        if (_type == OperationType.RandomFromSettings)
        {
            var settings = Resources.LoadAll<GunsCountChangerSettings>("");
            if (settings == null)
                throw new System.Exception("Gun count changers resources didn't load.");
            int index = Random.Range(0, settings.Length);
            _type = settings[index].Type;
            _value = settings[index].Value;
        }

        switch (_type)
        {
            case OperationType.Add:
                _text.text = $"+{_value}";
                _meshRenderer.material = _materialBlue;
                break;
            case OperationType.Subtract:
                _text.text = $"-{_value}";
                _meshRenderer.material = _materialRed;
                break;
            case OperationType.Multiply:
                _text.text = $"x{_value}";
                _meshRenderer.material = _materialBlue;
                break;
            case OperationType.Divide:
                _text.text = $"÷{_value}";
                _meshRenderer.material = _materialRed;
                break;
        }
    }
}
