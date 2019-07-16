using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireController : MonoBehaviour
{
    public bool isWork = true;

    public MTrigger trigger;
    public string strTag = Tags.Monster;

    public float timeLiveMin = 3.0f;
    public float timeLiveMax = 9.0f;
    public float timeLive;

    void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (!isWork)
        {
            return;
        }

        if (trigger != null)
        {
            trigger.onStay = OnStay;
        }

        timeLive = Random.Range(timeLiveMin, timeLiveMax);

        Destroy(gameObject, timeLive);
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

    private void OnStay(Collider collider)
    {
        if ( collider.CompareTag(strTag) )
        {
            Health healthT = collider.GetComponent<Health>();
            if (healthT != null)
            {
                float valueHurtT = GameInfo.playerHurt_Fire;
                healthT.Hurt(valueHurtT);
            }
        }
    }
}
