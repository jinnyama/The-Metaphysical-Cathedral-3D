using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //テキストウィンドウ管理用スクリプト
    //テキスト開始処理、テキスト終了処理、テキスト進行管理など
    public Text textUI;

    public static TextManager instance; 

    private string[] texts;
    private string copystring;
    
    public string startstring="<color=yellow>";//黄色指定
    public string endstring="</color>";//色終わり
    private int changeableindex;
    private int index;
    private bool isActive;
    public bool IsCopychange= false;
    void Start()
    {
        instance = this;
        //textUI.gameObject.SetActive(false);
        //isActive = false;
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            index++;

            if (index < texts.Length)
            {
                textUI.text = texts[index];
            }
            
            else
            {
                EndText();
            }
        }
        if (Input.GetButtonDown("Fire1")&& index==changeableindex)
        {
            LetterchangeAction();
            IsCopychange = true;
        }
    }

    public void StartText(TextScenario scenario)
    {
        if (scenario == null) return;
        //Debug.Log(scenario.name);
        texts = scenario.texts;
        //copystring = scenario.scenarioString;
        changeableindex = scenario.changeableindex;//変更可能なインデックスを取得
        index = 0;
        isActive = true;
        PlayerScript.instance.TextWindow.enabled = true;
        PlayerScript.instance.ismoveplayer = false;
        textUI.gameObject.SetActive(true);
        textUI.text = texts[0];
    }

    void EndText()
    {
        isActive = false;
        textUI.gameObject.SetActive(false);
        PlayerScript.instance.scenario = null;
        PlayerScript.instance.TextWindow.enabled = false;
        PlayerScript.instance.ismoveplayer = true;
    }
    public string SerchCopystring(int mode)
    {
        string text;
        if (mode == 0)
        {
            //copystring = texts[changeableindex];
            text = textUI.text;
            int startindex = text.IndexOf(startstring);
            int endIndex = text.IndexOf(endstring);
            if (startindex >= 0&& endIndex > startindex)
            {
                copystring = text.Substring(startindex+startstring.Length, endIndex- (startindex + startstring.Length));
                return copystring;
            }
            
        }
        else if (mode==1)
        {
            text =BookScript.instance.bookstring[PlayerScript.instance.activetextIndex];
            
            //text=BookScript.instance.choicetext;
            return text;
        }
        return "";
        
   
    }
     public void LetterchangeAction()
    {
        int startindex = textUI.text.IndexOf("<color=red>");
        //int endIndex = text.IndexOf(endstring);
        if(startindex>=0){
            // textUI.text = textUI.text.Remove(startindex+7,6);
            // textUI.text = textUI.text.Insert(startindex+7,"yellow");
            textUI.text = textUI.text.Replace("<color=red>","<color=yellow>");
        }
    } 

}
