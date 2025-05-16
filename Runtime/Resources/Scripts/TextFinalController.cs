using TMPro;
using UnityEngine;

public class TextFinalController : MonoBehaviour
{
    public TextMeshProUGUI tituleFinal;
    public TextMeshProUGUI txtFinal;

    private int totalQuestions;


    void Start()
    {
        totalQuestions = Quiz.countSuscess + Quiz.countMistakes;
        string FirstName = RegisterUser.userNameFinal.Split(' ')[0];
        
        if (Quiz.countSuscess > 2)
        {
            tituleFinal.SetText("Que pena!!");
            txtFinal.SetText("Parabéns " + FirstName + " acertou " + Quiz.countSuscess + " de " + totalQuestions + "<br>Obrigado pela sua participação!! ");
        }
        else
        {
            tituleFinal.SetText("Parabéns!!");
            txtFinal.SetText("Que pena " + FirstName + " acertou apenas " + Quiz.countSuscess + " de " + totalQuestions + "<br>Obrigado pela sua participação!! ");
        }

    }


}
