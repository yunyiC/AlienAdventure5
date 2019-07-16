using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialManager : MonoBehaviour
{
    public static MaterialManager main;

    public Material[] materials;

    void Awake()
    {
        MaterialManager.main = this;
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
        
    }

    public static Material GetMaterialRandom()
    {
        if (main != null && main.materials.Length > 0)
        {
            int nT = Random.Range(0, main.materials.Length - 1);
            return main.materials[nT];
        }
        else
        {
            return null;
        }
    }
}
