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
    }
    public void itemslotcolorchange()
    {
        //アイテムスロットの枠の色変更処理
    }
    public void itemrealize()
    {
        //アイテム具現化処理
    }
    
    
    
    
}
