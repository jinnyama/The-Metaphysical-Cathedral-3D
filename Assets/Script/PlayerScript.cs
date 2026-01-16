using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject  seeObjects ;// プレイヤーが見ているアイテムオブジェクト
    public string seeItemname;// プレイヤーが見ているアイテムname

    public int itemCounts=0; // プレイヤーが所持しているアイテム数
    public UnityEngine.UI.Image [] itemsrot ;// プレイヤーが所持しているアイテムスロット

    public Text [] itemsrotChildrenText ;// アイテムスロットの子Textコンポーネント
    private int activeItemIndex = 0; //現在選択されているアイテムスロットのインデックス
    private int maxActiveItemIndex = 0; //最大インデックス数

    public int maxitemCount = 5; //最大所持数
    public GameObject [] itemObjects;// プレイヤーが所持しているアイテムオブジェクト

    [SerializeField] Camera     fpsCam;             // カメラ
    [SerializeField] float      distance = 0.8f;    // 検出可能な距離

    public UnityEngine.UI.Image TextWindow; //テキストウィンドウ表示用

    private bool isGetItem;     // アイテム取得フラグ
    private bool isTereport;    // テレポート取得フラグ

    //private bool IstextWindowActive=false; //テキストウィンドウ表示フラグ

    private Vector3 PlayerPosition;
    private Vector3 initialPosition;

    public float sensitivity = 1;
    private float mouseScrollDelta;
    
    public TextScenario scenario;
    public TextScenario[] hintscenarios;
    public SignText textManager;
    public static PlayerScript instance;

    // Start is called before the first frame update
    void Start()
    {
        TextWindow.enabled=false;
        itemObjects=new GameObject [maxitemCount];
        itemsrotChildrenText=new Text [itemsrot.Length];
        instance = this;
        Color c;
        c.a = .05f;
        Debug.Log("ActiveItemIndex:"+ activeItemIndex);
        Debug.Log("MaxActiveItemIndex:"+ maxActiveItemIndex);
        for(int i=0;i<itemsrot.Length;i++)
        {
            // Imageを取得してから、その色を変える（using UnityEngine.UI; が必要）
            itemsrot[i].color =  new Color(1,1,1,0);
            itemsrotChildrenText[i]= itemsrot[i].GetComponentInChildren<Text>();
            itemsrotChildrenText[i].color=new Color(0,0,0,0);
            itemsrot[i].GetComponent<Outline>().OutlineColor = Color.black;
        }
        PlayerPosition=this.transform.position;
        initialPosition=this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position=PlayerPosition;
            Debug.Log(PlayerPosition);
            Debug.Log("初期位置にリセットしました");
            PlayerPosition=initialPosition;
        }
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
        if (isHit && raycastHit.collider.gameObject.tag!="Untagget"&&seeObjects!=raycastHit.collider.gameObject)
        {
            // LogにHitしたオブジェクト名を出力
            Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);
            //アイテムオブジェクトを更新
            seeObjects=raycastHit.collider.gameObject;
            //アウトラインエフェクトを有効化
            
            switch (seeObjects.tag)
            {
                case "item":
                    seeObjects.GetComponent<Outline>().enabled = true;
                    isGetItem = true;
                    break;
                case "tereport":
                    seeObjects.GetComponent<Outline>().enabled = true;
                    isTereport = true;
                    break;
                case "Hint":
                    seeObjects.GetComponent<Outline>().enabled = true;
                    isGetItem = false;
                    isTereport = false;
                    //TextWindow.enabled = true; 
                    break;
                default:
                    break;
            }
        }
        if (!isHit)
        {
            if (seeObjects == null)
            {
                //アイテム取得フラグを下ろす
                isGetItem = false;
                //テレポート取得フラグを下ろす
                isTereport = false;
                
                //アウトラインエフェクトを無効化
                //seeObjects.GetComponent<Outline>().enabled = false;
                return;
            }
            //アイテムオブジェクトをリセット
            seeObjects = null;
        }


        if (Input.GetKeyDown(KeyCode.E)&& isGetItem && itemCounts< maxitemCount)
        {

           
            Debug.Log("ActiveItemIndex:"+ activeItemIndex);
            Debug.Log("MaxActiveItemIndex:"+ maxActiveItemIndex);
            //アイテム欄の画像を更新
           switch (seeObjects.name)
            {
                case "Book":
                    //アイテムをアイテム欄に追加
                    itemObjects[itemCounts]= seeObjects;
                    //最大インデックス数を更新
                    maxActiveItemIndex=maxActiveItemIndex>4?4:maxActiveItemIndex+1;
                    itemsrot[itemCounts].sprite=GameManager.Instance.book.sprite;
                    itemsrot[itemCounts].color=new Color(1,1,1,1);
                    itemsrotChildrenText[itemCounts].color=new Color(0,0,0,1);
                    GameManager.Instance.IsBookmodeenable = true;
                    //アイテムオブジェクトを削除
                    Destroy(seeObjects);
                    Debug.Log("本を取得しました");
                    //itemCountsを増やす
                    itemCounts += 1;
                    break;
                case "Pickaxe":
                    //アイテムをアイテム欄に追加
                    itemObjects[itemCounts]= seeObjects;
                    //最大インデックス数を更新
                    maxActiveItemIndex=maxActiveItemIndex>4?4:maxActiveItemIndex+1;
                    itemsrot[itemCounts].sprite= GameManager.Instance.pickaxe.sprite;
                    itemsrot[itemCounts].color=new Color(1,1,1,1);
                    itemsrotChildrenText[itemCounts].color=new Color(0,0,0,1);
                    //アイテムオブジェクトを削除
                    Destroy(seeObjects);
                    Debug.Log("つるはしを取得しました");
                    //itemCountsを増やす
                    itemCounts += 1;
                    break;
                //他のアイテムもここに追加
            }


            seeObjects = null;
            
            //Debug.Log("ItemCount:"+ itemCounts);
        }
        if (Input.GetKeyDown(KeyCode.E) && isTereport)
        {
            Debug.Log("テレポートしました");
            //テレポート処理をここに追加
            switch (seeObjects.name)
            {
                case "CastleGate":
                    PlayerPosition=this.transform.position;
                    this.transform.position=new Vector3(303.7f,0f,875f);
                    break;
                case "SanctuaryGate":
                    PlayerPosition=this.transform.position;
                    this.transform.position=new Vector3(508f,15.5f,-1190f);
                    break;
                //他のテレポートもここに追加
            }
            isTereport = false;
            seeObjects.GetComponent<Outline>().enabled = false;
            seeObjects = null;
        }
        if (Input.GetKeyDown(KeyCode.E) && !isGetItem && !isTereport && seeObjects!=null)
        {
            Debug.Log("テキストウィンドウ表示");
            //テキストウィンドウ表示処理をここに追加
            switch (seeObjects.name)
            {
                case "Hint1":
                    scenario=hintscenarios[0];
                    break;
                case "Hint2":
                    scenario=hintscenarios[1];
                    break;
                case "Hint3":
                    scenario=hintscenarios[2];
                    break;
                default:
                    break;
            }
            TextWindow.enabled = true;
            textManager.StartText(scenario);
            
        }
        
        //マウスホイールの入力を取得
        mouseScrollDelta=Input.mouseScrollDelta.y * sensitivity;

        //mauseScrollDeltaの値に応じてactiveItemIndexを増減
        if(activeItemIndex>=0 && activeItemIndex<maxActiveItemIndex)
        {
            activeItemIndex+= (int)mouseScrollDelta;
            if(activeItemIndex>=maxActiveItemIndex)
            {
                activeItemIndex=maxActiveItemIndex-1;
            }
            else if(activeItemIndex<0)
            {
                activeItemIndex=0;
            }
            Debug.Log("ActiveItemIndex Changed:"+ activeItemIndex);
            mouseScrollDelta=0;
        }
        for(int i=0;i<maxActiveItemIndex;i++)
        {
            
            if(i==activeItemIndex)
            {
                itemsrotChildrenText[i].color=Color.yellow;
            }
            else
            {
                itemsrotChildrenText[i].color = Color.black;
            }
        }

       

    }
}
