using TMPro;
using Unity.Collections;
using UnityEngine;

public class FormatterCPF : MonoBehaviour
{
    public TMP_InputField inputField;
    private string lastText = "";



    void Start()
    {
        if (inputField == null)
            inputField = GetComponent<TMP_InputField>();

        inputField.onValueChanged.AddListener(FormatCPF);
    }

    void FormatCPF(string input)
    {
        string numeric = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c))
                numeric += c;

        }

        if (numeric.Length > 11)
            numeric = numeric.Substring(0, 11);

        string formatted = "";
        for (int i = 0; i < numeric.Length; i++)
        {
            formatted += numeric[i];
            if (i == 2 || i == 5)
                formatted += ".";
            else if (i == 8)
                formatted += "-";
        }

        if (formatted != lastText)
        {
            lastText = formatted;
            inputField.text = formatted;

        }
    }
}
