using UnityEngine;
using UnityEngine.SceneManagement;

public class StartQuiz : MonoBehaviour
{

    public void OnMouseDown()
    {
        Debug.Log("Cliquei");


        TransitionController transition = FindObjectOfType<TransitionController>();


        StartCoroutine(transition.TransitionScene());

    }
}
