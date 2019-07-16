using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterController : MonoBehaviour
{
    public NavMeshAgent agentMonster;//AI自动寻路组件
    public Animator animMonster;//动画机
    public BoxCollider cldMonster;
    public Health health;

    public GameObject objTarget;

    public bool isDead = false;
    public bool isAttack = false;
    public bool isWalk = false;
    public bool isRest = false;

    public MonsterCreator cMonster;

    public enum StateMonster
    {
        REST,       //休息,等待,观察等不需要动的情况
        PATROL,     // 巡逻
        TRACK,      //追踪
        ATTACK,     //攻击
        DEAD,       //死亡
    }

    public float distanceWithPlayer;

    public StateMonster state = StateMonster.PATROL;//怪物当前的状态

    public Vector3 posTarget;//怪物的追踪目标
    public Health healthTarget;//进入领域的可攻击生物
    public Vector3 positionStart;//怪物出生的位置,也是怪物要守护的地点

    public float timeDeadDispear = 5.0f;

    public float radiusPatrol = 10.0f;//怪物发现新目标的有效距离
    public float radiusTrack = 5.0f;//怪物追踪目标的有效距离
    public float radiusAttack = 2.0f;//怪物攻击玩家的有效距离

    public float patientMax = 3.0f;//怪物追踪玩家的耐心上限
    public float patientNow = 0.0f;//怪物追踪玩家的耐心-当前

    public float timeAttackMax = 0.2f;//怪物攻击一次的积累时间-上限
    public float timeAttackNow = 0.0f;//怪物攻击一次的积累时间-当前

    public float timeRestMax = 2.0f;//怪物休息/观察的时间-上限
    public float timeRestMin = 0.5f;//怪物休息/观察的时间-下限
    public float timeRestNow = 0.0f;//怪物休息/观察的时间-当前

    public float valueHurt = 10.0f;

    public Vector3 positionLast;

    public LayerMask layerGround = 1 << Layers.Ground;

    public float distanceToPlayer
    {
        get
        {
            if (healthTarget == null)
            {
                distanceWithPlayer = float.MaxValue;
            }
            else
            {
                distanceWithPlayer = Vector3.Distance(transform.position, healthTarget.transform.position);
            }

            return distanceWithPlayer;
        }
    }

    public float distanceToTarget
    {
        get
        {
            return Vector3.Distance(transform.position, posTarget);
        }
    }

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (agentMonster == null)
        {
            agentMonster = GetComponent<NavMeshAgent>();
            if (agentMonster == null)
            {
                Debug.Log("没有设置怪物AI组件！");
            }
        }
        if (cldMonster == null)
        {
            cldMonster = GetComponent<BoxCollider>();
            if (cldMonster == null)
            {
                Debug.Log("没有设置怪物碰撞器组件！");
            }
        }
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (agentMonster != null)
        {
            agentMonster.enabled = true;
        }
        if (health == null)
        {
            health = GetComponent<Health>();
        }
        if (health != null)
        {
            health.onDead = OnDead;
        }

        posTarget = GetPosTargetRandom();
        if (objTarget != null)
        {
            objTarget.transform.position = posTarget;
        }

        if (cMonster == null)
        {
            positionStart = transform.position;
        }

        radiusAttack = Mathf.Min(radiusAttack, radiusTrack);//攻击距离小于等于追踪距离
        radiusTrack = Mathf.Min(radiusTrack, radiusPatrol);//追踪距离小于等于巡逻距离
        timeRestMin = Mathf.Min(timeRestMin, timeRestMax);

        StateChange(StateMonster.TRACK);
    }

    void Update()
    {
        Moving();
    }

    void LateUpdate()
    {
        LookAtTarget();
    }

    void FixedUpdate()
    {
        
    }

    public void Initialize()
    {
        Init();
        //Init2();
    }

    private void Moving()
    {
        switch (state)
        {
            case StateMonster.REST:
                Resting();
                break;
            case StateMonster.PATROL:
                Patroling();
                break;
            case StateMonster.TRACK:
                Tracking();
                break;
            case StateMonster.ATTACK:
                Attacking();
                break;
            case StateMonster.DEAD:
                //Do Noing
                break;
            default:
                break;
        }
    }

    private void LookAtTarget()
    {
        if(healthTarget == null)
        {
            return;
        }

        Vector3 lookForwardT;
        lookForwardT = healthTarget.transform.position - this.transform.position;
        float angleLook = Mathf.Atan2(lookForwardT.x, lookForwardT.z) * 180.0f / Mathf.PI;
        transform.rotation = Quaternion.Euler(0.0f, angleLook, 0.0f);
    }

    public void StateChange(StateMonster newState)
    {
        state = newState;
        switch (state)
        {
            case StateMonster.REST:
                timeRestNow = GetTimeRestRandom();
                isRest = true;
                isWalk = false;
                if(animMonster != null)
                {
                    animMonster.SetBool("isRest", isRest);
                    animMonster.SetBool("isWalk", isWalk);
                }
                break;
            case StateMonster.PATROL:
                patientNow = patientMax;
                isRest = false;
                isWalk = true;
                isAttack = false;
                if (animMonster != null)
                {
                    animMonster.SetBool("isRest", isRest);
                    animMonster.SetBool("isWalk", isWalk);
                    animMonster.SetBool("isAttack", isAttack);
                }
                break;
            case StateMonster.TRACK:
                patientNow = patientMax;
                isRest = false;
                isWalk = true;
                isAttack = false;
                if (animMonster != null)
                {
                    animMonster.SetBool("isRest", isRest);
                    animMonster.SetBool("isWalk", isWalk);
                    animMonster.SetBool("isAttack", isAttack);
                }
                break;
            case StateMonster.ATTACK:
                isAttack = true;
                if (animMonster != null)
                {
                    animMonster.SetBool("isAttack", isAttack);
                }
                break;
            case StateMonster.DEAD:
                isDead = true;
                if (animMonster != null)
                {
                    animMonster.SetBool("isDead", isDead);
                }
                break;
            default:
                break;
        }
    }

    //休息,等待,观察等不需要动的情况
    private void Resting()
    {
        //Debug.Log("Resting");

        timeRestNow -= Time.deltaTime;
        if (timeRestNow <= 0.0f)
        {
            //休息时间结束,寻找一个新目标,继续巡逻
            posTarget = GetPosTargetRandom();
            if (objTarget != null)
            {
                objTarget.transform.position = posTarget;
            }
            StateChange(StateMonster.PATROL);
        }

        if (healthTarget != null)
        {
            //玩家进入领域
            StateChange(StateMonster.TRACK);
            return;
        }
    }

    //巡逻
    private void Patroling()
    {
        //Debug.Log("Patroling");

        SetTarget(posTarget);

        float moveSpeedT = (transform.position - positionLast).magnitude;
        if (moveSpeedT < 0.5f)
        {
            patientNow -= Time.deltaTime;
            if (patientNow <= 0.0f)
            {
                StateChange(StateMonster.REST);
            }
        }
        positionLast = transform.position;

        if (distanceToTarget < radiusAttack)
        {
            //巡逻到目标处,进行休息/观察
            StateChange(StateMonster.REST);
        }
        if (healthTarget != null)
        {
            //玩家进入领域
            StateChange(StateMonster.TRACK);
        }
    }


    //追踪
    private void Tracking()
    {
        //Debug.Log("Tracking");

        if (healthTarget == null)
        {
            //Debug.Log("失去跟踪目标!");
            StateChange(StateMonster.PATROL);
            return;
        }
        //耐心下降
        patientNow -= Time.deltaTime;
        if (patientNow <= 0.0f)
        {
            //Debug.Log("失去耐心!");
            healthTarget = null;
            StateChange(StateMonster.PATROL);
        }
        else
        {
            SetTarget(healthTarget.transform.position);
        }

        if (distanceToPlayer <= radiusAttack)
        {
            //Debug.Log("切换攻击状态!");
            StateChange(StateMonster.ATTACK);
        }
        else
        {
            //Debug.Log("保持追踪状态!" + distanceToPlayer);
        }
    }

    //攻击
    private void Attacking()
    {
        //Debug.Log("Attacking");

        //先完成这个状态该做的事情
        timeAttackNow += Time.deltaTime;
        Attack();

        //检测状态切换
        if (distanceToPlayer > radiusAttack)
        {
            //玩家离开攻击范围
            //Debug.Log("玩家离开攻击范围");
            StateChange(StateMonster.TRACK);
            timeAttackNow = 0.0f;
        }
    }

    private void Attack()
    {
        if(healthTarget == null)
        {
            Debug.Log("目标丢失!");
            return;
        }

        /*
        float hurtValueT = valueHurt * Time.deltaTime;
        healthTarget.Hurt(hurtValueT);
        */

        /**/
        if (timeAttackNow < timeAttackMax)
        {
            return;
        }

        timeAttackNow -= timeAttackMax;
        healthTarget.Hurt(valueHurt);
    }

    private void SetTarget(Vector3 positionTarget)
    {
        if (agentMonster.isActiveAndEnabled)
        {
            agentMonster.SetDestination(positionTarget);
        }
        else
        {
            Debug.Log("导航没有开启!");
        }
    }

    public void SetTarget(Health healthOther)
    {
        //Debug.Log("听到玩家的声音!");

        healthTarget = healthOther;
        patientNow = patientMax;
    }

    private Vector3 GetPosTargetRandom()
    {
        Vector3 posTargetT = transform.position;
        Vector3 posOriginT = new Vector3(
            positionStart.x + Random.Range(-radiusPatrol, radiusPatrol),
            positionStart.y + 5.0f,
            positionStart.z + Random.Range(-radiusPatrol, radiusPatrol)
            );
        //Debug.Log("原点 " + posOriginT);

        Ray rayT = new Ray(posOriginT, Vector3.down);
        float maxDistanceT = 100.0f;

        if (Physics.Raycast(rayT, out RaycastHit hitInfoT, maxDistanceT, layerGround))
        {
            //Debug.Log("获取新的目标点");
            posTargetT = hitInfoT.point;
            Debug.DrawLine(posOriginT, posTargetT, Color.red);
        }
        else
        {
            Debug.Log("使用旧的目标点");
        }

        return posTargetT;
    }

    private float GetTimeRestRandom()
    {
        return Random.Range(timeRestMin, timeRestMax);
    }

    private void SetColor(Color newColor)
    {

    }

    private void OnDead()
    {
        if(cMonster != null)
        {
            cMonster.numMonsterNow--;
        }

        GameObject objDropT = DropsManager.GetObjDropRandom();
        if (objDropT != null)
        {
            objDropT.transform.position = transform.position;
        }

        healthTarget = null;
        tag = Tags.Untagged;
        StateChange(StateMonster.DEAD);
        Destroy(gameObject, timeDeadDispear);

        if(cldMonster != null)
        {
            cldMonster.enabled = false;
        }

        if(agentMonster != null)
        {
            agentMonster.enabled = false;
        }
        
    }
}
