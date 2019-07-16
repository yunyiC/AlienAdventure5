using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 自定义按钮控制
/// </summary>
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isWork = true;      //是否工作

    public Button button;           //组件:按钮
    public Text text;               //组件:文字

    public bool isChangeSize = true;//是否在鼠标悬浮时放大字体

    public int fontSizeNormal = 50; //字体正常大小
    public int fontSizeBig = 60;    //字体变大后大小

    public GameEventType eventType = GameEventType.LoadLevel;//按钮触发的事件类型
    public LevelManager.Levels levelTo = LevelManager.Levels.LevelBase;//下一关,类型为加载关卡时有效

    public GameObject panelA;//第一个面板,类型为切换面板时有效
    public GameObject panelB;//第二个面板,类型为切换面板时有效

    /// <summary>
    /// 按钮事件类型
    /// </summary>
    public enum GameEventType
    {
        LoadLevel,//加载关卡
        ContinueGame,//继续游戏
        ExitGame,//退出游戏
        ChangePanel,//切换面板
    }

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
        }
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

    public void OnClick()
    {
        //Debug.Log("检测到按钮按下!");
        text.fontSize = fontSizeNormal;

        switch (eventType)
        {
            case GameEventType.LoadLevel:
                LevelManager.LoadLevel(levelTo);
                break;
            case GameEventType.ContinueGame:
                UIManager.IsPause = false;
                break;
            case GameEventType.ExitGame:
                Application.Quit();
                break;
            case GameEventType.ChangePanel:
                panelA.SetActive(false);
                panelB.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData dataEvent)
    {
        if(text != null)
        {

            //text.fontStyle = FontStyle.Bold;
            text.fontSize = fontSizeBig;
        }
    }

    public void OnPointerExit(PointerEventData dataEvent)
    {
        if (text != null)
        {
            //text.fontStyle = FontStyle.Normal;
            text.fontSize = fontSizeNormal;
        }
    }
}
