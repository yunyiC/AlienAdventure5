using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropsManager : MonoBehaviour
{
    public static DropsManager main;

    public GameObject[] objDrops;
    public float[] rateDropDrops;

    void Awake()
    {
        DropsManager.main = this;
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

    public static GameObject GetObjDropRandom()
    {
        if(main == null)
        {
            return null;
        }
        if(main.objDrops == null)
        {
            return null;
        }

        float rateMax = 0;
        foreach(float rateT in main.rateDropDrops)
        {
            rateMax += rateT;
        }

        float randomT = Random.Range(0.0f, rateMax);

        for(int i=0;i<main.rateDropDrops.Length;i++)
        {
            if(randomT < main.rateDropDrops[i])
            {
                return Instantiate(main.objDrops[i]);
            }
            else
            {
                randomT -= main.rateDropDrops[i];
            }
        }

        return null;
    }
}
