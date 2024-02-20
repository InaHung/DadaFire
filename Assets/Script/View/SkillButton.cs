using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillButton : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SetButton(Sprite sprite, int level)
    {
      
        image.sprite = sprite;
        text.text = level.ToString();
        gameObject.SetActive(true);
    }

}
