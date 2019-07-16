using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthShow : MonoBehaviour
{
    public bool isWork = true;

    public Health health;

    public Transform tfmTipShield;
    public Transform tfmTipBlood;

    public Vector3 scaleTShield;
    public Vector3 scaleTBlood;

    public Transform tfmCamera;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
            if (health == null)
            {
                isWork = false;
                return;
            }
        }

        if (tfmTipShield == null)
        {
            isWork = false;
            return;
        }

        if (tfmTipBlood == null)
        {
            isWork = false;
            return;
        }

        scaleTShield = tfmTipShield.localScale;
        scaleTBlood = tfmTipBlood.localScale;
    }

    void Start()
    {
        tfmCamera = Camera.main.transform;
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

        tfmTipShield.localScale = GetNewScale(scaleTShield, health.proportionShield);
        tfmTipBlood.localScale = GetNewScale(scaleTBlood, health.proportionBlood);

        LookingAtPlayer();
    }

    private void LookingAtPlayer()
    {
        if (tfmCamera == null)
        {
            return;
        }

        Vector3 lookForwardT;
        lookForwardT = tfmCamera.transform.position - this.transform.position;
        float angleLook = Mathf.Atan2(lookForwardT.x, lookForwardT.z) * 180.0f / Mathf.PI;
        transform.rotation = Quaternion.Euler(0.0f, angleLook, 0.0f);
    }

    private Vector3 GetNewScale(Vector3 scaleT, float percentage)
    {
        //百分比必须介于0和1之间
        if( percentage < 0.0f || percentage > 1.0f )
        {
            return Vector3.zero;
        }

        Vector3 scaleR = scaleT;
        scaleR.x *= percentage;
        return scaleR;
    }
}
