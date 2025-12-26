using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<GameObject > itemObjects = new List<GameObject>();
    public int itemCounts;
    [SerializeField] Camera     fpsCam;             // カメラ
    [SerializeField] float      distance = 0.8f;    // 検出可能な距離

    // Start is called before the first frame update
    void Start()
    {
        
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
        
        // なにかを検出したら
        if (isHit)
        {
            // LogにHitしたオブジェクト名を出力
            Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);
        }
        itemObjects.Add(raycastHit.collider.gameObject);

    }
}
