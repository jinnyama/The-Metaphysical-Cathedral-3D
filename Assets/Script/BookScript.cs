using UnityEngine;
using UnityEngine.UI;
  

public class BookScript : MonoBehaviour
{
    public string []bookstring;
    public static BookScript instance;
    public int maxtextindex=0;
    public int activetextIndex=0;
    public Text []booktext;//max 5

    public UnityEngine.UI.Image [] BookTextSrot;//max 5
    //public 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        booktext=new Text[BookTextSrot.Length];
        for(int i=0;i<BookTextSrot.Length;i++)
        {
            booktext[i]= BookTextSrot[i].GetComponentInChildren<Text>();
            maxtextindex++;
        }
        for(int i=0;i<booktext.Length;i++)
        {
            booktext[i].text="";
        }
        


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
            ShowBookTexts();
            //booktextの更新
            for (int i = 0; i < bookstring.Length; i++)
            {
                BookTextSrot[i].enabled=true;
                //booktext[i].text = bookstring[i];
            }
            //activetextIndexの変更
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                activetextIndex++;
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                activetextIndex--;
            }
            //activetextIndexの範囲制限
            if(activetextIndex>=maxtextindex)
            {
                activetextIndex=maxtextindex-1;
            }
            else if(activetextIndex<0)
            {
                activetextIndex=0;
            }
            
            for(int i=0;i<maxtextindex;i++)
            {
            
                if(i==activetextIndex)
                {
                    BookTextSrot[i].GetComponent<Outline>().effectColor= Color.yellow;
                }
                else
                {
                    BookTextSrot[i].GetComponent<Outline>().effectColor = Color.black;
                }
            }

        }

    }

    // BookTextSrotの透明化処理
    void HideBookTexts()
    {
        for (int i = 0; i < BookTextSrot.Length; i++)
        {
            BookTextSrot[i].color= new Color(1, 1, 1, 0);
            //BookTextSrot[i].GetComponent<Outline>().effectColor = new Color(0, 0, 0, 0);
        }
    }
    void ShowBookTexts()
    {
        for (int i = 0; i < BookTextSrot.Length; i++)
        {
            BookTextSrot[i].color = new Color(1, 1, 1, 1);
            //BookTextSrot[i].GetComponent<Outline>().effectColor = Color.black;
        }
    }
}
