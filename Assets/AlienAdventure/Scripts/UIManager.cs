using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    public bool isWork = true;

    public GameObject UICrystal;
    public GameObject UICrystalChange;
    public GameObject UIHealth;
    public GameObject UITipKey;
    public GameObject UITipWarning;
    public float timeShowWarningMax = 1.0f;
    public float timeShowWarningNow = 0.0f;
    public GameObject UILevel;
    public GameObject UIMenu;
    public GameObject UIBeHurt;
    public float timeShowHurtMax = 1.0f;
    public float timeShowHurtNow = 0.0f;

    public Text textValueCrystal;
    public Text textValueChangeCrystal;
    public float timeShowCrystalChangeMax = 1.0f;
    public float timeShowCrystalChangeNow = 0.0f;
    public Slider sldShield;
    public Slider sldBlood;
    public Text textTipKey;
    public Text textWarning;
    public Text textLevelName;
    public Image imgBeHurt;

    public int valueCrystalLast = 0;
    public int valueCrystalChange = 0;
    public Color colorBeHurt;


    public static bool IsPause
    {
        get
        {
            return GameInfo.isPause;
        }
        set
        {
            GameInfo.isPause = value;
            main.UIMenu.SetActive(GameInfo.isPause);
            CursorManager.SetCursor(GameInfo.isPause);
            if(GameInfo.isPause)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }


    public static string TipKey
    {
        set
        {
            if (main != null)
            {
                if (main.isWork)
                {
                    if(value != null)
                    {
                        main.UITipKey.SetActive(true);
                        main.textTipKey.text = value;
                    }
                    else
                    {
                        main.UITipKey.SetActive(false);
                    }
                }
            }
        }
    }

    public static string TipWarning
    {
        set
        {
            if ((main != null) && (main.isWork))
            {
                main.UITipWarning.SetActive(true);
                main.textWarning.text = value;
                main.timeShowWarningNow = main.timeShowWarningMax;
            }
        }
    }

    void Awake()
    {
        UIManager.main = this;
        Init();
    }

    private void Init()
    {
        if (textValueCrystal == null)
        {
            if(UICrystal != null)
            {
                Transform tfmValueCrystalT = UICrystal.transform.Find("txtCrystalValue");
                if(tfmValueCrystalT != null)
                {
                    textValueCrystal = tfmValueCrystalT.GetComponent<Text>();
                }
                Transform tfmValueChangeCrystalT = UICrystal.transform.Find("txtCrystalValueChange");
                if (tfmValueChangeCrystalT != null)
                {
                    textValueChangeCrystal = tfmValueChangeCrystalT.GetComponent<Text>();
                }
            }
        }

        if(sldShield == null)
        {
            if(UIHealth != null)
            {
                Transform tfmShieldT = UIHealth.transform.Find("Shield");
                if(tfmShieldT != null)
                {
                    sldShield = tfmShieldT.GetComponent<Slider>();
                }
            }
        }
        if (sldBlood == null)
        {
            if (UIHealth != null)
            {
                Transform tfmBloodT = UIHealth.transform.Find("Blood");
                if (tfmBloodT != null)
                {
                    sldShield = tfmBloodT.GetComponent<Slider>();
                }
            }
        }

        if (textTipKey == null)
        {
            if (UITipKey != null)
            {
                Transform tfmTextTipKeyT = UIHealth.transform.Find("TextTipKey");
                if (tfmTextTipKeyT != null)
                {
                    textTipKey = tfmTextTipKeyT.GetComponent<Text>();
                }
            }
        }

        if (textWarning == null)
        {
            if (UITipWarning != null)
            {
                Transform tfmTextWArningT = UIHealth.transform.Find("TextWarning");
                if (tfmTextWArningT != null)
                {
                    textWarning = tfmTextWArningT.GetComponent<Text>();
                }
            }
        }
        if(textLevelName == null)
        {
            if(UILevel != null)
            {
                Transform tfmLevelNameT = UILevel.transform.Find("TextLevelName");
                if (tfmLevelNameT != null)
                {
                    textLevelName = tfmLevelNameT.GetComponent<Text>();
                }
            }
        }

        if(imgBeHurt == null)
        {
            if(UIBeHurt != null)
            {
                Transform tfmImageBeHurt = UIBeHurt.transform.Find("ImageBeHurt");
                if (tfmImageBeHurt != null)
                {
                    imgBeHurt = tfmImageBeHurt.GetComponent<Image>();
                }
            }
        }

        if(imgBeHurt != null)
        {
            colorBeHurt = imgBeHurt.color;
            imgBeHurt.color = new Color(
                colorBeHurt.r,
                colorBeHurt.g,
                colorBeHurt.b,
                0.0f);
        }

        if(UITipKey != null)
        {
            UITipKey.SetActive(false);
        }
        if (UITipWarning != null)
        {
            UITipWarning.SetActive(false);
        }
        if(UICrystalChange != null)
        {
            UICrystalChange.SetActive(false);
        }

        valueCrystalLast = GameInfo.playerCrystal;

        IsPause = false;

        TipKey = null;
        isWork = true;
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

        if ( CheckPause() )
        {
            return;
        }

        RefreshHealth();
        RefreshWarning();
        RefreshLevelName();
        RefreshCrystal();
        RefreshBeHurt();
    }

    private bool CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            IsPause = !IsPause;
        }

        return IsPause;
    }

    private void RefreshCrystal()
    {
        textValueCrystal.text = "" + GameInfo.playerCrystal;

        if (timeShowCrystalChangeNow > 0.0f)
        {
            timeShowCrystalChangeNow -= Time.deltaTime;
            if (timeShowCrystalChangeNow <= 0.0f)
            {
                valueCrystalChange = 0;
                UICrystalChange.SetActive(false);
            }
        }

        if (valueCrystalLast != GameInfo.playerCrystal)
        {
            if (timeShowCrystalChangeNow > 0.0f)
            {
                valueCrystalChange += GameInfo.playerCrystal - valueCrystalLast;
            }
            else
            {
                UICrystalChange.SetActive(true);
                valueCrystalChange = GameInfo.playerCrystal - valueCrystalLast;
            }
            if (valueCrystalChange > 0)
            {
                textValueChangeCrystal.text = "+" + valueCrystalChange;
            }
            else
            {
                textValueChangeCrystal.text = "" + valueCrystalChange;
            }
            timeShowCrystalChangeNow = timeShowCrystalChangeMax;
            valueCrystalLast = GameInfo.playerCrystal;
        }
    }

    private void RefreshHealth()
    {
        sldShield.value = GameInfo.PlayerProportionShield;
        sldBlood.value = GameInfo.PlayerProportionBlood;
    }

    private void RefreshLevelName()
    {
        textLevelName.text = LevelManager.GetLevelNameC(GameInfo.levelNow);
    }

    private void RefreshWarning()
    {
        if (timeShowWarningNow > 0.0f)
        {
            timeShowWarningNow -= Time.deltaTime;
            if (timeShowWarningNow <= 0.0f)
            {
                UITipWarning.SetActive(false);
            }
        }
    }

    private void RefreshBeHurt()
    {
        if(timeShowHurtNow > 0.0f)
        {
            timeShowHurtNow -= Time.deltaTime;
            imgBeHurt.color = colorBeHurt * (timeShowHurtNow / timeShowHurtMax) * 0.5f;
        }
    }

    public static void ShowHurt()
    {
        main.timeShowHurtNow = main.timeShowHurtMax;
    }
}
