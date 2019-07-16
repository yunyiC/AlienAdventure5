using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager main;

    public bool isWork = true;

    public GameObject objPortal;
    public string[] strDialogues;

    public UIElectricBall uiElectricBall;
    public GeneratorController[] generators;

    public int numIntect = 0;
    public int numAll = 0;

    void Awake()
    {
        GeneratorManager.main = this;
        Init();
    }

    private void Init()
    {
        numAll = generators.Length;
    }

    void Start()
    {
        Init2();
    }

    private void Init2()
    {
        numIntect = GetNumIntect();
        if (uiElectricBall != null)
        {
            uiElectricBall.Show(numIntect, numAll);
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

    public static void Check()
    {
        main.numIntect = main.GetNumIntect();
        main.uiElectricBall.Show(main.numIntect, main.numAll);
        if (main.numIntect == main.numAll)
        {
            UIEcho.AddDialogues(main.strDialogues);
            if (main.objPortal != null)
            {
                main.objPortal.SetActive(true);
            }
        }
    }

    private void Working()
    {
        if ( ! isWork)
        {
            return;
        }


    }

    private int GetNumIntect()
    {
        int numIntectT = 0;

        if (generators == null || generators.Length < 1)
        {
            return numIntectT;
        }

        foreach (GeneratorController generatorT in generators)
        {
            if (generatorT == null)
            {
                continue;
            }

            if (generatorT.isElectric)
            {
                numIntectT++;
            }
        }

        return numIntectT;
    }
}
