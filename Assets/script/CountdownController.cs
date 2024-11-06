using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdown = 3;  // Unityから調整できるようにpublicに
    public TextMeshProUGUI countdownText;
    public Canvas startUI;          // カウントダウン用のUI

    private GameDirector gameDirector;  // ゲームの開始を管理するスクリプトの参照

    void Start()
    {
        // GameDirectorスクリプトを取得して関連付け
        gameDirector = FindObjectOfType<GameDirector>();

        ResetCountdown();
    }

    // 画面をタップしたらカウントダウンを開始
    void Update()
    {
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))  // マウスクリックまたはタップ
        {
            StartCoroutine(StartCountdown());
        }
    }

    // カウントダウンを初期状態にリセットするメソッド
    public void ResetCountdown()
    {
        countdown = 3;  // カウントをリセット
        startUI.gameObject.SetActive(true);  // StartUIを表示
        countdownText.text = "Tap Screen";  // 「Tap Screen」を表示
    }

    IEnumerator StartCountdown()
    {

        // カウントダウン開始
        while (countdown > 0)
        {
            // 数字のフォントサイズを700に設定
            countdownText.fontSize = 700;
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1.0f);  // 1秒待機
            countdown--;
        }

        countdownText.fontSize = 230;
        countdownText.text = "Start!";
        yield return new WaitForSeconds(1.0f);

        // カウントダウンが終了したらゲーム開始
        gameDirector.StartGame();

        // カウントダウンUIを非表示
        startUI.gameObject.SetActive(false);
    }
}
