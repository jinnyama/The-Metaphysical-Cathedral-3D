using UnityEngine;

public class SignText : MonoBehaviour
{
    //playerが看板に触れたときにテキストウィンドウに表示する用
    
    //private string[] signText={"空が　　　いる","　　　　　　　いる祠",　"がかかっている","檻に　　　　いる"};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //テキストウィンドウにsignText配列の０ばんめをうけとってる→一般化⚠
            TextScript.Instance.signtext.text = TextScript.Instance.signText[0];
        }        
    }
}
