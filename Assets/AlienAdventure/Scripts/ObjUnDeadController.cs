using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjUnDeadController : MonoBehaviour
{
    public MeshRenderer mesh;
    public Health health;

    public float radius = 2.0f;

    void Awake()
    {
        
    }

    void Start()
    {
        if (mesh != null)
        {
            Material materialT = MaterialManager.GetMaterialRandom();
            if (materialT != null)
            {
                mesh.material = materialT;
            }
        }

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
        
    }

    private void OnDead()
    {
        Debug.Log("方块被破坏!");

        Vector3 forceT = new Vector3()
        {
            x = Random.Range((-1) * radius, radius),
            y = Random.Range((0.5f) * radius, radius),
            z = Random.Range((-1) * radius, radius),
        };
        transform.position = transform.position + forceT;

        if (health != null)
        {
            health.ReSet();
            health.bloodMax += GameInfo.playerHurt_Bullet;
            health.bloodNow = health.bloodMax;
        }

        if (mesh != null)
        {
            Material materialT = MaterialManager.GetMaterialRandom();
            if (materialT != null)
            {
                mesh.material = materialT;
            }
        }

        /*
        GameObject objExplosiveCubeT = Instantiate(gameObject, transform.parent);
        if (objExplosiveCubeT != null)
        {
            Transform transformT = objExplosiveCubeT.GetComponent<Transform>();
            if (transformT != null)
            {
                Vector3 forceT = new Vector3()
                {
                    x = Random.Range((-1) * radius, radius),
                    y = Random.Range((-1) * radius, radius),
                    z = Random.Range((-1) * radius, radius),
                };
                transformT.position = transform.position + forceT;
            }
        }
        */
        /*
        GameObject objExplosiveCubeT = Instantiate(gameObject, transform.parent);
        if (objExplosiveCubeT != null)
        {
            Transform transformT = objExplosiveCubeT.GetComponent<Transform>();
            if (transformT != null)
            {
                Vector3 forceT = new Vector3()
                {
                    x = Random.Range( (-1) * radius, radius ),
                    y = Random.Range((-1) * radius, radius),
                    z = Random.Range((-1) * radius, radius),
                };
                transformT.position = transform.position + forceT;
            }
            Health healthT = objExplosiveCubeT.GetComponent<Health>();
            if (healthT != null)
            {
                healthT.ReSet();
                healthT.shieldMax *= 2;
                healthT.shieldNow = healthT.shieldMax;
                healthT.bloodMax *= 2;
                healthT.bloodNow = healthT.bloodMax;
            }
        }
        */
    }
}
