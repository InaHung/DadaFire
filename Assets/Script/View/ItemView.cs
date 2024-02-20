using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemView : MonoBehaviour, IView
{
    [Regist]
    private ItemMediator mediator;
    public TreasureChestSpawner chestSpawner;
    public int meatHp;
    public List<SpecialItem> specialItems = new List<SpecialItem>();
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
        chestSpawner.onGetTreasure = GetSpecailItem;
    }
    public void GetSpecailItem(Vector3 spawnPosition)
    {
        int i = Random.Range(0, specialItems.Count);
        SpecialItem specialItem = Instantiate(specialItems[i]);
        specialItem.transform.position = spawnPosition;
        specialItem.onEatSpecailItem = GetSpecialSkill;
    }
    public void GetSpecialSkill(SpecialItemType specialItemType)
    {
        switch (specialItemType)
        {
            case SpecialItemType.magnet:
                mediator.GetAllDaimonds();
                break;
            case SpecialItemType.bomb:
                mediator.Bomb();
                break;
            case SpecialItemType.meat:
                mediator.SetPlayerHp(meatHp);
                break;

        }
    }
    public void StopTween(bool stop)
    {
        if (stop)
            chestSpawner.spawnTween.Pause();
        else
            chestSpawner.spawnTween.Play();
    }
         


}
