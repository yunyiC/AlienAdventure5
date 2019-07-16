using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    public LevelManager.Levels levelNow = LevelManager.Levels.LevelBase;

    public string[] strWelcomes;

    void Awake()
    {
        
    }

    void Start()
    {
        GameInfo.levelNow = levelNow;
        UIEcho.AddDialogues(strWelcomes);
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
}
