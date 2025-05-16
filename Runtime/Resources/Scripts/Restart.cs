using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene("AwakeScene");
    }

}
