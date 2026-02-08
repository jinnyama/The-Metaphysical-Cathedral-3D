using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//アイテム取得やアイテム欄用のスクリプト
public class itemScript :MonoBehaviour
{
    //アイテムの処理
    //===============================
    // アイテムを拾う・アイテム欄に入れる・アイテム欄の枠の色を変える
    // Iボタンでアイテムスロット上にあるアイテムを具現化＆アイテムスロット上のイラストを削除
    //マウス右クリックでアイテムの使用にする←本やツルハシ
    
    static public itemScript instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    
    public void itemget()
    {
        //アイテム取得処理
    }
    public void itemuse()
    {
        //アイテム使用処理
        switch (PlayerScript.instance.UseitemObjects.name)
        {
            case "Book":
                //本の使用処理をここに追加
                Debug.Log("本を使用しました");
                break;
            case "Pickaxe":
                //ツルハシの使用処理をここに追加
                Debug.Log("ツルハシを使用しました");
                break;
        }
    }
    
    public void Itemrealize(GameObject itemObject)
    {
        if(itemObject==null)
        {
            Debug.Log("具現化するアイテムがありません");
            return;
        }
        //アイテム具現化処理
        switch (itemObject.name)
        {
            case "Book":
                //本の具現化処理をここに追加
                GameManager.Instance.EnterBookmode();
                Debug.Log("本を具現化しました");
                break;
            case "Pickaxe":
                //ツルハシの具現化処理をここに追加
                //GameObject pickeaxe =Instantiate(PlayerScript.instance.woodpickaxeprefab);
                //pickeaxe.transform.position = PlayerScript.instance.transform.position + PlayerScript.instance.transform.forward * 0.5f + new Vector3(0, 1.0f, 0);
                //Itemdestroy("pickaxe");
                
                
                
                Debug.Log("ツルハシを具現化しました");

                break;
            case "Key_Rusty":
                //錆びた鍵の具現化処理をここに追加
                // GameObject rustykey =Instantiate(PlayerScript.instance.woodkeyprefab);
                // rustykey.transform.position = PlayerScript.instance.transform.position + PlayerScript.instance.transform.forward * 0.5f + new Vector3(0, 1.0f, 0);
                // Itemdestroy("Key_Rusty");
                if(PlayerScript.instance.seeObjects!=null&&PlayerScript.instance.seeObjects.name=="Door_Wooden_Round_Right")
                {
                    //錆びた鍵を使って木製の扉を開ける処理
                    Destroy(PlayerScript.instance.seeObjects);
                    Debug.Log("木製の扉を錆びた鍵で開けました");
                }
                TextManager.instance.StartText(PlayerScript.instance.hintscenarios[6]);//錆びた鍵使用時のテキスト
                break;
            case "Key_Silver":
                //銀の鍵の具現化処理をここに追加
                // GameObject silverkey =Instantiate(PlayerScript.instance.silverkeyprefab);
                // silverkey.transform.position = PlayerScript.instance.transform.position + PlayerScript.instance.transform.forward * 0.5f + new Vector3(0, 1.0f, 0);
                // Itemdestroy("Key_Silver");
                if(PlayerScript.instance.seeObjects!=null&&(PlayerScript.instance.seeObjects.name=="Door_Gate_Wooden_Left"|| PlayerScript.instance.seeObjects.name=="Door_Gate_Wooden_Right"))
                {
                    //銀の鍵を使って木製の門を開ける処理
                    PlayerScript.instance.tereportUse();
                }
                TextManager.instance.StartText(PlayerScript.instance.hintscenarios[10]);//銀の鍵使用時のテキスト
                break;
            default:
                Debug.Log("具現化するアイテムがありません");
                break;
        }
    }
    public bool destroycheaker()
    {
        //壊せるオブジェクトがあるかどうかの判定
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.collider.CompareTag("destroyeable")&&PlayerScript.instance.isDestroyItem)
            {
                return true;
            }
        }
        return false;
    }
    public void Itemdestroy(string itemObject)
    {
        int i;
        //前のものを壊す処理
        if(PlayerScript.instance.seeObjects!=null&&destroycheaker())
                {
                    Destroy(PlayerScript.instance.seeObjects);
                }
                for(i=0;i<PlayerScript.instance.itemsrot.Length;i++)
                {
                    if(PlayerScript.instance.itemsrot[i].name==itemObject)
                    {
                        PlayerScript.instance.itemsrot[i].sprite=null;
                        break;
                    }
                }for(;i<PlayerScript.instance.itemsrot.Length-1;i++)
                {
                    PlayerScript.instance.itemsrot[i].sprite= PlayerScript.instance.itemsrot[i+1].sprite;
                    PlayerScript.instance.itemsrot[i+1].sprite=null;
                    PlayerScript.instance.itemObjects[i]= PlayerScript.instance.itemObjects[i+1];
                    PlayerScript.instance.itemObjects[i+1]=null;
                    PlayerScript.instance.itemsrot[i+1].color=new Color(1,1,1,0);
                    PlayerScript.instance.itemsrotChildrenText[i+1].color=new Color(0,0,0,0);
                }
    }
    
    
    
    
}
