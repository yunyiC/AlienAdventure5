using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController main;

    public Transform tfmTest;

    public Health health;

    public Gun gun;
    public SoundController soundController;
    public CrystalPickUp crystalPickUp;

    public bool isWork_GunBullet = true;
    public bool isWork_GunFire = true;
    public bool isWork_SoundController = true;
    public bool isWork_CrystalPickUp = true;

    public GameObject objSoundController;
    public GameObject objCrystalPickUp;

    public float distanceShoot
    {
        get
        {
            return GameInfo.playerShootDistance;
        }
        set
        {
            GameInfo.playerShootDistance = value;
        }
    }
    public LayerMask layerShoot = Layers.Ground | Layers.Monster;

    void Awake()
    {
        main = this;
        Init();
    }

    private void Init()
    {
        if(gun != null)
        {
            gun.isWorkBullet = isWork_GunBullet;
            gun.isWorkFire = isWork_GunFire;
        }
        if (soundController != null)
        {
            soundController.isWork = isWork_SoundController;
            objSoundController = soundController.gameObject;
            objSoundController.SetActive(isWork_SoundController);
        }
        if (crystalPickUp != null)
        {
            crystalPickUp.isWork = isWork_CrystalPickUp;
            objCrystalPickUp = crystalPickUp.gameObject;
            objCrystalPickUp.SetActive(isWork_CrystalPickUp);
        }
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (health != null)
        {
            health.onDead = OnDead;
        }
    }

    void Update()
    {

    }

    void LateUpdate()
    {

    }

    void FixedUpdate()
    {
        RefreshAimTarget();
    }

    private void RefreshAimTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distanceShoot, layerShoot))
        {
            GameInfo.posAimed = hitInfo.point;
        }
        else
        {
            if (Camera.main != null)
            {
                GameInfo.posAimed = Camera.main.transform.position + Camera.main.transform.forward * distanceShoot;
            }
            else
            {
                GameInfo.posAimed = this.transform.position + this.transform.forward * distanceShoot;
            }
        }

        if (tfmTest != null)
        {
            tfmTest.position = GameInfo.posAimed;
        }
    }

    public void OnDead()
    {
        Relive();
    }

    public void Relive()
    {
        health.ReSet();

        GameObject objRespawn = RespawnManager.GetRespawnNearest(transform);
        if(objRespawn != null)
        {
            transform.position = objRespawn.transform.position;
            transform.rotation = objRespawn.transform.rotation;
        }
        else
        {
            Vector3 posPlayerT = transform.position;
            posPlayerT.y = 1.0f;
            transform.position = posPlayerT;
        }
    }
}
