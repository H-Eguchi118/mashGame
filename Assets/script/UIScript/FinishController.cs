using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class FinishController : MonoBehaviour
{

    private int mashCount;                   // ゲームの最終カウント
    private int highScore;               // ハイスコア
    private string filePath;             // JSONファイルのパス

    //各パンのスコア
    private int carryScore = 0;
    private int croissantScore = 0;
    private int richBreadScore = 0;
    private int breadScore = 0;

    [SerializeField] private GorstCharaController _gorstCharaController;  // ゲームの開始を管理するスクリプトの参照
    [SerializeField] private AudioManager _audioManager;  // ゲームの開始を管理するスクリプトの参照

    public FinishUI finishUI;


    void Start()
    {
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
        finishUI.rankImage.gameObject.SetActive(true);
        RankImageView();


        _gorstCharaController.ViewStanding();



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

    private void RankImageView()
    {
        if (mashCount >= 300)
        {
            finishUI.rankImage.sprite = finishUI.carry1;
            finishUI.rankText.text = "みんな大好きカレーパン";
        }
        else if (mashCount >= 200)
        {
            finishUI.rankImage.sprite = finishUI.croissant2;
            finishUI.rankText.text = "ゆうがなクロワッサン";

        }
        else if (mashCount >= 100)
        {
            finishUI.rankImage.sprite = finishUI.richBread3;
            finishUI.rankText.text = "ちょっとリッチな食パン";

        }
        else
        {
            finishUI.rankImage.sprite = finishUI.dread4;
            finishUI.rankText.text = "ふつうの食パン";

            if (mashCount <= 9)
            {
                carryScore++;
            }

        }
    }

    private void onClickButton()
    {
        finishUI.reTryGameButton.onClick.AddListener(() => ReTryGame());
        finishUI.returnBuutton.onClick.AddListener(() => GotoStartScene());
    }


    private void ReTryGame()
    {
        SceneManager.LoadScene("StompGamescene");
    }

    private void GotoStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void SaveBreadData()
    {

    }
    public void LoadeBreadData()
    {

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

    public Image rankImage;//ランクごとのイメージを表示する枠
    public TextMeshProUGUI rankText; // ランクの名前を表示するText

    //各ランクイメージ
    public Sprite carry1;
    public Sprite croissant2;
    public Sprite richBread3;
    public Sprite dread4;


}

