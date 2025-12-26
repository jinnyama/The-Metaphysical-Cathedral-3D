using UnityEngine;
using UnityEngine.UI;

//クイズなどのテキスト管理用
public class TextScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Text []diarytext;//UNity側でTextオブジェクトをいれる
    public Text copytext;
    public Text pastetext;
    public Text signtext;
    public  string[][] Text={
        new string[] {"晴れて","文字が書かれて","虹","囲われている","聖典","囲われて"},
        new string[] {"曇って","壊れて","橋","金","ゲートの鍵","壊れて"}
    };
    private string[] signText={"空が　　　いる","　　　　　　　いる祠",　"がかかっている","檻に　　　　いる"};
    public static TextScript Instance;
    
    public string choisetext;


    void Start()
    {

        Instance = this;
        //diarytext=new Text[5];
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;

        if (GameManager.Instance.Gamemode == "bookmode")
        {
            //ダイアリーテキストがテキスト配列の０ばんめをうけとってる→一般化⚠
            for (int i = 0; i < diarytext.Length; i++)
            {
                diarytext[i].text = Text[0][i];
            }
        }
        if (GameManager.Instance.Gamemode == "signmode")
        {
            //ペーストてきすとがテキスト配列の１ばんめをうけとってる→一般化⚠
            pastetext.text = Text[1][GameManager.Instance.currentquiznumber];
            //テキストウィンドウにsignText配列の０ばんめをうけとってる→一般化⚠
            signtext.text= signText[GameManager.Instance.currentquiznumber];
        }
        if (GameManager.Instance.Gamemode == "")
        {
            for (int i = 0; i < diarytext.Length; i++)
            {
                diarytext[i].text = "";
            }
            
            signtext.text = "";
            pastetext.text = "";
        }
    }
    



}
