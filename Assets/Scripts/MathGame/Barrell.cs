using UnityEngine;
using TMPro;

public class Barrell : MonoBehaviour
{
    private TextMeshProUGUI _numberText;
    private int _number;
    public int Number { get { return _number; } }

    void Start()
    {
        _numberText = GetComponentInChildren<TextMeshProUGUI>();
        string n = _numberText.text;
        _number = int.Parse(n);
    }
}
