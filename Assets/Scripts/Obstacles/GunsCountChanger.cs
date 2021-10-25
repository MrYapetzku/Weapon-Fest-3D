using TMPro;
using UnityEngine;

public class GunsCountChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MeshRenderer _meshRenderer;

    public char Operation { get; private set; }
    public int Value { get; private set; }

    private void Awake()
    {
        var settings = Resources.LoadAll<GunsCountChangerSettings>("");
        if (settings == null)
            throw new System.Exception("Gun count changers resources didn't load.");

        int index = Random.Range(0, settings.Length);

        _meshRenderer.material = settings[index].Material;
        Operation = settings[index].Operation;
        Value = settings[index].Value;
        _text.text = Operation + Value.ToString();
    }
}
