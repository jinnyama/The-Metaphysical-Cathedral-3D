using UnityEngine;
using UnityEngine.UI;
public class BookScript : MonoBehaviour
{
    public string []bookstring;
    public static BookScript instance;
    public int maxtextindex=0;
    public int activetextIndex=0;
    public int freetextindex=0;
    private int halfmaxindex=6;

    public Text copytext;
    public Text BookText;

    public string copystring;
    private string Bookstring;
    public string choicetext;//矢印でBookTextの文字色変更時に変更する用
    public float textmouseScrollDelta;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        bookstring=new string[12];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Gamemode != "bookmode")
        {
            HideBookTexts();
        }
        if(GameManager.Instance.Gamemode=="bookmode")
        {        
            //マウスホイールの入力を取得
            //textmouseScrollDelta=PlayerScript.instance.mouseScrollDelta;
            ShowBookTexts();
            //ホイール検知
            // //activetextIndexの変更
            // if(Input.GetKeyDown(KeyCode.RightArrow))
            // {
            //     activetextIndex++;
            // }
            // if(Input.GetKeyDown(KeyCode.LeftArrow))
            // {
            //     activetextIndex--;
            // }
            // //activetextIndexの範囲制限
            // if(activetextIndex>maxtextindex)
            // {
            //     activetextIndex=maxtextindex;
            // }
            // else if(activetextIndex<0)
            // {
            //     activetextIndex=0;
            // }
            
            for(int i=0;i<maxtextindex;i++)
            {
                // if(i==activetextIndex)
                // {
                //     BookTextSrot[i].GetComponent<Outline>().effectColor= Color.yellow;
                // }
                // else
                // {
                //     BookTextSrot[i].GetComponent<Outline>().effectColor = Color.black;
                // }
            }


        }

    }

    // BookTextSrotの透明化処理
    void HideBookTexts()
    {
        BookText.text="";
    }
    void ShowBookTexts()
    {
        BookText.text="";
        int i;
        for (i = 0; i < bookstring.Length/2&&i<maxtextindex; i++)//前半部分表示
        {
            BookText.text+=bookstring[i];
            //BookTextSrot[i].GetComponent<Outline>().effectColor = Color.black;
        }
        for (; i < maxtextindex; i++)//後半部分表示
        {
            BookText.text+=bookstring[i];
            //BookTextSrot[i].GetComponent<Outline>().effectColor = Color.black;
        }
        
    }
    public void CopyAction()
    {
        //コピーテキストにコピーしたテキストを代入
        copytext.text = "copy:" + TextManager.instance.SerchCopystring(PlayerScript.instance.ChoiseText());
        Debug.Log(TextManager.instance.SerchCopystring(PlayerScript.instance.ChoiseText()));
        Debug.Log(PlayerScript.instance.ChoiseText());
        //bookstringにコピーしたテキストを代入
        bookstring[freetextindex++]="<color=black>"+copytext.text[5..]+"</color>\n";
        maxtextindex++;

    }
    public void PasteAction()
    {
        string pastetext=TextManager.instance.textUI.text;
        int startindex = pastetext.IndexOf(TextManager.instance.startstring);
        int endIndex = pastetext.IndexOf(TextManager.instance.endstring);
        if (startindex >= 0&& endIndex > startindex)
        {
            copystring = pastetext.Substring(startindex+TextManager.instance.startstring.Length, endIndex- (startindex + TextManager.instance.startstring.Length));
                
        }
        TextManager.instance.textUI.text=TextManager.instance.textUI.text.Replace(copystring,copytext.text);
    }
}
