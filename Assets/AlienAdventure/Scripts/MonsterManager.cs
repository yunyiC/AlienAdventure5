using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterManager : MonoBehaviour
{
    public static MonsterManager main;

    public GameObject[] objMonsters;
    public float[] rateOfMonsters;

    void Awake()
    {
        MonsterManager.main = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void LateUpdate()
    {

    }

    void FixedUpdate()
    {

    }

    public static GameObject GetMonsterRandom()
    {
        if (main == null)
        {
            return null;
        }
        if (main.objMonsters == null)
        {
            return null;
        }

        float rateMax = 0;
        foreach (float rateT in main.rateOfMonsters)
        {
            rateMax += rateT;
        }

        float randomT = Random.Range(0.0f, rateMax);

        for (int i = 0; i < main.rateOfMonsters.Length; i++)
        {
            if (randomT < main.rateOfMonsters[i])
            {
                return Instantiate(main.objMonsters[i]);
            }
            else
            {
                randomT -= main.rateOfMonsters[i];
            }
        }

        return null;
    }
}
