using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreView : MonoBehaviour, IView
{
    [Regist]
    private ScoreMediator mediator;
    int currentScore;
    public Slider slider;
    public TextMeshProUGUI text;

    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
     
        slider.maxValue = mediator.SetInitScore();
        text.text = mediator.SetInitLevel().ToString();
    }
    public void SetScore(int score)
    {
        slider.value = score;
        
    }
    public  void SetLevel(int level)
    {
        text.text = level.ToString();
    }
    public void SetMaxSlider(int score)
    {
        slider.maxValue = score;
    }    
   

}
