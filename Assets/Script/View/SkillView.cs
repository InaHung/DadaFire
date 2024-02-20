using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillView : MonoBehaviour, IView
{
    [Regist]
    private SkillMediator mediator;
    private SelectSkills[] selectSkills;
    public SkillButton[] skillButton;
    public WeaponSpriteSetting spriteSetting;

    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
    }
    private void Update()
    {
        
    }
    public void ShowWeaponChoice(SelectSkills[] selects)
    {
        
        selectSkills = selects;
        for (int i = 0; i < selectSkills.Length; i++)
        {
           
            Sprite sp = GetWeaponSprite(selectSkills[i].weaponType);
            skillButton[i].SetButton(sp, selectSkills[i].level);
        }
    }



    public Sprite GetWeaponSprite(WeaponType weaponType)
    {

        for (int i = 0; i < spriteSetting.weaponSprite.Count; i++)
        {
            if (weaponType == spriteSetting.weaponSprite[i].weaponType)
            {
                return spriteSetting.weaponSprite[i].sprite;
            }
        }
        return null;
    }
    public void ChooseWeapon(int number)
    {
        mediator.ChooseWeapon(selectSkills[number].weaponType);
        foreach(var button in skillButton)
        {
            button.gameObject.SetActive(false);
        }
        
    }

  
}


