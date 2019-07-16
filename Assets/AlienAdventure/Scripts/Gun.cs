using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [Header("预设")]
    public bool isWork = true;
    public bool isLookAtTarget = true;
    public bool isWorkBullet = true;
    public bool isWorkFire = true;
    
    public Transform tfmMuzzle;
    public Transform tfmTarget;

    public float distanceMax = 2.0f;
    public float speedMax = 5.0f;

    public GameObject objBullet;
    public float timeBulletColdMax = 0.2f;
    public float timeBulletColdNow = 0.0f;

    public GameObject objBulletFire;
    public float timeFireColdMax = 1.0f;
    public float timeFireColdNow = 0.0f;
    public float force = 800.0f;

    public float timeReTargetMax = 0.5f;
    public float timeReTargetNow = 0.0f;

    [Header("观测")]
    public Vector3 posTarget;
    public Transform tfmPlayer;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if(tfmMuzzle == null)
        {
            tfmMuzzle = transform.Find("Muzzle");
        }

        transform.parent = null;
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (tfmPlayer == null)
        {
            tfmPlayer = PlayerController.main.transform;
        }
    }

    void Update()
    {
        if ( ! GameInfo.isPause)
        {
            Working();
        }
    }

    void LateUpdate()
    {
        if ( ! GameInfo.isPause)
        {
            
        }
    }

    void FixedUpdate()
    {
        if (!GameInfo.isPause)
        {
            LookAtTarget();
            FollowingTarget();
        }
    }

    private void Working()
    {
        if ( ! isWork)
        {
            return;
        }

        ShootingOfBullet();
        ColdDowningOfBullet();
        ShootingOfFire();
        ColdDowningOfFire();
    }

    private void FollowingTarget()
    {
        if (tfmTarget == null)
        {
            return;
        }

        timeReTargetNow += timeReTargetMax;
        if (timeReTargetNow >= timeReTargetMax)
        {
            timeReTargetNow -= timeReTargetMax;
            posTarget = tfmTarget.position;
        }

        float speedT = 0.0f;
        Vector3 moveDirectionT = posTarget - transform.position;
        float distanceT = moveDirectionT.magnitude;
        if (distanceT >= distanceMax)
        {
            speedT = speedMax;
        }
        else
        {
            speedT = (distanceT / distanceMax) * speedMax;
        }
        float moveDistanceT = speedT * Time.fixedDeltaTime;
        Vector3 moveT = moveDirectionT * moveDistanceT;
        transform.position = transform.position + moveT;
    }

    private void ShootingOfBullet()
    {
        if ( ! isWorkBullet)
        {
            return;
        }

        if (objBullet == null)
        {
            isWorkBullet = false;
            return;
        }

        if (timeBulletColdNow > 0.0f)
        {
            return;
        }

        if (InputM.MouseL)
        {
            ShootOfBullet();
        }
    }

    private void ShootingOfFire()
    {
        if ( ! isWorkFire)
        {
            return;
        }

        if (objBulletFire == null)
        {
            isWorkFire = false;
            return;
        }

        if (timeFireColdNow > 0.0f)
        {
            return;
        }

        if (InputM.MouseR)
        {
            ShootOfFire();
        }
    }

    private void LookAtTarget()
    {
        if ( ! isWork)
        {
            return;
        }

        if ( ! isLookAtTarget)
        {
            return;
        }

        Vector3 forwardT = GameInfo.posAimed - transform.position;
        if (forwardT.magnitude > 0.01f)
        {
            Quaternion rotationT = Quaternion.LookRotation(forwardT);
            transform.rotation = rotationT;
        }
    }

    private void ShootOfBullet()
    {
        //Debug.Log("子弹目标位置：" + posTarget);
        GameObject objBulletT = Instantiate(objBullet);
        if (objBulletT != null)
        {
            Bullet bulletT = objBulletT.GetComponent<Bullet>();
            if (bulletT != null)
            {
                if (tfmMuzzle != null)
                {
                    objBulletT.transform.position = tfmMuzzle.position;
                }
                else
                {
                    objBulletT.transform.position = transform.position;
                }
                bulletT.Fly(GameInfo.posAimed, GameInfo.playerShootDistance);
            }
            else
            {
                Destroy(objBulletT, 0.0f);
            }
        }

        timeBulletColdNow = timeBulletColdMax;
    }

    private void ShootOfFire()
    {
        //Debug.Log("子弹目标位置：" + posTarget);
        GameObject objBulletFireT = Instantiate(objBulletFire);
        if (objBulletFireT != null)
        {
            Transform tfmBulletFireT = objBulletFireT.transform;
            if (tfmBulletFireT != null)
            {
                tfmBulletFireT.position = transform.position;
            }

            BulletFire bulletFireT = objBulletFireT.GetComponent<BulletFire>();
            if (bulletFireT != null)
            {

            }
            else
            {
                Destroy(objBulletFireT, 0.0f);
            }

            Rigidbody rgBulletFire = objBulletFireT.GetComponent<Rigidbody>();
            if (rgBulletFire != null)
            {
                Vector3 vecForceT = GameInfo.posAimed - tfmMuzzle.position;
                vecForceT = vecForceT.normalized * force;
                rgBulletFire.AddForce(vecForceT);
            }
        }

        timeFireColdNow = timeFireColdMax;
    }

    private void ColdDowningOfBullet()
    {
        if(timeBulletColdNow > 0.0f)
        {
            timeBulletColdNow -= Time.deltaTime;
        }
    }

    private void ColdDowningOfFire()
    {
        if (timeFireColdNow > 0.0f)
        {
            timeFireColdNow -= Time.deltaTime;
        }
    }

 
}
