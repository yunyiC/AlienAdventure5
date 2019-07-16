using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 火焰弹,爆炸并放出火焰
/// </summary>
public class BulletFire : MonoBehaviour
{
    public GameObject objEffectBoom;    //爆炸时特效
    public GameObject objEffectFire;    //火焰特效

    public float timeLiveMax = 0.3f;    //最长生存时间
    public float timeLiveNow;           //当前生存时间

    public int numFire = 5;             //爆炸产生火焰的数量
    public float flyForce = 1.0f;       //发射时所受力的大小
    public float flyRadius = 1.0f;      //爆炸水平半径
    public float flyHeightMin = 0.1f;   //爆炸垂直高度最小值
    public float flyHeightMax = 1.0f;   //爆炸垂直高度最大值

    void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// 用于初始化自身数值
    /// </summary>
    private void Init()
    {
        timeLiveNow = 0.0f;
    }

    void Start()
    {
        Init2();
    }

    /// <summary>
    /// 初始化2
    /// 用于初始化其他对象的数值
    /// </summary>
    private void Init2()
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
    /// 当碰到地面
    /// 爆炸
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.Ground))
        {
            Boom();
        }
    }

    /// <summary>
    /// 飞行
    /// 受重力影响
    /// </summary>
    private void Flying()
    {
        timeLiveNow += Time.fixedDeltaTime;
        if (timeLiveNow >= timeLiveMax)
        {
            Boom();
        }
    }

    /// <summary>
    /// 爆炸
    /// 当遇到地面时
    /// </summary>
    private void Boom()
    {
        if (objEffectBoom != null)
        {
            GameObject objEffectT = Instantiate(objEffectBoom);
            if (objEffectT != null)
            {
                objEffectT.transform.position = transform.position;
            }
        }

        if (objEffectFire != null)
        {
            for (int i = 0; i < numFire; i++)
            {
                GameObject objEffectT = Instantiate(objEffectFire);
                if (objEffectT != null)
                {
                    objEffectT.transform.position = transform.position;
                    Rigidbody rgFireT = objEffectT.GetComponent<Rigidbody>();
                    if (rgFireT != null)
                    {
                        Vector3 forceT = new Vector3
                        {
                            x = Random.Range( (-1) * flyRadius, flyRadius ),
                            y = Random.Range(flyHeightMin, flyHeightMax),
                            z = Random.Range((-1) * flyRadius, flyRadius),
                        };
                        forceT = forceT * flyForce;
                        objEffectT.transform.Translate(forceT);
                        rgFireT.AddForce(forceT);
                    }
                }
            }
        }

        Destroy(gameObject, 0.0f);
    }


}
