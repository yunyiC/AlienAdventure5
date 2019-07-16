using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReturnBase : MonoBehaviour
{
    public bool isWork = true;
    public KeyCode keyReturnBase = KeyCode.B;
    public LevelManager.Levels levelTo = LevelManager.Levels.LevelBase;

    void Awake()
    {
        
    }

    void Start()
    {
        //UIManager.TipWarning = "按B返回基地";
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
        if(!isWork)
        {
            return;
        }

        if(Input.GetKeyDown(keyReturnBase))
        {
            LevelManager.LoadLevel(levelTo);
        }
    }
}
