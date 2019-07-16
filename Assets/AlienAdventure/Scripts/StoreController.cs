using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoreController : MonoBehaviour
{
    public Shelf[] shelfs;
    public Crystal crystalFree;
    public bool isCheck = true;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        GameObject[] objShelfs = GameObject.FindGameObjectsWithTag(Tags.Shelf);
        if(objShelfs == null)
        {
            return;
        }

        shelfs = new Shelf[objShelfs.Length];
        for(int i=0;i<objShelfs.Length;i++)
        {
            shelfs[i] = objShelfs[i].GetComponent<Shelf>();
            shelfs[i].id = i;
        }
        if ( ! StoreInfo.isInited)
        {
            StoreInfo.Init(shelfs.Length);
        }
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if(shelfs == null)
        {
            return;
        }

        for (int i=0;i<shelfs.Length;i++)
        {
            if(StoreInfo.isShiefSolds[i])
            {
                shelfs[i].SetSold();
            }
        }

        if(StoreInfo.isPicked)
        {
            Destroy(crystalFree.gameObject,0.0f);
            isCheck = false;
        }
    }

    void Update()
    {
        Checking();
    }

    void LateUpdate()
    {

    }

    void FixedUpdate()
    {

    }

    private void Checking()
    {
        if( ! isCheck)
        {
            return;
        }

        if(crystalFree == null)
        {
            StoreInfo.isPicked = true;
            isCheck = false;
        }
    }
}
