using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public float floatAmplitude = 10f;  
    public float floatSpeed = 1f;       
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
