using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour,IView
{
    [Regist]
    private GameOverMediator mediator;
    public UnityEngine.GameObject gameObjectRoot;
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
        gameObjectRoot.SetActive(false);
    }
    public void GameOver()
    {
        gameObjectRoot.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        gameObjectRoot.SetActive(false);
        Time.timeScale = 1;
    }
}
