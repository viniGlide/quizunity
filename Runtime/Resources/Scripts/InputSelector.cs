using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InputFieldType : MonoBehaviour, IPointerClickHandler
{
    public GameManager.InputType inputType;
    public TMP_InputField inputField;

    private void Awake()
    {
        if (inputField == null)
            inputField = GetComponent<TMP_InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.SetActiveInput(this);
    }
}
