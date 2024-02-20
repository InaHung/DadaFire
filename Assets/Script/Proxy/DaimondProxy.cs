using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DaimondProxy : IProxy
{
    [Regist]
    private ScoreProxy scoreProxy;
    [Regist]
    private PlayerProxy playerProxy;
    public List<Daimond> currentDaimond = new List<Daimond>();
    public Vector3 enemyPosition;
    public DaimondType daimondType1;

    public void SetDaimond(Daimond daimond)
    {
        if (!currentDaimond.Contains(daimond))
        {
            currentDaimond.Add(daimond);
        }

    }
    public void EatDaimond(Daimond daimond)
    {
        scoreProxy.AddScore(daimond.score);
        if (currentDaimond.Contains(daimond))
        {
            currentDaimond.Remove(daimond);
        }
    }
    public void EatAllDaimonds()
    {

        foreach (Daimond daimond in currentDaimond)
        {
            daimond.eatMagnet = true;
            daimond.GetPlayerTransform(playerProxy.playerTransform);
        } 
        
    }
    public void SetEnemyInfo(Vector3 vector3, DaimondType daimondType)
    {
        enemyPosition = vector3;
        daimondType1 = daimondType;
        Broadcast(EnemyEvent.ON_GET_ENEMY_INFO);
    }
   /* public void StopTween(bool stop)
    {
        if(stop)
        {
            foreach (var daimond in currentDaimond)
            {
                daimond.moveTween.Pause();
        }
        }
        else
            foreach (var daimond in currentDaimond)
            {
                daimond.moveTween.Play();
            }

    }*/

}
