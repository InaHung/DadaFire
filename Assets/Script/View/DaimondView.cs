using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DaimondView : MonoBehaviour, IView
{
    public Dictionary<DaimondType, ObjectPool> daimondPool = new Dictionary<DaimondType, ObjectPool>();
    public List<Daimond> daimondList = new List<Daimond>();
    [Regist]
    private DaimondMediator mediator;
    public DaimondSpawner daimondSpawner;
    public Daimond daimond1;
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
        daimondSpawner.onDaimondCreate += SetDaimond;
        daimondSpawner.onEatDaimond += DaimondDespose;
        CreateDaimondPool();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            {
            DaimondDespose(daimond1);
        }
    }
    public void SetDaimond(Daimond daimond)
    {
        mediator.SetDaimond(daimond);
    }

    public void SetEnemyInfo(Vector3 enemyPositon, DaimondType daimondType)
    {

        daimondSpawner.CreatDaimond(enemyPositon, daimondType);

    }
    public void DaimondDespose(Daimond daimond)
    {
        mediator.EatDaimond(daimond);
    }

    
    public void CreateDaimondPool()
    {
        for (int i = 0; i < daimondList.Count; i++)
        {

            UnityEngine.GameObject daimondObject = new UnityEngine.GameObject();
            daimondObject.transform.SetParent(transform);
            daimondObject.name = daimondList[i].name + "Pool";
            ObjectPool pool = daimondObject.AddComponent<ObjectPool>();
            pool.SetPrefab(daimondList[i]);
            pool.InitPool();
            daimondPool.Add(daimondList[i].daimondType, pool);

        }
        daimondSpawner.SetMyPool(daimondPool);
    }
   
   
}
