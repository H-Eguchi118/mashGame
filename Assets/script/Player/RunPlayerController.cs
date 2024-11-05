using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunPlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    [Header("Movement Settings")]
    public float JumpForce;        // 最小の走る力
    public float baseRunForce;        // 最小の走る力
    public float maxRunForce;        // 最大の走る力
    public float decayRate;           // 減速率
    public float accelerationFactor;  // 加速の影響を調整する係数（小さいほどゆっくり加速）

    private float currentRunForce;      // 現在の加速力
                                        // private string lastButtonPressed = ""; // 前回押されたボタンを記録
    private float lastInputTime = 0f;      // 最後にボタンが押された時間
    private float lastInterval = 0f;       // 連打の速さを記録

    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    // 追加: スプライトの参照を保持するための変数
    public Sprite standingSprite; // 立ち絵のスプライト
    public Sprite rightFootSprite; // 右足上げのスプライト
    public Sprite leftFootSprite; // 左足上げのスプライト

    private SpriteRenderer spriteRenderer; // スプライトレンダラーの参照

    [SerializeField] private RunGameDirector gameDirector;

    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        currentRunForce = baseRunForce;  // 初期値を基本の走る力に設定

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standingSprite;//初期スプライトを設定

    }

    void Update()
    {
        PlayerMoving();
        DecelerateOverTime();
    }

    //動作
    public void PlayerMoving()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastButtonPressed != "L")
        {
            lastButtonPressed = "L";
            RunRight();
            spriteRenderer.sprite = leftFootSprite;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            lastButtonPressed = "R";
            RunRight();
            spriteRenderer.sprite = rightFootSprite;

        }
        else if ((Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();

        }
    }

    // 右方向に走るための力を加えるメソッド
    void RunRight()
    {
        //連打速度
        RunForce();

        Debug.Log("Current Run Force: " + currentRunForce); // デバッグ用

        // 力を加える
        rigid2D.AddForce(new Vector2(currentRunForce, 0), ForceMode2D.Impulse);

    }

    // 右方向に走るための力を加えるメソッド
    //void RunLeft()
    //{
    //    //連打速度
    //    RunForce();

    // 現在のX軸の速度に加速を加える
    //    rigid2D.AddForce(new Vector2(-currentRunForce, 0), ForceMode2D.Force);

    //}

    void Jump()
    {
        if (Mathf.Abs(rigid2D.velocity.y) < 0.01f)//地面にいるときのみジャンプ
            rigid2D.AddForce(transform.up * this.JumpForce);
    }


    // 一定時間連打がないときに徐々に減速する
    void DecelerateOverTime()
    {
        if (Time.time - lastInputTime > 0.5f)  // 0.5秒間入力がない場合減速開始
        {
            currentRunForce = Mathf.Max(currentRunForce - decayRate * Time.deltaTime, baseRunForce);  // 最小値を超えないように
        }
    }

    //連打すると加速する
    void RunForce()
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

            // 最後に入力があった時間を更新
            lastInputTime = currentTime;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            rigid2D.velocity = Vector2.zero;
            Debug.Log("hit enemy");
            Debug.Log(rigid2D.velocity);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");
            gameDirector.StopTimer();
        }
    }
}
