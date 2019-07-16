using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrystalPickUp : MonoBehaviour
{
    public bool isWork = true;

    public string strTag = Tags.Crystal;

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

    void OnTriggerEnter(Collider collider)
    {
        if(!isWork)
        {
            return;
        }

        if(collider.CompareTag(strTag))
        {
            Crystal crystalT = collider.GetComponent<Crystal>();
            if(crystalT == null)
            {
                Destroy(collider.gameObject, 0.0f);
            }
            else
            {
                Destroy(collider.gameObject, 0.0f);
                GameInfo.playerCrystal += crystalT.value;
            }
        }
    }
}
