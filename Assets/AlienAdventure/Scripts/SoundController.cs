using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundController : MonoBehaviour
{
    public bool isWork = true;

    public Health healthTarget;

    void Awake()
    {
        
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

    private void OnTriggerStay(Collider collider)
    {
        if(!isWork)
        {
            return;
        }

        if(collider.CompareTag(Tags.Monster))
        {
            //Debug.Log("玩家发出的声音被怪物听到了");

            MonsterController monsterControllerT = collider.GetComponent<MonsterController>();
            if (monsterControllerT != null)
            {
                monsterControllerT.SetTarget(healthTarget);
            }
        }
    }
}
