using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public   Sprite[] image;//背景用←Skyboxに変更予定
    public Image book;
    public Image woodpickaxe;
    public Image metalpickaxe;
    public Image woodkey;
    public Image metalkey;

    private SpriteRenderer sign;//看板
    private SpriteRenderer black;
    
    private SpriteRenderer background;
    //public GameObject Background;
    public string Gamemode="";
    public int currentquiznumber=0;
    public bool IsBookmodeenable = false;
    public bool IsPickaxemodeenable = false;
    public bool IsSignmodecheak = false;
    public static GameManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        //background=Background.GetComponent<SpriteRenderer>()  ;

        //画像準備
        book = GameObject.Find("book").GetComponent<Image>();
        woodpickaxe = GameObject.Find("pickaxe").GetComponent<Image>();
        metalpickaxe = GameObject.Find("metalpickaxe").GetComponent<Image>();
        woodkey = GameObject.Find("woodkey").GetComponent<Image>();
        metalkey = GameObject.Find("ironkey").GetComponent<Image>();
        // 初期状態は透明
        Color c=book.color;
        c.a = 0f;
        book.color = c;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Escape) && Gamemode=="bookmode" )
        {
            StartCoroutine(ImageFadeOut(book,1.0f,Color.white));
            Gamemode = "";
            //IsBookmodecheak = false;
        }
        if (Input.GetKeyDown(KeyCode.L) && Gamemode == "")
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
        



    }
    public void EnterBookmode()
    {
        if (Gamemode==""&&IsBookmodeenable)
        {
            StartCoroutine(ImageFadeIn(book,1.0f, Color.white));
            Gamemode = "bookmode";
            //IsBookmodecheak = true;
        }
    }
    public void ExitBookmode()
    {
        Gamemode = "";
        StartCoroutine(ImageFadeOut(book,1.0f,Color.white));
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
     private System.Collections.IEnumerator ImageFadeIn(Image spr,float duration,Color c )
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
    private System.Collections.IEnumerator ImageFadeOut(Image spr,float duration,Color c )
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
        background.sprite = image[currentquiznumber + 1];
        StartCoroutine(FadeOut(spr,0.5f, Color.black));
         
    }
    public void ResetGame()
    {
        // ゲームの状態を初期化する処理をここに追加
        Gamemode = "";
        currentquiznumber = 0;
        IsBookmodeenable = false;
        IsSignmodecheak = false;
        // その他の初期化処理もここに追加
    }
   
}
