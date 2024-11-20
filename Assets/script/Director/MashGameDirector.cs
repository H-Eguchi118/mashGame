using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // シーン管理用のライブラリを追加

public class MashGameDirector : MonoBehaviour, IGameDirector
{
    public float time;
    public int mashCount = 0;  // スタート時にリセットされる

    public TextMeshProUGUI timeUI;  // TextMeshPro用のUIテキスト    
    public TextMeshProUGUI countUI;
    //public Button retryButton;  // Retryボタンの参照
    //public Button selectButton;  // Selectボタンの参照
    public GameObject finishUI; // FinishUI のパネル
    public GameObject startUI;  // StartUI のパネル

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // 前回押されたボタンを記録

    [SerializeField] private GorstCharaController _gorstCharaController;  // ゲームの開始を管理するスクリプトの参照
    [SerializeField] private CountdownController _countdownController;  // CountdownControllerの参照
    [SerializeField] private AudioManager _audioManager;  // CountdownControllerの参照
    [SerializeField] private FinishController _finishController;  // CountdownControllerの参照
    [SerializeField] private BreadImageManager _breadImageManager;

    private void Start()
    {
        _gorstCharaController = FindObjectOfType<GorstCharaController>();
        if (_gorstCharaController == null)
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
        Debug.Log("mash game Started");

        isGameStarted = true;
        _audioManager.PlayStompGameBgm();
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
            mashCount++;
            lastButtonPressed = "L";
            UpdateCountUI();
            _gorstCharaController.ViewGorstL();
            _breadImageManager.CheckBreadScore();
            _audioManager.PlayStompSE();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            mashCount++;
            lastButtonPressed = "R";
            UpdateCountUI();
            _gorstCharaController.ViewGorstR();
            _audioManager.PlayStompSE();
            _breadImageManager.CheckBreadScore();

        }
    }


    // カウントを表示するUIの更新
    void UpdateCountUI()
    {
        countUI.text = mashCount.ToString();
    }


    // ゲームオーバーを確認する処理
    void CheckGameOver()
    {
        if (time <= 0)
        {
            time = 0;
            _audioManager.StopStompGameBgm();
            GameOver();
        }
    }

    // ゲームオーバー処理
    void GameOver()
    {
        isGameStarted = false;
        Debug.Log("Game Over! Final Count: " + mashCount);

        _finishController.ShowFinishUI(mashCount);  // ゲーム終了時にスコアを表示

        finishUI.SetActive(true); // FinishUI を表示
    }
}