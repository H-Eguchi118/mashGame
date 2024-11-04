using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    [Header("Movement Settings")]
    public float baseRunForce;        // 最小の走る力（インスペクターで設定）
    public float maxRunForce;        // 最大の走る力（インスペクターで設定）
    public float decayRate;           // 減速率（インスペクターで設定）
    public float accelerationFactor;  // 加速の影響を調整する係数（小さいほどゆっくり加速）

    private float currentRunForce;      // 現在の加速力
    private string lastButtonPressed = ""; // 前回押されたボタンを記録
    private float lastInputTime = 0f;      // 最後にボタンが押された時間
    private float lastInterval = 0f;       // 連打の速さを記録

    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        currentRunForce = baseRunForce;  // 初期値を基本の走る力に設定
    }

    void Update()
    {
        GorstRunning();
        DecelerateOverTime();
    }

    // 交互に連打して右方向に加速しながら走る動作
    public void GorstRunning()
    {
        // 左矢印または右矢印が押され、前回の入力と異なるボタンが押されたとき
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastButtonPressed != "L")
        {
            lastButtonPressed = "L";
            RunRight();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            lastButtonPressed = "R";
            RunRight();
        }
    }

    // 右方向に走るための力を加えるメソッド
    void RunRight()
    {
        // 連打速度の計算
        float currentTime = Time.time;
        float interval = currentTime - lastInputTime;

        // 連打が速いほど currentRunForce をゆっくり大きくする
        if (interval > 0)
        {
            // accelerationFactor を使って加速の影響を抑える
            float scaledInterval = interval * accelerationFactor;
            currentRunForce = Mathf.Lerp(currentRunForce, maxRunForce, scaledInterval);
        }

        // 力を加える
        rigid2D.AddForce(new Vector2(currentRunForce, 0), ForceMode2D.Impulse);

        // 最後に入力があった時間を更新
        lastInputTime = currentTime;
    }

    // 一定時間連打がないときに徐々に減速する
    void DecelerateOverTime()
    {
        if (Time.time - lastInputTime > 0.5f)  // 0.5秒間入力がない場合減速開始
        {
            currentRunForce = Mathf.Max(currentRunForce - decayRate * Time.deltaTime, baseRunForce);  // 最小値を超えないように
        }
    }
}
