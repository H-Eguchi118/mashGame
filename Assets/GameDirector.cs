using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使うために必要

public class GameDirector : MonoBehaviour
{
    public float time = 30.0f;
    public int startTime = 3;
    public int count = 0;
    public int score = 0;

    public TextMeshProUGUI timeUI;  // TextMeshPro用のUIテキスト    
    public TextMeshProUGUI countUI;

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (isGameStarted && time > 0)
        {
            UpdateTime();   // 時間の更新処理
            CheckKeyInput();   // キーボード入力の確認処理
            CheckGameOver();   // ゲームオーバー判定

        }
    }

    // 時間を減らしてUIを更新する処理
    void UpdateTime()
    {
        time -= Time.deltaTime;
        timeUI.text = time.ToString("F1");
    }

    // キーボード入力があった場合の処理
    void CheckKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastButtonPressed != "L")
        {
            count++;
            lastButtonPressed = "L";
            UpdateCountUI();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            count++;
            lastButtonPressed = "R";
            UpdateCountUI();
        }
    }

    // ボタンAが押されたときの処理
    public void OnButtonLPressed()
    {
        if (lastButtonPressed != "L")
        {
            count++;
            lastButtonPressed = "L";
            UpdateCountUI();
        }
    }

    // ボタンBが押されたときの処理
    public void OnButtonRPressed()
    {
        if (lastButtonPressed != "R")
        {
            count++;
            lastButtonPressed = "R";
            UpdateCountUI();
        }
    }

    // カウントを表示するUIの更新
    void UpdateCountUI()
    {
        countUI.text = count.ToString();
    }

    // ゲームオーバーを確認する処理
    void CheckGameOver()
    {
        if (time <= 0)
        {
            GameOver();
        }
    }

    // ゲームオーバー処理
    void GameOver()
    {
        isGameStarted = false;
        Debug.Log("Game Over! Final Count: " + count);
    }

    // ゲーム開始時の処理
    public void StartGame()
    {
        isGameStarted = true;
        time = 30.0f; // ゲーム開始時に時間をリセット
        count = 0; // カウントもリセット
        lastButtonPressed = ""; // 最初はどのボタンも押されていない状態
        UpdateCountUI();
    }
}
