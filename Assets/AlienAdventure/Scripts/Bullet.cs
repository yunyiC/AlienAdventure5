using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹
/// 记录子弹飞行速度和飞出方向
/// 用触发器检测实现造成伤害
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("预置")]
    public GameObject objSpark;     //子弹爆炸之后的特效
    public float speedFly = 20.0f;  //子弹飞行速度

    [Header("观测")]
    public Vector3 directionFly;    //子弹飞行方向
    public bool isFly = false;      //子弹是否飞行
    public float timeLive = 1.0f;   //子弹最多生存时间,防止子弹永不消失
    //public float valueHurt = 20.0f;   //子弹伤害

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
        Flying();
    }

    /// <summary>
    /// 碰撞检测
    /// 当撞击地面,爆炸
    /// 当撞击怪物,爆炸并造成伤害
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.Ground))
        {
            Boom();
            return;
        }
        else if(collider.CompareTag(Tags.Monster))
        {
            Health healthT = collider.GetComponent<Health>();
            if (healthT != null)
            {
                //healthT.Hurt(valueHurt);
                healthT.Hurt(GameInfo.playerHurt_Bullet);
                Boom();
                return;
            }
        }
    }

    /// <summary>
    /// 子弹开始飞行,代替初始化函数
    /// </summary>
    /// <param name="posTarget">子弹瞄准的目标位置</param>
    /// <param name="radiusShoot">子弹有效距离</param>
    public void Fly(Vector3 posTarget, float radiusShoot = 100.0f)
    {
        directionFly = posTarget - transform.position;
        directionFly.Normalize();
        timeLive = radiusShoot / speedFly;
        isFly = true;
        Destroy(gameObject, timeLive);
    }

    /// <summary>
    /// 飞行函数,实现子弹的移动
    /// 具有物理性质
    /// </summary>
    private void Flying()
    {
        if( ! isFly )
        {
            return;
        }

        Vector3 moveDirection = directionFly;
        float moveDistance = speedFly * Time.fixedDeltaTime;
        Vector3 move = moveDirection * moveDistance;

        transform.Translate(move);
    }

    /// <summary>
    /// 子弹爆炸,产生粒子特效,并销毁自身
    /// </summary>
    private void Boom()
    {
        if(objSpark == null)
        {
            return;
        }

        GameObject objSparkT = Instantiate(objSpark);
        objSparkT.transform.position = transform.position;
        ParticleSystem psSparkT = objSparkT.GetComponent<ParticleSystem>();
        if(psSparkT != null)
        {
            ParticleSystem.MainModule mainModule = psSparkT.main;
            Destroy(objSparkT, mainModule.duration);
        }
        else
        {
            Destroy(objSparkT, 0.0f);
        }
        Destroy(gameObject, 0.0f);
    }
}
