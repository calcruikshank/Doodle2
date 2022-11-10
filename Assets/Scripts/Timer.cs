using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timerValue;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Transform promptUI;

    public static Timer singleton;

    public bool hasStarted = false;
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
        timerText = this.GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {

    }
    void Update()
    {

        timerText.text = ((int)timerValue).ToString();
        promptUI.SetAsLastSibling();
        timerText.transform.SetAsLastSibling();
        if (!hasStarted) return;

        promptUI.gameObject.SetActive(false);
        timerValue -= Time.deltaTime;

        if (timerValue <= 0)
        {
            GameManager.singleton.EndTurn();
        }
    }

    public void SetTimer(float value, Vector3 positionToPutTimer)
    {
        if (value == 0) transform.gameObject.SetActive(false);
        promptUI.gameObject.SetActive(true);
           hasStarted = false;
        timerValue = value;
        transform.position = positionToPutTimer;
        transform.localEulerAngles = GameManager.singleton.eulerAnglesToSet;

        promptUI.position = positionToPutTimer;
        promptUI.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
        if (GameManager.singleton.eulerAnglesToSet == Vector3.zero)
        {
            transform.localPosition = new Vector3(this.transform.localPosition.x - 130, this.transform.localPosition.y + 160, this.transform.localPosition.z);
        }
        if (GameManager.singleton.eulerAnglesToSet.z == 180)
        {
            transform.localPosition = new Vector3(this.transform.localPosition.x + 130, this.transform.localPosition.y - 160, this.transform.localPosition.z);
        }
        if (GameManager.singleton.eulerAnglesToSet.z == 90)
        {
            transform.localPosition = new Vector3(this.transform.localPosition.x - 160, this.transform.localPosition.y - 130, this.transform.localPosition.z);
        }
        if (GameManager.singleton.eulerAnglesToSet.z == 270)
        {
            transform.localPosition = new Vector3(this.transform.localPosition.x + 160, this.transform.localPosition.y + 130, this.transform.localPosition.z);
        }

    }

    public void StartTimer()
    {
        if (timerValue == 0) return;
        if (hasStarted) return;
        hasStarted = true;
    }

   
}
