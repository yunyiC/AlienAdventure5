using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIEcho : MonoBehaviour
{
    public static UIEcho main;

    public bool isWork = true;
    public bool isShowDialogue = false;

    public GameObject objDialogue;
    public Text textDialogue;

    public float timeEachWord = 0.5f;
    public float timeShowMax = 0.0f;
    public float timeShowNow = 0.0f;
    public float timeShowWordMax = 0.2f;
    public float timeShowWordNow = 0.0f;
    public int numWordsMax = 0;
    public int numWordsNow = 0;

    public Queue<string> dialogues = new Queue<string>();
    public string strShowNow = "";


    void Awake()
    {
        UIEcho.main = this;
        Init();
    }

    private void Init()
    {
        if ( ! isWork)
        {
            gameObject.SetActive(false);
        }

        SetDialogueShow(isShowDialogue);
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {

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

        Showing();
    }

    private void Showing()
    {
        if (timeShowNow < timeShowMax)
        {
            timeShowNow += Time.deltaTime;
            if (timeShowWordNow < timeShowWordMax)
            {
                timeShowWordNow += Time.deltaTime;
            }
            else
            {
                if (numWordsNow < numWordsMax)
                {
                    numWordsNow++;
                    textDialogue.text = strShowNow.Substring(0, numWordsNow);
                    timeShowWordNow -= timeShowWordMax;
                }
            }
        }
        else
        {
            if (dialogues.Count < 1)
            {
                //没有需要显示的信息了
                SetDialogueShow(false);
            }
            else
            {
                //有需要显示的信息
                SetDialogueShow(true);
                ShowNextDialogue();
            }
        }
    }

    private void ShowNextDialogue()
    {
        strShowNow = dialogues.Dequeue();
        timeShowMax = strShowNow.Length * timeEachWord;
        timeShowNow = 0.0f;

        timeShowWordNow = 0.0f;
        numWordsMax = strShowNow.Length;
        numWordsNow = 1;

        //textDialogue.text = strShowNow;
    }

    private void SetDialogueShow(bool newState)
    {
        isShowDialogue = newState;
        objDialogue.SetActive(isShowDialogue);
        if ( ! isShowDialogue)
        {
            //隐藏之后清空显示
            strShowNow = "";
            textDialogue.text = strShowNow;
        }
    }

    public static void AddDialogues(string[] strDialogues)
    {
        foreach (string strT in strDialogues)
        {
            AddDialogue(strT);
        }
    }

    public static void AddDialogue(string strDialogue)
    {
        //Debug.Log(strDialogue + " 添加成功 ");
        main.dialogues.Enqueue(strDialogue);
    }
}
