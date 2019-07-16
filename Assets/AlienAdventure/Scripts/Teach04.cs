using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teach04 : Teach
{
    public bool isWork = true;

    public string[] strDialogues = new string[]
    {
        "很好,你已经掌握了所有操作",
        "按[E]进入下一关",
    };

    public bool isE = false;

    void Awake()
    {

    }

    void Start()
    {
        Debug.Log("Teach04.Start");
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

        if (!isE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isE = true;
            }
        }

        if (isE)
        {
            ToNextTeach();
        }
    }

    private void ToNextTeach()
    {
        LevelManager.LoadLevel(LevelManager.Levels.Level1);
    }
}
