using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratorController : MonoBehaviour
{
    public bool isWork = true;
    public bool isElectric = true;
    public bool isShowTip = false;

    public MTrigger trigger;
    public GameObject objEffectElectric;
    public Transform tfmMessage;

    public float speedMessageRotate = 10.0f;
    public float rateElectric = 0.2f;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        float numRandomT = Random.Range(0.0f, 1.0f);
        isElectric = (numRandomT < rateElectric) ? true : false;
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
            trigger.onExit = OnExit;
        }

        if (objEffectElectric != null)
        {
            objEffectElectric.SetActive(isElectric);
        }
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

        if (isShowTip)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isShowTip = false;
                UIManager.TipKey = null;

                isElectric = true;
                if (objEffectElectric != null)
                {
                    objEffectElectric.SetActive(true);
                    GeneratorManager.Check();
                }
            }
        }

        if (tfmMessage != null)
        {
            float angleRotateT = speedMessageRotate * Time.deltaTime;
            tfmMessage.Rotate(Vector3.up, angleRotateT, Space.Self);
        }
    }

    private void OnEnter(Collider collider)
    {
        if (!isWork)
        {
            return;
        }

        if (isElectric)
        {
            return;
        }

        if (collider.CompareTag(Tags.Player))
        {
            isShowTip = true;
            UIManager.TipKey = "按[E]激活电球";
        }
    }

    private void OnExit(Collider collider)
    {
        if (!isWork)
        {
            return;
        }

        if (isElectric)
        {
            return;
        }

        if (collider.CompareTag(Tags.Player))
        {
            if (collider.CompareTag(Tags.Player))
            {
                isShowTip = false;
                UIManager.TipKey = null;
            }
        }
    }
}
