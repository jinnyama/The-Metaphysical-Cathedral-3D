using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject  seeObjects ;// プレイヤーが見ているアイテムオブジェクト
    public string seeItemname;// プレイヤーが見ているアイテムname

    public int itemCounts=0; // プレイヤーが所持しているアイテム数
    public UnityEngine.UI.Image [] itemsrot ;// プレイヤーが所持しているアイテムスロット

    public int activeItemIndex = 0; //現在選択されているアイテムスロットのインデックス
    public int maxActiveItemIndex = 1; //最大インデックス数

    public int maxitemCount = 5; //最大所持数
    public GameObject [] itemObjects;// プレイヤーが所持しているアイテムオブジェクト

    [SerializeField] Camera     fpsCam;             // カメラ
    [SerializeField] float      distance = 0.8f;    // 検出可能な距離

    private bool isGetItem;     // アイテム取得フラグ

    public float sensitivity = 1;
    private float mouseScrollDelta;
    
    public static PlayerScript instance;

    // Start is called before the first frame update
    void Start()
    {
        itemObjects=new GameObject [maxitemCount];
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Rayはカメラの位置からとばす
        var rayStartPosition   = fpsCam.transform.position;
        // Rayはカメラが向いてる方向にとばす
        var rayDirection       = fpsCam.transform.forward.normalized;

        // Hitしたオブジェクト格納用
        RaycastHit raycastHit;

        // Rayを飛ばす（out raycastHit でHitしたオブジェクトを取得する）
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);
        
        // Debug.DrawRay (Vector3 start(rayを開始する位置), Vector3 dir(rayの方向と長さ), Color color(ラインの色));
        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        
        // なにか新しいアイテムを検出したら
        if (isHit &&raycastHit.collider.gameObject.tag == "item" && seeObjects!=raycastHit.collider.gameObject)
        {
            // LogにHitしたオブジェクト名を出力
            Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);
            //アイテムオブジェクトを更新
            seeObjects=raycastHit.collider.gameObject;
            //アウトラインエフェクトを有効化
            seeObjects.GetComponent<Outline>().enabled = true;
            //アイテム取得フラグを立てる
            isGetItem = true;
        }
        if (!isHit)
        {
            if (seeObjects == null)
            {
                //アイテム取得フラグを下ろす
                isGetItem = false;
                return;
            }
            //アウトラインエフェクトを無効化
            seeObjects.GetComponent<Outline>().enabled = false;
            //アイテムオブジェクトをリセット
            seeObjects = null;
        }


        if (Input.GetKeyDown(KeyCode.E)&& isGetItem && itemCounts< maxitemCount)
        {
            //アイテムをアイテム欄に追加
            itemObjects[itemCounts]= seeObjects;
            //最大インデックス数を更新
            maxActiveItemIndex=maxActiveItemIndex>4?4:maxActiveItemIndex+1;
            //アイテム欄の画像を更新
           switch (seeObjects.name)
            {
                case "Book":
                    itemsrot[itemCounts].sprite=GameManager.Instance.book.sprite;
                    GameManager.Instance.IsBookmodeenable = true;
                    Debug.Log("本を取得しました");
                    break;
                case "Pickaxe":
                    itemsrot[itemCounts].sprite= GameManager.Instance.pickaxe.sprite;
                    Debug.Log("つるはしを取得しました");
                    break;
                //他のアイテムもここに追加
            }

            //アイテムオブジェクトを削除
            Destroy(seeObjects);
            //itemCountsを増やす
            itemCounts += 1;
            //Debug.Log("ItemCount:"+ itemCounts);
        }
        //マウスホイールの入力を取得
        mouseScrollDelta=Input.mouseScrollDelta.y * sensitivity*.05f;
        //mauseScrollDeltaの値に応じてactiveItemIndexを増減
        activeItemIndex+= (int)mouseScrollDelta;
        if(activeItemIndex<0)
        {
            activeItemIndex=0;
        }
        if(activeItemIndex>maxActiveItemIndex)
        {
            activeItemIndex=maxActiveItemIndex;
        }
        
        for(int i=0;i<=maxActiveItemIndex;i++)
        {
            if(itemsrot[i].sprite==null) break;
            if(i==activeItemIndex)
            {
                itemsrot[i].GetComponent<Outline>().OutlineColor = Color.yellow;
            }
            else
            {
                itemsrot[i].GetComponent<Outline>().OutlineColor = Color.black;
            }
        }
        
       

    }
}
