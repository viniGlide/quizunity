using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllRegister : MonoBehaviour
{
    public Button buttonRegister;

    public TMP_InputField userName;
    public TMP_InputField userDoc;
    public TMP_InputField userPhone;
    public TMP_InputField userEmail;

    public  bool verName;
    public  bool verEmail;
    public  bool verDoc;
    public  bool verPhone;
    

    void Start()
    {

        //buttonRegister.interactable = false;

        verName = false;
        verEmail = false;
        verPhone = false;
        verDoc = false;
    }

  


   public void Update()
    {
        //string username = userName.text.ToString();
        //string userdoc = userDoc.text.ToString();
        //string userphone = userPhone.text.ToString();
        //string useremail = userEmail.text.ToString();

        if (userName.text.Length > 0)
        {
            verName = true;
        }
        else
        {
            verName = false;
        }
        if (userPhone.text.Length > 0)
        {

            verPhone = true;
        }
        else
        {
            verPhone = false;
        }
        if (userEmail.text.Length > 0 && Regex.IsMatch(userEmail.text, @"^[\w\.-]+@[\w\.-]+\.\w{2,}$"))
        {
            verEmail = true;
        }
        else
        {
            verEmail = false;
            Debug.Log("Email não está de acordo");
        }
        if (userDoc.text.Length > 0 && userDoc.text.Length < 15)
        {
            verDoc = true;
        }
        else
        {
            verDoc = false;
        }

        if (verName && verDoc && verEmail && verPhone)
        {
            buttonRegister.interactable = true;
        }
      

    }
}
