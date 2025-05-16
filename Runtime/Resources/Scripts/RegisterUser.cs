using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public TextMeshProUGUI NameWarning;
    public TextMeshProUGUI EmailWarning;
    public TextMeshProUGUI DocWarning;
    public TextMeshProUGUI PhoneWarning;
    public TextMeshProUGUI RegisterWarning;

    public Animator Animator;

    public TMP_InputField userName;
    public TMP_InputField userDoc;
    public TMP_InputField userPhone;
    public TMP_InputField userEmail;
    public static string userNameFinal;

    private bool verName;
    private bool verEmail;
    private bool verDoc;
    private bool verPhone;
    public bool dataValidad = false;

    public bool isRegistered;
    public static int currentId;

    public string url = "http://localhost:5262/api/Register/postRegister";
    public class RegisterData
    {
        public int id;
        public string userName;
        public string userDoc;
        public string userPhone;
        public string userEmail;
    }

    [System.Serializable]
    public class RegisterResponse
    {
        public int id;

    }


    public void OnClickRegisterButton()
    {
        string username = userName.text.ToString();
        string userdoc = userDoc.text.ToString();
        string userphone = userPhone.text.ToString();
        string useremail = userEmail.text.ToString();

        userNameFinal = userName.text.ToString();

        RegisterData data = new RegisterData
        {
            id = 0,
            userName = userName.text,
            userDoc = userDoc.text,
            userPhone = userPhone.text,
            userEmail = userEmail.text

        };

        VerifyData();

        Debug.Log(userdoc);

        if (dataValidad == true && isRegistered == false)
        {
            string json = JsonUtility.ToJson(data);

            Debug.Log("Enviando JSON: " + json);

            StartCoroutine(PostRegister(json));


            dataValidad = false;
        }
        else
        {
            Debug.Log("Dados inválidos");
        }
    }
    public void VerifyData()
    {
        if (userName.text.Length > 0)
        {
            verName = true;
            NameWarning.SetText("");
        }
        else
        {
            verName = false;
            NameWarning.SetText("*Nome inválido.");
        }
        if (userPhone.text.Length > 0)
        {

            verPhone = true;
            PhoneWarning.SetText("");
        }
        else
        {
            verPhone = false;
            PhoneWarning.SetText("*Número inválido.");
        }
        if (userEmail.text.Length > 0 && Regex.IsMatch(userEmail.text, @"^[\w\.-]+@[\w\.-]+\.\w{2,}$"))
        {
            verEmail = true;
            EmailWarning.SetText("");
        }
        else
        {
            verEmail = false;
            EmailWarning.SetText("*Email inválido");
        }
        if (userDoc.text.Length > 0 && userDoc.text.Length == 14)
        {
            verDoc = true;
            DocWarning.SetText("");
        }
        else
        {
            verDoc = false;
            DocWarning.SetText("*CPF inválido");
        }

        if (verName && verDoc && verEmail && verPhone)
        {
            dataValidad = true;
        }
    }

    public IEnumerator PostRegister(string json)
    {
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Enviando JSON: " + json);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success || www.responseCode == 200)
            {
                RegisterWarning.SetText("");
                Debug.Log("Sucesso! Resposta: " + www.downloadHandler.text);

                RegisterResponse response = JsonUtility.FromJson<RegisterResponse>(www.downloadHandler.text);
                currentId = response.id;
                Debug.Log(currentId);
                Animator.SetTrigger("fadeout");
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("QuestionsScene");


            }
            else
            {
                Debug.LogError("Erro: " + www.error);
                Debug.LogError("Código: " + www.responseCode);
                Debug.LogError("Corpo: " + www.downloadHandler.text);

                RegisterWarning.SetText("*Usuário já cadastrado");
            }
        }
    }

}

