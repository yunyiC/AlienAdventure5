using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISight : MonoBehaviour
{
    public bool isWork = true;
    public bool IsWork
    {
        get
        {
            return isWork;
        }
        set
        {
            isWork = value;
            gameObject.SetActive(isWork);
        }
    }

    public RectTransform tfmSight;

    public Vector2 sizeMin = new Vector2(60, 36);
    public Vector2 sizeMax = new Vector2(180, 108);
    public float distanceMin = 2.0f;
    public float speedLevelChange;
    public float distanceMax
    {
        get
        {
            return GameInfo.playerShootDistance;
        }
        set
        {
            GameInfo.playerShootDistance = value;
        }
    }

    public Transform tfmPlayer;
    public float DistanceWithTarget
    {
        get
        {
            if ( (tfmPlayer == null) || (GameInfo.posAimed == null) )
            {
                return float.MaxValue;
            }
            else
            {
                return Vector3.Distance(tfmPlayer.position, GameInfo.posAimed);
            }
        }
    }

    public float levelTarget;
    public float levelNow;

    public Vector2 sizeNow;


    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (tfmSight == null)
        {
            tfmSight = GetComponent<RectTransform>();
        }

        if (tfmSight == null)
        {
            IsWork = false;
        }
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if ( ! IsWork)
        {
            return;
        }

        if (tfmPlayer == null)
        {
            tfmPlayer = PlayerController.main.transform;
        }
    }

    void Update()
    {
        Working();
    }

    void LateUpdate()
    {
        Showing();
    }

    void FixedUpdate()
    {
        
    }

    private void Working()
    {
        if (!IsWork)
        {
            return;
        }

        float distanceTargetT = DistanceWithTarget;
        if (distanceTargetT <= distanceMin)
        {
            levelTarget = 0.0f;
        }
        else if (distanceTargetT >= distanceMax)
        {
            levelTarget = 1.0f;
        }
        else
        {
            levelTarget = (distanceTargetT - distanceMin) / (distanceMax - distanceMin);
        }
    }

    private void Showing()
    {
        if (!IsWork)
        {
            return;
        }

        float levelChangeT = levelTarget - levelNow;
        if (Mathf.Abs(levelChangeT) > Mathf.Abs(speedLevelChange * Time.deltaTime) )
        {
            int symbol = (levelTarget > levelNow) ? 1 : -1;
            levelChangeT = speedLevelChange * Time.deltaTime * symbol;
        }

        levelNow += levelChangeT;

        sizeNow = Vector2.Lerp(sizeMin, sizeMax, levelNow);
        tfmSight.sizeDelta = sizeNow;
    }
}
