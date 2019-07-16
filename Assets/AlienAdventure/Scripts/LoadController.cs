using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadController : MonoBehaviour
{
    public Slider sldProgress;
    public Text textProgress;
    public Text textLevelTip;

    public float progress;

    void Awake()
    {
        
    }

    void Start()
    {
        StartCoroutine(Loading());
        if (textLevelTip != null)
        {
            textLevelTip.text = "正在加载 : " + LevelManager.GetLevelNameC( GameInfo.levelLoad );
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

    private IEnumerator Loading()
    {
        //AsyncOperation op = SceneManager.LoadLevelAsync(LevelManager.GetLevelNum(GameInfo.levelLoad));

        AsyncOperation op = SceneManager.LoadSceneAsync(LevelManager.GetLevelNameE(GameInfo.levelLoad));

        if(op == null)
        {
            yield break;
        }

        while ( ! op.isDone)
        {
            progress = op.progress;

            if(sldProgress != null)
            {
                sldProgress.value = progress;
            }
            if(textProgress != null)
            {
                int intProgress = (int)(progress * 100);
                textProgress.text = "" + intProgress + "%";
            }

            yield return null;
        }
    }
}
