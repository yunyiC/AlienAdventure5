using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    public bool isWork = true;
    public bool isPlayer = false;

    [Header("观测")]
    public float shieldNow = 0.0f;//护盾-当前
    public float bloodNow = 0.0f;//血量-当前
    public bool isDead = false;

    [Header("设置")]
    public float deadLine = -15.0f;//低于这个高度会死亡

    public float shieldMax = 100.0f;//护盾-上限

    public float shieldSpeedHeal = 40.0f;//护盾恢复速率
    public float timeNoHurtMax = 3.0f;//连续未受伤的时间-上限
    public float timeNoHurtNow = 0.0f;//连续未受伤的时间-当前

    public float bloodMax = 100.0f;//血量-上限

    public float timeDead = 0.0f;//死亡动画时间

    public delegate void function();

    public function onDead;

    public float proportionShield
    {
        get
        {
            if(shieldMax <= 0.0f)
            {
                return 0.0f;
            }
            return shieldNow / shieldMax;
        }
    }

    public float proportionBlood
    {
        get
        {
            if (bloodMax <= 0.0f)
            {
                return 0.0f;
            }
            return bloodNow / bloodMax;
        }
    }

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        onDead = OnDead;
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (isPlayer)
        {
            shieldMax = GameInfo.playerShieldMax;
            shieldNow = GameInfo.playerShield;
            bloodMax = GameInfo.playerBloodMax;
            bloodNow = GameInfo.playerBlood;
        }
        else
        {
            shieldNow = shieldMax;
            bloodNow = bloodMax;//初始状态:满血
        }
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

    private void Working()
    {
        if (!isWork)
        {
            return;
        }

        if(transform.position.y <= deadLine)
        {
            Dead();
            return;
        }

        if(timeNoHurtNow < timeNoHurtMax)
        {
            timeNoHurtNow += Time.deltaTime;
        }
        else
        {
            //长时间没受伤,护盾开始恢复
            if( ! isDead)
            {
                if (shieldNow < shieldMax)
                {
                    shieldNow += shieldSpeedHeal * Time.deltaTime;
                    if (shieldNow > shieldMax)
                    {
                        shieldNow = shieldMax;
                    }
                }
            }
        }

        if(isPlayer)
        {
            GameInfo.playerShieldMax = shieldMax;
            GameInfo.playerShield = shieldNow;
            GameInfo.playerBloodMax = bloodMax;
            GameInfo.playerBlood = bloodNow;
        }
    }

    public void Hurt(float valueHurt)
    {
        //受伤
        if (! isWork)
        {
            return;
        }

        float valueHurtT = valueHurt;
        if (valueHurtT <= 0.0f)
        {
            valueHurtT = Mathf.Abs(valueHurtT);//保证伤害值为正
        }
        else
        {
            timeNoHurtNow = 0.0f;
            bloodNow = HurtBlood(HurtShield(valueHurt));
            if ( bloodNow <= 0.0f)
            {
                isDead = true;
                Dead();
            }
        }

        if(isPlayer)
        {
            UIManager.ShowHurt();
        }
    }

    private float HurtShield(float valueHurt)
    {
        //先将伤害传给护盾,返回剩余伤害值.
        if (shieldNow > valueHurt)
        {
            shieldNow -= valueHurt;
            return 0.0f;
        }
        else
        {
            valueHurt -= shieldNow;
            shieldNow = 0.0f;
            return valueHurt;
        }
    }

    private float HurtBlood(float valueHurt)
    {
        //将伤害传给血量,返回剩余血量值.
        if (bloodNow > valueHurt)
        {
            bloodNow -= valueHurt;
        }
        else
        {
            bloodNow = 0.0f;
        }
        return bloodNow;
    }

    public void Heal(float valueHeal)
    {
        //治愈
        if (!isWork)
        {
            return;
        }

        bloodNow += valueHeal;
        if (bloodNow > bloodMax)
        {
            bloodNow = bloodMax;
        }
    }

    private void Dead()
    {
        //Debug.Log("Health死亡");
        if ( ! isWork)
        {
            return;
        }

        onDead();
        //Destroy(gameObject, timeDead);
    }

    private void OnDead()
    {
        Destroy(gameObject, 0.0f);
    }

    public void ReSet()
    {
        isDead = false;
        shieldNow = shieldMax;
        bloodNow = bloodMax;
    }
}
