using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Boomerang2DFramework.Framework.AudioManagement;

public class FinishController : MonoBehaviour
{
    public Canvas finishUI;               // 終了画面のCanvas
    public TextMeshProUGUI finishText;    // 終了メッセージ用のText
    public TextMeshProUGUI countText;     // 最終カウントを表示するText
    public TextMeshProUGUI highScoreText; // ハイスコアを表示するText
    public TextMeshProUGUI newText; // ハイスコアを表示するText

    private int count;                   // ゲームの最終カウント
    private int highScore;               // ハイスコア
    private string filePath;             // JSONファイルのパス

    [SerializeField] private GorstCharaController _gorstCharaController;  // ゲームの開始を管理するスクリプトの参照
    [SerializeField] private AudioManager _audioManager;  // ゲームの開始を管理するスクリプトの参照


    void Start()
    {
        filePath = Application.persistentDataPath + "/scoreData.json";  // 保存先ファイルパスを設定
        finishUI.gameObject.SetActive(false);  // ゲーム開始時は終了画面を非表示
        newText.gameObject.SetActive(false);   // 初期状態では「new Record!」を非表示
        LoadHighScore();                       // 保存されたハイスコアを読み込む
    }

    // ゲームが終了したときに呼ばれるメソッド
    public void ShowFinishUI(int finalCount)
    {
        _audioManager.PlayCountFinishSound();
        finishUI.gameObject.SetActive(true);  // 終了画面を表示
        count = finalCount;
        finishText.text = "FINISH!";
        countText.text = "Score: " + count.ToString();

        _gorstCharaController.ViewStanding();



        // ハイスコアの更新処理
        if (count > highScore)
        {
            highScore = count;
            newText.text = "new Record!";
            newText.gameObject.SetActive(true);  // 「new Record!」を表示
            StartCoroutine(BlinkNewRecordText()); // 点滅コルーチンを開始

            SaveHighScore();  // 新しいハイスコアを保存
        }

        highScoreText.text = "High Score: " + highScore.ToString(); // ハイスコアを表示
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
            newText.enabled = !newText.enabled;  // 表示と非表示を切り替える
            yield return new WaitForSeconds(0.5f);  // 0.5秒ごとに切り替え
        }
    }
}
