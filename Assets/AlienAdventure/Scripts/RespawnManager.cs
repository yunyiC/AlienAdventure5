using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnManager : MonoBehaviour
{
    public static GameObject[] objRespawns;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        objRespawns = GameObject.FindGameObjectsWithTag(Tags.Respawn);
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

    public static GameObject GetRespawnNearest(Transform tfmOther)
    {
        if(objRespawns == null)
        {
            return null;
        }

        float distanceT = float.MaxValue;
        GameObject objRespawnT = null;
        foreach(GameObject objT in objRespawns)
        {
            float distanceT2 = Vector3.Distance(objT.transform.position, tfmOther.position);
            if(distanceT2 < distanceT)
            {
                distanceT = distanceT2;
                objRespawnT = objT;
            }
        }
        return objRespawnT;
    }
}
