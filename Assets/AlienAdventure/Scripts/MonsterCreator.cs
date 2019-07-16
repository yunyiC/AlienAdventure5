using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCreator : MonoBehaviour
{
    public bool isWork = true;
    public Transform tfmSpawn;

    public int numMonsterAll = 20;//总共有的怪物数量
    public int numMonsterMax = 5;//场景中最多同时存在的怪物数量
    public int numMonsterNow = 0;//场景中当前存在的怪物数量

    public float timeSpawnMax = 5.0f;
    public float timeSpawnMin = 1.0f;
    public float timeSpawn;
    public float timeSpawnNow;

    public Color colorCalm;
    public Color colorAnger;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        numMonsterNow = 0;

        timeSpawnMin = Mathf.Min(timeSpawnMin, timeSpawnMax);

        timeSpawn = GetTimeSpawnRandom();
        timeSpawnNow = 0.0f;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Working();
    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    private void Working()
    {
        if (!isWork)
        {
            return;
        }

        Spawning();
    }

    private void Spawning()
    {
        timeSpawnNow += Time.deltaTime;
        if(timeSpawnNow > timeSpawn)
        {
            timeSpawnNow -= timeSpawn;
            timeSpawn = GetTimeSpawnRandom();
            CreateMonster();
        }
    }

    public void CreateMonster()
    {
        if( ! isWork)
        {
            return;
        }
        if(numMonsterNow >= numMonsterMax)
        {
            return;
        }
        if(numMonsterAll <= 0)
        {
            return;
        }

        GameObject objMonsterT = MonsterManager.GetMonsterRandom();
        if (objMonsterT != null)
        {
            MonsterController monster = objMonsterT.GetComponent<MonsterController>();
            if (monster != null)
            {
                NavMeshHit closestHit;
                if (NavMesh.SamplePosition(tfmSpawn.position, out closestHit, 50, 1))
                {
                    monster.transform.position = closestHit.position;
                    monster.positionStart = this.transform.position;
                    monster.cMonster = this;
                    monster.Initialize();
                    numMonsterNow++;
                    numMonsterAll--;//每生产一只,总数量就少一只.
                    //Debug.Log("成功生成僵尸");
                }
                else
                {
                    Destroy(objMonsterT, 0.0f);
                    Debug.Log("没找到合适的出生点,僵尸生成失败");
                }
            }
            else
            {
                Debug.Log("获取僵尸组件失败");
                Destroy(objMonsterT, 0.0f);
                return;
            }
        }
        else
        {
            Debug.Log("申请随机僵尸失败");
        }
    }

    private float GetTimeSpawnRandom()
    {
        return Random.Range(timeSpawnMin, timeSpawnMax);
    }
}
