using TMPro;
using UnityEngine;

public class GunsCountChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MeshRenderer _meshRenderer;

    private char _operation;
    private int _value;

    private void Awake()
    {
        var settings = Resources.LoadAll<GunsCountChangerSettings>("");
        int index = Random.Range(0, settings.Length);

        _meshRenderer.material = settings[index].Material;
        _operation = settings[index].Operation;
        _value = settings[index].Value;

        _text.text = _operation + _value.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player)
        {
            switch (_operation)
            {
                case '+':                    
                    player.IncreaseGunsCountBy(_value);
                    break;
                case '-':
                    player.DecreaseGunsCountBy(_value);
                    break;
                case '*':
                    int increaseValue = player.GunsCount * (_value - 1);
                    player.IncreaseGunsCountBy(increaseValue);
                    break;
                case '/':
                    int decreaseValue = player.GunsCount - (player.GunsCount  / _value);
                    player.DecreaseGunsCountBy(decreaseValue);
                    break;
            }
        }
    }
}
