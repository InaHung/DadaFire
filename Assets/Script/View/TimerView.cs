using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour,IView
{
    [Regist]
    private TimerMediator mediator;
    public  float currentTime;
    public TextMeshProUGUI timeText;
    public  bool pause;
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
        mediator.SetCurrentTime(0);
    }
    private void Update()
    {
        if (pause)
            return;

        currentTime += Time.deltaTime;
        mediator.SetCurrentTime(currentTime); 
        timeText.text = currentTime.ToString("F0");


    }

}
