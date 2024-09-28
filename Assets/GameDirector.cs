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

    public TextMeshProUGUI timeUI;  // TextMeshPro用のUIテキスト    public GameObject gameStartUI;
    public TextMeshProUGUI countUI;
    //public TextMeshProUGUI scoreUI;

   // private bool isGameClear = false;
    private bool isGameStarted = false;

    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeUI.text =  time.ToString("F1");

        // ゲームが開始されている場合、時間を減らす処理を追加
        if (isGameStarted && time > 0)
        {

            time -= Time.deltaTime;
           // UpdateTimeUI();


            if (time <= 0)
            {
                GameOver();
            }
        }
    }

    // Button Aが押されたときの処理
    public void OnButtonLPressed()
    {
        if (lastButtonPressed != "L")
        {
            count++;
            lastButtonPressed = "L";
            //UpdateCountUI();
        }
    }

    // Button Bが押されたときの処理
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

    // 残り時間を表示するUIの更新
    void UpdateTimeUI()
    {
        timeUI.text = "Time: " + time.ToString("F1");
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
       // UpdateTimeUI();
    }
}
