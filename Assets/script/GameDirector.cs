using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    public float time = 30.0f;
    public int count = 0;  // スタート時にリセットされる
    public int score = 0;

    public TextMeshProUGUI timeUI;  // TextMeshPro用のUIテキスト    
    public TextMeshProUGUI countUI;

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    void Update()
    {
        if (isGameStarted && time > 0)
        {
            UpdateTime();   // 時間の更新処理
            CheckKeyInput();   // キーボード入力の確認処理
            CheckGameOver();   // ゲームオーバー判定
        }
    }

    // ゲーム開始時に呼び出される
    public void StartGame()
    {
        isGameStarted = true;
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
}
