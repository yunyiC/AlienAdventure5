using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teach01 : Teach
{
    public bool isWork = true;

    public GameObject objTeach02;

    public string[] strDialogues = new string[]
    {
        "欢迎来到虚拟训练场!",
        "我将教你熟悉基本操作",
        "W:向前移动",
        "S:向后移动",
        "A:向左移动",
        "D:向右移动",
    };

    public bool isW = false;
    public bool isS = false;
    public bool isA = false;
    public bool isD = false;

    void Awake()
    {
        
    }

    void Start()
    {
        Debug.Log("Teach01.Start");
        UIEcho.AddDialogues(strDialogues);
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
        if ( ! isWork)
        {
            return;
        }

        if( ! isW)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isW = true;
            }
        }

        if ( ! isS)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isS = true;
            }
        }

        if ( ! isA)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                isA = true;
            }
        }

        if ( ! isD)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                isD = true;
            }
        }

        if (isW && isS && isA && isD)
        {
            ToNextTeach();
        }
    }

    private void ToNextTeach()
    {
        objTeach02.SetActive(true);
        gameObject.SetActive(false);
    }
}
