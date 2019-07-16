using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teach03 : Teach
{
    public bool isWork = true;

    public GameObject objTeach04;

    public string[] strDialogues = new string[]
    {
        "继续,高级操作",
        "鼠标左键:射击",
        "鼠标右键:发射燃烧弹",
    };

    public bool isMouseLeft = false;
    public bool isMouseRight = false;

    void Awake()
    {
        
    }

    void Start()
    {
        Debug.Log("Teach03.Start");
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

        if (!isMouseLeft)
        {
            if (InputM.MouseDownL)
            {
                isMouseLeft = true;
            }
        }

        if (!isMouseRight)
        {
            if (InputM.MouseDownR)
            {
                isMouseRight = true;
            }
        }

        if (isMouseLeft && isMouseRight)
        {
            ToNextTeach();
        }
    }

    private void ToNextTeach()
    {
        objTeach04.SetActive(true);
        gameObject.SetActive(false);
    }
}
