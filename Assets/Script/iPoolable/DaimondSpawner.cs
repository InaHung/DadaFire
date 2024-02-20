using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DaimondSpawner : MonoBehaviour
{
  
    public Action<Daimond> onDaimondCreate;
    public Action<Daimond> onEatDaimond;
    public Dictionary<DaimondType, ObjectPool> daimondPool = new Dictionary<DaimondType, ObjectPool>();
    private bool CanGetDaimond()
    {

        return UnityEngine.Random.Range(0, 2) == 1;

    }
   
    public void SetMyPool(Dictionary<DaimondType, ObjectPool> objectPool)
    {
        daimondPool = objectPool;
    }
    public void CreatDaimond(Vector3 enemyPosition, DaimondType daimondType)
    {
        if (CanGetDaimond())
        {
            Daimond daimond = daimondPool[daimondType].GetObject() as Daimond;
            daimond.transform.SetParent(transform);
            onDaimondCreate(daimond);
            daimond.transform.position = enemyPosition;
            daimond.OnDisposeDaimond = (daimond) =>
            {
                onEatDaimond(daimond);
            };
        }
           

      

    }

   




}
