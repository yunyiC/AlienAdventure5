using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    public bool isWork = true;

    public bool isForOnce = true;

    public MTrigger trigger;
    public string strTag = Tags.Player;

    public string[] strDialogue;

    void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (trigger != null)
        {
            trigger.onEnter = OnEnter;
        }
        else
        {
            Debug.Log("trigger = null");
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

    private void OnEnter(Collider collider)
    {
        //Debug.Log("Dialogue:OnEnter");
        if ( ! isWork)
        {
            return;
        }

        if ( collider.CompareTag(strTag) )
        {
            //Debug.Log("检测到玩家进入对话区域");
            UIEcho.AddDialogues(strDialogue);
            if (isForOnce)
            {
                isWork = false;
            }
        }
    }
}
