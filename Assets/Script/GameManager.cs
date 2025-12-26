using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public   Sprite[] image;
    private SpriteRenderer book;
    private SpriteRenderer sign;//看板
    private SpriteRenderer black;
    private SpriteRenderer background;
    public GameObject Background;
    public string Gamemode="";
    public int currentquiznumber=0;
    public bool IsBookmodecheak = false;
    public bool IsSignmodecheak = false;
    public static GameManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        background=Background.GetComponent<SpriteRenderer>()  ;

        background.sprite=image[0];
        book = GameObject.Find("Book").GetComponent<SpriteRenderer>();
        sign = GameObject.Find("Sign").GetComponent<SpriteRenderer>();
        black = GameObject.Find("BlackBoard").GetComponent<SpriteRenderer>();

        // 初期状態は透明
        Color c=book.color;
        c.a = 0f;
        book.color = c;
        sign.color = c;
        black.color = c;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B) && Gamemode == "")
        {
            StartCoroutine(FadeIn(book,1.0f, Color.white));
            Gamemode = "bookmode";
            //IsBookmodecheak = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Gamemode=="bookmode" )
        {
            StartCoroutine(FadeOut(book,1.0f,Color.white));
            Gamemode = "";
            //IsBookmodecheak = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Gamemode == "")
        {
            StartCoroutine(FadeIn(sign,0.5f, Color.gray));
            Gamemode= "signmode";
            //IsSignmodecheak = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Gamemode=="signmode")
        {
            StartCoroutine(FadeOut(sign,0.5f, Color.gray));
            Gamemode = "";
            if(IsSignmodecheak){
                //現在のクイズ番号をインクリメント
                currentquiznumber++;
                IsSignmodecheak = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && TextScript.Instance.choisetext != "")//一般化完了
        {
            //コピーテキストに「copy:」＋選択テキストを代入
            TextScript.Instance.copytext.text= "copy:" + TextScript.Instance.choisetext;
            //選択テキストの色を赤に変更
            TextScript.Instance.diarytext[0].color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.V) && TextScript.Instance.choisetext != "" && Gamemode == "signmode")//雲って用
        {
            //テキスト配列に直接代入？  →ペーストテキストがテキスト配列の１番目をうけとってる(テキストスクリプト参照)
            TextScript.Instance.Text[1][currentquiznumber]=TextScript.Instance.choisetext;
            if (TextScript.Instance.choisetext == "晴れて")
            {
                //ペーストテキストの色を赤色に変更
                TextScript.Instance.pastetext.color = Color.red;
                //壁紙の変更
                StartCoroutine(BlackboardFadeIn(black,0.5f, Color.black));
                
                IsSignmodecheak = true;
            
            }
        }



    }
    private System.Collections.IEnumerator FadeIn(SpriteRenderer spr,float duration,Color c )
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            c.a = t;
            spr.color = c;
            yield return null;

        }
    }
    private System.Collections.IEnumerator FadeOut(SpriteRenderer spr,float duration,Color c )
    {
        float elapsed = 0.5f;
        while (elapsed >0)
        {
            elapsed -= Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            c.a = t;
            
            spr.color = c;
            yield return null;
        }
    }
   private System.Collections.IEnumerator BlackboardFadeIn(SpriteRenderer spr,float duration,Color c )
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            c.a = t;
            spr.color = c;
            yield return null;
        }
        background.sprite = image[GameManager.Instance.currentquiznumber + 1];
        StartCoroutine(FadeOut(spr,0.5f, Color.black));
         
    }

}
