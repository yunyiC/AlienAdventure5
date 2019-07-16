using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crystal : MonoBehaviour
{
    public int value = 1;
    public float speedRotate = 90.0f;

    void Awake()
    {
        
    }

    void Start()
    {
        transform.position += new Vector3(0.0f, 0.5f, 0.0f);
    }

    void Update()
    {
        Vector3 eulRotate = new Vector3(0.0f, speedRotate * Time.deltaTime, 0.0f);
        transform.Rotate(eulRotate);
    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
