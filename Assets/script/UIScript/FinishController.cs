using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using Boomerang2DFramework.Framework.AudioManagement;
public class FinishController : MonoBehaviour
{

    private int mashCount;   // ゲームの最終カウント
    private int highScore;   // ハイスコア
    private string filePath; // JSONファイルのパス

    [SerializeField] private AudioManager _audioManager;  // ゲームの開始を管理するスクリプトの参照
    [SerializeField] private MashGameDirector _mashGameDirector;  // ゲームの開始を管理するスクリプトの参照
    [SerializeField] private BreadImageManager _breadManager;  // ゲームの開始を管理するスクリプトの参照
    [SerializeField]private LoadImageManager _loadImageManager;

    public FinishUI finishUI;


    void Start()
    {
        _loadImageManager.HideLoadImage();
        filePath = Application.persistentDataPath + "/scoreData.json";  // 保存先ファイルパスを設定
        finishUI.finishCanvas.gameObject.SetActive(false);  // ゲーム開始時は終了画面を非表示
        finishUI.newText.gameObject.SetActive(false);   // 初期状態では「new Record!」を非表示
        LoadHighScore();                       // 保存されたハイスコアを読み込む
        onClickButton();
    }

    // ゲームが終了したときに呼ばれるメソッド
    public void ShowFinishUI(int finalCount)
    {
        _audioManager.PlayCountFinishSound();
        finishUI.finishCanvas.gameObject.SetActive(true);  // 終了画面を表示
        mashCount = finalCount;
        //finishUI.finishText.text = "FINISH!";
        finishUI.countText.text = "踏んだ回数: " + mashCount.ToString();

        //パンのスコア結果表示
        _breadManager.BreadScoreSet();
        StartCoroutine(_breadManager.DisplayBreadData());

        _breadManager.AddTotalMoneyScore(out int totalMoney);


        // ハイスコアの更新処理
        if (mashCount > highScore)
        {
            highScore = mashCount;
            finishUI.newText.gameObject.SetActive(true);  // 「new Record!」を表示
            StartCoroutine(BlinkNewRecordText()); // 点滅コルーチンを開始

            SaveHighScore();  // 新しいハイスコアを保存
        }

        finishUI.highScoreText.text = "最高記録: " + highScore.ToString(); // ハイスコアを表示
    }

    // ハイスコアをJSONファイルに保存する処理
    void SaveHighScore()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.highScore = highScore;

        string json = JsonUtility.ToJson(scoreData);  // JSONに変換
        File.WriteAllText(filePath, json);  // ファイルに書き込み
    }

    // JSONファイルからハイスコアを読み込む処理
    void LoadHighScore()
    {
        if (File.Exists(filePath))  // ファイルが存在する場合
        {
            string json = File.ReadAllText(filePath);  // ファイルを読み込む
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);  // JSONからオブジェクトに変換
            highScore = scoreData.highScore;
        }
        else
        {
            highScore = 0;  // ファイルが存在しない場合はハイスコアを0に初期化
        }
    }

    // 「new Record!」を点滅させるコルーチン
    IEnumerator BlinkNewRecordText()
    {
        while (true)
        {
            finishUI.newText.enabled = !finishUI.newText.enabled;  // 表示と非表示を切り替える
            yield return new WaitForSeconds(0.5f);  // 0.5秒ごとに切り替え
        }
    }
    private void onClickButton()
    {
        finishUI.reTryGameButton.onClick.AddListener(() => StartCoroutine(ReTryGame()));
        finishUI.returnBuutton.onClick.AddListener(() => StartCoroutine(GotoStartScene()));
        finishUI.goShopBuutton.onClick.AddListener(() => StartCoroutine(GotoShopScene()));
    }


    private IEnumerator ReTryGame()
    {
        _audioManager.PlayDecisionButtonSound();
        _loadImageManager.DisplayLoadInCanvas();

        yield return new WaitForSeconds(4.0f);//4秒待機

        SceneManager.LoadScene("StompGamescene");
    }

    private IEnumerator GotoStartScene()
    {
        _audioManager.PlayDecisionButtonSound();

        _loadImageManager.DisplayLoadInCanvas();

        yield return new WaitForSeconds(4.0f);//4秒待機

        SceneManager.LoadScene("StartScene");
    }
    private IEnumerator GotoShopScene()
    {
        _audioManager.PlayDecisionButtonSound();

        _loadImageManager.DisplayLoadInCanvas();

        yield return new WaitForSeconds(4.0f);//4秒待機

        SceneManager.LoadScene("ShopScene");
    }
}

[System.Serializable]
public class FinishUI
{
    public Canvas finishCanvas;               // 終了画面のCanvas
    public TextMeshProUGUI finishText;    // 終了メッセージ用のText
    public TextMeshProUGUI countText;     // 最終カウントを表示するText
    public TextMeshProUGUI highScoreText; // ハイスコアを表示するText
    public TextMeshProUGUI newText; // ハイスコアを表示するText

    public Button reTryGameButton;
    public Button returnBuutton;
    public Button goShopBuutton;
}



