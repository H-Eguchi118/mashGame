using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // シーン管理用のライブラリを追加

public class GameDirector : MonoBehaviour
{
    public float time = 30.0f;
    public int count = 0;  // スタート時にリセットされる
    public int score = 0;

    public TextMeshProUGUI timeUI;  // TextMeshPro用のUIテキスト    
    public TextMeshProUGUI countUI;
    public Button retryButton;  // Retryボタンの参照
    public Button selectButton;  // Selectボタンの参照
    public GameObject finishUI; // FinishUI のパネル
    public GameObject startUI;  // StartUI のパネル

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    public GorstCharaController gorstCharaController;  // ゲームの開始を管理するスクリプトの参照
    public CountdownController countdownController;  // CountdownControllerの参照

    private void Start()
    {
        gorstCharaController = FindObjectOfType<GorstCharaController>();
        if (gorstCharaController == null)
        {
            Debug.LogError("GorstCharaController is not found.");
        }
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

    // ゲーム開始時に呼び出される
    public void StartGame()
    {
        isGameStarted = true;
        count = 0; // カウントをリセット
        score = 0; // スコアをリセット
        time = 3.0f; // 時間をリセット

        lastButtonPressed = ""; // 最後に押されたボタンの記録もリセット
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
            gorstCharaController.ViewGorstL();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            count++;
            lastButtonPressed = "R";
            UpdateCountUI();
            gorstCharaController.ViewGorstR();

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
            time = 0;
            GameOver();
        }
    }

    // ゲームオーバー処理
    void GameOver()
    {
        isGameStarted = false;
        Debug.Log("Game Over! Final Count: " + count);

        FinishController finishController = FindObjectOfType<FinishController>();
        finishController.ShowFinishUI(count);  // ゲーム終了時にスコアを表示


        finishUI.SetActive(true); // FinishUI を表示
        startUI.SetActive(false); // StartUI を非表示

    }


    // RETRYボタンが押された時の処理
    public void PushRetryBtn()
    {
        Debug.Log("Retry button pressed");

        // CountdownControllerを初期状態に戻す
        if (countdownController != null)
        {
            countdownController.ResetCountdown();  // カウントダウンのリセット
        }
        else
        {
            Debug.LogError("CountdownController is not found.");
        }

        // ゲーム画面のUI設定のリセット
        finishUI.SetActive(false);  // FinishUIを非表示
        startUI.SetActive(true);    // StartUIを再表示
    }

    public void PushSetectBtn()
    {
        gorstCharaController.ViewMoving();
        SceneManager.LoadScene("SelectScene");  // SelectSceneに移動

    }

}