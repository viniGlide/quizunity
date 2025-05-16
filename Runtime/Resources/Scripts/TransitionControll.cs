using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControll : MonoBehaviour
{
    public Animator animatorTransition;
    public int sceneIndex = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(TransitionScene());
        }
    }

    public void TransitionSceneButton()
    {
        StartCoroutine(TransitionScene());
    }

    public IEnumerator TransitionScene()
    {
        animatorTransition.SetTrigger("fadeout");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneIndex);
    }

}

