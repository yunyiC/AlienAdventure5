using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : MonoBehaviour
{
    public bool isWork = true;
    public bool isShowTip = false;

    public LevelManager.Levels levelTo = LevelManager.Levels.LevelBase;

    public MTrigger[] triggers = null;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (!isWork)
        {
            return;
        }

        if (triggers == null || triggers.Length < 1)
        {
            triggers = GetComponentsInChildren<MTrigger>();
        }
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (!isWork)
        {
            return;
        }

        if (triggers != null)
        {
            foreach (MTrigger triggerT in triggers)
            {
                triggerT.onEnter = OnEnter;
                triggerT.onStay = OnStay;
                triggerT.onExit = OnExit;
            }
        }

        SetTipShow(false);
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
        if( ! isWork)
        {
            return;
        }

        if (isShowTip && Input.GetKeyDown(KeyCode.E))
        {
            LoadNewLevel();
        }
    }

    private void LoadNewLevel()
    {
        if (!isWork)
        {
            return;
        }

        LevelManager.LoadLevel(levelTo);
    }

    private void OnEnter(Collider collider)
    {
        if (!isWork)
        {
            return;
        }

        if (collider.CompareTag(Tags.Player))
        {
            SetTipShow(true);
        }
    }

    private void OnStay(Collider collider)
    {
        if (!isWork)
        {
            return;
        }

        if (collider.CompareTag(Tags.Player))
        {
            //SetTipShow(true);
        }
    }

    private void OnExit(Collider collider)
    {
        if (!isWork)
        {
            return;
        }

        if (collider.CompareTag(Tags.Player))
        {
            SetTipShow(false);
        }
    }

    private void SetTipShow(bool newState)
    {
        //Debug.Log("SetWork" + newState);
        isShowTip = newState;
        if(isShowTip)
        {
            UIManager.TipKey = "按[E]进入 " + LevelManager.GetLevelNameC(levelTo);
        }
        else
        {
            UIManager.TipKey = null;
        }
    }
}
