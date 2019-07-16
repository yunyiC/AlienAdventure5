using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Minimap : MonoBehaviour
{
    public bool isWork = true;

    public bool isRotateMap = true;

    public Image imgMap;
    public RectTransform tfmMapR;
    public RectTransform tfmMap;
    public Vector2 posMapStart;
    public Vector2 posMap;
    public Vector2 sizeMap;

    public Transform tfmPlayer;
    public Vector3 posPlayerStart;
    public Vector3 posPlayer;
    public float speedMap = 4.5f;

    public Transform tfmCamera;

    public Image imgPlayer;
    public RectTransform tfmImgPlayer;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if( ! isWork)
        {
            gameObject.SetActive(false);
        }

        posMap = new Vector2(100.0f, 0.0f);
        //sizeMap = tfmMap.rect.size;
        //rectMapStart = new Rect(posMap,sizeMap);
        if(imgMap != null)
        {
            if (tfmMap == null)
            {
                tfmMap = imgMap.GetComponent<RectTransform>();
            }
        }
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        if (tfmMap != null)
        {
            //tfmMap.localPosition = posMap;
            posMapStart = tfmMap.localPosition;
        }

        tfmCamera = Camera.main.transform;

        tfmPlayer = PlayerController.main.transform;
        posPlayerStart = tfmPlayer.position;
    }

    void Update()
    {
        Moving();
    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    private void Moving()
    {
        if( ! isWork)
        {
            return;
        }

        //Debug.Log("Moving");

        if (tfmMap != null)
        {
            posPlayer = tfmPlayer.position;
            Vector3 offset = posPlayer - posPlayerStart;
            offset = offset * speedMap;

            posMap.x = posMapStart.x - offset.x;
            posMap.y = posMapStart.y - offset.z;
            //tfmMap.rect.position = posMap;
            tfmMap.localPosition = posMap;
        }

        if (isRotateMap)
        {


            if (tfmMapR != null)
            {
                Vector3 eulerangleT = new Vector3
                {
                    x = 0.0f,
                    y = 0.0f,
                    z = tfmCamera.rotation.eulerAngles.y
                };
                tfmMapR.localRotation = Quaternion.Euler(eulerangleT);
            }
        }
        else
        {
            if (tfmImgPlayer != null)
            {
                Vector3 eulerangleT = new Vector3
                {
                    x = 0.0f,
                    y = 0.0f,
                    z = tfmCamera.rotation.eulerAngles.y * (-1)
                };
                tfmImgPlayer.localRotation = Quaternion.Euler(eulerangleT);
            }
        }
    }
}
