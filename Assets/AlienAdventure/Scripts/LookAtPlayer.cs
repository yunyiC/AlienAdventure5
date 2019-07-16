using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtPlayer : MonoBehaviour
{
    public Transform tfmCamera;

    void Awake()
    {
        
    }

    void Start()
    {
        tfmCamera = Camera.main.transform;
    }

    void Update()
    {
        LookingAtPlayer();
    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        
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
}
