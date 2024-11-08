using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RunPlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    [Header("Movement Settings")]
    public float JumpForce;
    public float baseRunForce;
    public float maxRunForce;
    public float decayRate;
    public float accelerationFactor;

    private float currentRunForce;
    private float lastInputTime = 0f;
    public bool isGrounded = true;  // 地面にいるかどうかの判定

    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    [SerializeField] private PlayerVisualsController _visualsController; // スプライト制御スクリプトの参照

    private Animator animator;

    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentRunForce = baseRunForce;
    }

    void Update()
    {
        HandleMovementInput();
        DecelerateOverTime();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastButtonPressed != "L")
        {
            lastButtonPressed = "L";
            //_visualsController.UpdateFootSprite("L");
            RunRight();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            lastButtonPressed = "R";
            //_visualsController.UpdateFootSprite("R");
            RunRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _visualsController.PlayJumpSound();
            Jump();
        }
    }

    //走る動作
    void RunRight()
    {
        CalculateRunForce();
        rigid2D.AddForce(new Vector2(currentRunForce, 0), ForceMode2D.Impulse);
        animator.SetBool("isRunning", true);

        if (!isGrounded)
            animator.SetBool("isRunning", false);



    }
    //ジャンプ動作
    void Jump()
    {
        if (Mathf.Abs(rigid2D.velocity.y) < 0.01f && isGrounded)
        {
            rigid2D.AddForce(transform.up * JumpForce);

            // 空中にいるのでisGroundedをfalseにする
            isGrounded = false;
            animator.SetBool("isJumping", true);

        }

    }

    //一定時間入力がなかった場合の減速処理
    void DecelerateOverTime()
    {
        if (isGrounded && Time.time - lastInputTime > 0.5f)
        {
            animator.SetBool("isRunning", false);

            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y); // Y方向の速度はそのまま
        }
    }

    //加速処理
    void CalculateRunForce()
    {
        float currentTime = Time.time;
        float interval = currentTime - lastInputTime;

        if (interval > 0)
        {
            float scaledInterval = interval * accelerationFactor;
            currentRunForce = Mathf.Lerp(currentRunForce, maxRunForce, scaledInterval);
            lastInputTime = currentTime;
        }
    }

    //Collision2D接触処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが地面に接触した場合、isGroundedをtrueにする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);


        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            _visualsController.PlayDamageSound();
            rigid2D.velocity = Vector2.zero;
            Debug.Log(rigid2D.velocity);

            StartCoroutine(_visualsController.BlinkSprite());//点滅処理を開始
            Debug.Log("点滅処理を呼び出しました");

            animator.SetTrigger("Damage");
        }


    }



}
