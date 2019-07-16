using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shelf : MonoBehaviour
{
    public bool isWork = true;
    public int id = -1;
    public GameObject objCommodity;//货物
    public float speedRotate = 90.0f;
    public bool isTip = false;//是否正在展示提示,也是判断玩家是否可以按E键购买的关键
    public int valueWithCrystal = 20;

    [Header("购买效果")]
    public bool isHurtAdd_Bullet = false;
    public float numHurtAdd_Bullet = 20.0f;
    


    void Awake()
    {
        if(gameObject == null)
        {
            isWork = false;
        }
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

    void OnTriggerEnter(Collider collider)
    {
        if(!isWork)
        {
            return;
        }

        if(collider.CompareTag(Tags.Player))
        {
            SetTipPurchase(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!isWork)
        {
            return;
        }

        if (collider.CompareTag(Tags.Player))
        {
            SetTipPurchase(false);
        }
    }

    private void Working()
    {
        if(!isWork)
        {
            return;
        }

        float angleRotate = speedRotate * Time.deltaTime;
        objCommodity.transform.Rotate(Vector3.up, angleRotate);

        if(isTip)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                BePurchase();
            }
        }
    }

    private void BePurchase()
    {
        if(GameInfo.playerCrystal < valueWithCrystal)
        {
            //Debug.Log("玩家没有足够水晶!");
            UIManager.TipWarning = "你的水晶不够啊!";
            return;
        }

        GameInfo.playerCrystal = GameInfo.playerCrystal - valueWithCrystal;
        if (isHurtAdd_Bullet)
        {
            GameInfo.playerHurt_Bullet = GameInfo.playerHurt_Bullet + numHurtAdd_Bullet;
            UIManager.TipWarning = "攻击力增加";
        }

        SetSold();
    }
    
    private void SetTipPurchase(bool newState)
    {
        isTip = newState;
        if(isTip)
        {
            UIManager.TipKey = "按[E]购买" + "($" + valueWithCrystal + ")";
        }
        else
        {
            UIManager.TipKey = null;
        }
    }

    public void SetSold()
    {
        SetTipPurchase(false);
        isWork = false;
        objCommodity.SetActive(false);
        if(id > -1)
        {
            StoreInfo.isShiefSolds[id] = true;
        }
    }
}
