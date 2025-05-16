using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Animator transition;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
             
        }
    }


    public void LoadSceneWithTransition(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("end");
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(sceneIndex);
        
    }

}
