using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public Button[] buttonAnswer;
    public TextMeshProUGUI questionsText;
    public Animator animatorText;

    public static List<string> UserAwnsers = new List<string>();


    public static int countSuscess;
    public static int countMistakes;


    public class Question
    {
        public string questionText;
        public string correctAnswer;
        public List<string> wrongAnswers;
    }

    public List<Question> allQuestions = new List<Question>
    {
        new Question
        {
            questionText = "Qual país venceu a primeira Copa do Mundo de Futebol?",
            correctAnswer = "Uruguai",
            wrongAnswers = new List<string>{"Brasil","Alemanha","Itália"}
        },
         new Question
        {
            questionText = "Quantos jogadores cada time deve ter em campo no início de uma partida oficial de futebol?",
            correctAnswer = "11",
            wrongAnswers = new List<string>{"10","12","9"}
        },
         new Question
         {
            questionText = "Quem é o maior artilheiro da história da Seleção Brasileira?",
            correctAnswer = "Neymar",
            wrongAnswers = new List<string>{"Ronaldo","Romário","Pelé"}

         },
          new Question
         {
            questionText = "Qual clube brasileiro foi campeão da Libertadores e do Mundial de Clubes em 2012?",
            correctAnswer = "Corinthians",
            wrongAnswers = new List<string>{"Flamengo","Palmeiras","São Paulo"}

         },
           new Question
         {
            questionText = "Qual é o maior estádio de futebol do Brasil em capacidade de público?",
            correctAnswer = "Maracanã",
            wrongAnswers = new List<string>{"Arena Corinthians","Morumbi","Mineirão"}

         }

    };
    private int currentIndex = 0;
    private int index = 0;


    void Awake()
    {
        ShowQuestion();
        index = currentIndex;
    }
    private void Start()
    {
        countSuscess = 0;
        countMistakes = 0;
        UserAwnsers.Clear();
    }

    public void SwitchQuestion()
    {
        if (currentIndex != index)
        {
            ShowQuestion();
            index = currentIndex;
        }
    }
    void ShowQuestion()
    {

        if (currentIndex < 0 || currentIndex >= allQuestions.Count)
        {
            SceneManager.LoadScene("FinalScene");
            return;
        }


        Question currentQuestion = allQuestions[currentIndex];
        questionsText.text = currentQuestion.questionText;


        List<string> allAnswers = new List<string>(currentQuestion.wrongAnswers);
        allAnswers.Add(currentQuestion.correctAnswer);


        Shuffle(allAnswers);


        for (int i = 0; i < buttonAnswer.Length; i++)
        {
            int index = i;
            buttonAnswer[i].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[i];
            buttonAnswer[i].onClick.RemoveAllListeners();
            buttonAnswer[i].onClick.AddListener(() => verification(allAnswers[index], buttonAnswer[index]));
        }
    }



    void verification(string answerChoosed, Button buttonClicked)
    {
        
        string correctAnswer = allQuestions[currentIndex].correctAnswer;

        UserAwnsers.Add(answerChoosed);

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        Animator buttonAnimator = buttonClicked.gameObject.GetComponent<Animator>();
        DesactiveButtons();

        if (answerChoosed == correctAnswer)
        {
            buttonAnimator.SetTrigger("Correct");
            Debug.Log("Acerto!");
        }
        else
        {
            buttonAnimator.SetTrigger("Error");
            Debug.Log("Erro!");
        }

        StartCoroutine(WaitAndSwitch(answerChoosed == correctAnswer));
    }


    public IEnumerator WaitAndSwitch(bool acertou)
    {
       
        animatorText.SetTrigger("Switch");
        yield return new WaitForSeconds(0.5f);  
        float animationProgress = 0f;

        while (animationProgress < 0.5f) 
        {
            animationProgress = animatorText.GetCurrentAnimatorStateInfo(0).normalizedTime % 1; 
            yield return null;  
        }
        
        if (acertou) { countSuscess++; }
        else { countMistakes++; }

        currentIndex++;

        SwitchQuestion();
        ActiveButtons();
    }

    void DesactiveButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            buttonAnswer[i].interactable = false;
        }
    }

    void ActiveButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            buttonAnswer[i].interactable = true;
        }
    }
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(0, list.Count);
            T temp = list[rnd];
            list[rnd] = list[i];
            list[i] = temp;
        }
    }
}
