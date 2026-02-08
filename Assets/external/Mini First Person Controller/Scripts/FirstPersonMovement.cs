using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rig;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!PlayerScript.instance.ismoveplayer)return;
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        //Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        // ===== ここから変更 =====

        // WASD入力で方向を作る
        Vector2 inputDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) inputDir.y += 1f;
        if (Input.GetKey(KeyCode.S)) inputDir.y -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x += 1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x -= 1f;

        // 斜め移動が速くならないようにする
        inputDir = inputDir.normalized;

        // 入力 × スピード
        Vector2 targetVelocity = inputDir * targetMovingSpeed;

        // ===== ここまで変更 =====

        // Apply movement.
        rig.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, GetComponent<Rigidbody>().linearVelocity.y, targetVelocity.y);
    }
}