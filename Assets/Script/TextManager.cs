using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //テキストウィンドウ管理用スクリプト
    //テキスト開始処理、テキスト終了処理、テキスト進行管理など
    public Text textUI;

    public TextManager instance; 

    private string[] texts;
    private string copystring;
    private int index;
    private bool isActive;
    void Start()
    {
        instance = this;
        textUI.gameObject.SetActive(false);
        //isActive = false;
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
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
        if (Input.GetButtonDown("Fire1")&& copystring!="")
        {
            GameManager.Instance.CopyAction(copystring);
        }
    }

    public void StartText(TextScenario scenario)
    {
        texts = scenario.texts;
        copystring = scenario.scenarioString;
        index = 0;
        isActive = true;
        PlayerScript.instance.TextWindow.enabled = true;
        textUI.gameObject.SetActive(true);
        textUI.text = texts[0];
    }

    void EndText()
    {
        isActive = false;
        textUI.gameObject.SetActive(false);
        PlayerScript.instance.TextWindow.enabled = false;
    }
}
