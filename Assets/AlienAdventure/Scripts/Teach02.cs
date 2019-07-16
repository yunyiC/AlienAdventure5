using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teach02 : Teach
{
    public bool isWork = true;

    public GameObject objTeach03;

    public string[] strDialogues = new string[]
    {
        "然后,进阶操作",
        "左Shift:跑步",
        "空格键(Space):跳跃",
    };

    public bool isLeftShift = false;
    public bool isSpace = false;

    void Awake()
    {
        
    }

    void Start()
    {
        Debug.Log("Teach02.Start");
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
        if (!isWork)
        {
            return;
        }

        if (!isLeftShift)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isLeftShift = true;
            }
        }

        if (!isSpace)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpace = true;
            }
        }

        if (isLeftShift && isSpace)
        {
            ToNextTeach();
        }
    }

    private void ToNextTeach()
    {
        objTeach03.SetActive(true);
        gameObject.SetActive(false);
    }
}
