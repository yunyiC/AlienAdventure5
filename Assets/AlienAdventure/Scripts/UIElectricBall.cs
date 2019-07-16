using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIElectricBall : MonoBehaviour
{
    public bool isWork = true;

    public Text textStateElectricBall;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if ( ! isWork)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    void Start()
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
        if (!isWork)
        {
            return;
        }


    }

    public void Show(int numIntect, int numAll)
    {
        textStateElectricBall.text = "" + numIntect + "/" + numAll;
    }
}
