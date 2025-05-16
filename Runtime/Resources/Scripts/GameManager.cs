using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public enum InputType
    {
        Name,
        CPF,
        Phone,
        Email
    }
    public static GameManager Instance;

    [SerializeField] TMP_Text printBox;

    private TMP_InputField currentInputField;
    private InputType currentInputType;
    private string rawInput = "";

   
    private void Awake()
    {
        Instance = this;
    }

    public void SetActiveInput(InputFieldType inputFieldType)
    {
        currentInputField = inputFieldType.inputField;
        currentInputType = inputFieldType.inputType;
        rawInput = GetRawFromFormatted(currentInputField.text, currentInputType);
        currentInputField.Select();
    }

    public void AddLetter(string letter)
    {
        if (currentInputField == null || string.IsNullOrEmpty(letter))
            return;

        // Decide regras com base no tipo
        switch (currentInputType)
        {
            case InputType.CPF:
                if (!char.IsDigit(letter[0]) || rawInput.Length >= 11)
                    return;
                rawInput += letter;
                currentInputField.SetTextWithoutNotify(FormatCPF(rawInput));
                break;

            case InputType.Phone:
                if (!char.IsDigit(letter[0]) || rawInput.Length >= 11)
                    return;
                rawInput += letter;
                currentInputField.SetTextWithoutNotify(FormatPhone(rawInput));
                break;

            case InputType.Email:
                rawInput += letter;
                currentInputField.SetTextWithoutNotify(rawInput); // e-mail pode ser livre
                break;

            case InputType.Name:
                rawInput += letter;
                currentInputField.SetTextWithoutNotify(rawInput); // nome também
                break;
        }

        currentInputField.caretPosition = currentInputField.text.Length;
    }

    public void DeleteLetter()
    {
        if (currentInputField == null || rawInput.Length == 0)
            return;

        rawInput = rawInput.Substring(0, rawInput.Length - 1);

        switch (currentInputType)
        {
            case InputType.CPF:
                currentInputField.SetTextWithoutNotify(FormatCPF(rawInput));
                break;

            case InputType.Phone:
                currentInputField.SetTextWithoutNotify(FormatPhone(rawInput));
                break;

            default:
                currentInputField.SetTextWithoutNotify(rawInput);
                break;
        }

        currentInputField.caretPosition = currentInputField.text.Length;
    }

    public void SubmitWord()
    {
        if (currentInputField == null)
            return;

        printBox.text = currentInputField.text;
        rawInput = "";
        currentInputField.text = "";
    }

    private string GetRawFromFormatted(string text, InputType type)
    {
        return type switch
        {
            InputType.CPF or InputType.Phone => new string(text.Where(char.IsDigit).ToArray()),
            _ => text
        };
    }

    private string FormatCPF(string digits)
    {
        string f = "";
        for (int i = 0; i < digits.Length; i++)
        {
            f += digits[i];
            if (i == 2 || i == 5) f += ".";
            else if (i == 8) f += "-";
        }
        return f;
    }

    private string FormatPhone(string digits)
    {
        digits = new string(digits.Where(char.IsDigit).ToArray());

        switch (digits.Length)
        {
            case 0:
                return "";
            case 1:
            case 2:
                return $"({digits}";
            case 3:
            case 4:
            case 5:
            case 6:
                return $"({digits.Substring(0, 2)}) {digits.Substring(2)}";
            case 7:
            case 8:
            case 9:
            case 10:
                return $"({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7)}";
            default:
                return $"({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7, 4)}";
        }
    }
}
