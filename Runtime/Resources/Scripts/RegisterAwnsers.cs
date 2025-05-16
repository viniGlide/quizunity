using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class RegisterAwnsers : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RegisterAnswer(RegisterUser.currentId, Quiz.UserAwnsers));
    }




    public IEnumerator RegisterAnswer(int userId, List<string> answers)
    {
        string jsonArray = "[" + string.Join(",", answers.Select(a => $"\"{a}\"")) + "]";

        UnityWebRequest www = UnityWebRequest.Put($"http://localhost:5262/api/Register/postUserAwnsers/{userId}", jsonArray);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError(www.error);
        else
            Debug.Log("Sucesso!");
    }


}