using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mouseX) > 0.001f)
        {
            transform.RotateAround(transform.position, Vector3.up, mouseX);
        }
        if (Mathf.Abs(mouseY) > 0.001f)
        {
            transform.RotateAround(transform.position, transform.right, -mouseY);
        }

    }
}
