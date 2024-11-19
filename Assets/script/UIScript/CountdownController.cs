using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdown = 3;  // Unityから調整できるようにpublicに
    public TextMeshProUGUI countdownText;
    public Canvas startUI;          // カウントダウン用のUI

    public MonoBehaviour gameDirectorObject; // UnityのInspectorで設定する    private IGameDirector _gameDirector;
    private IGameDirector gameDirector;
    private bool isCountingDown = false; // カウントダウン中かどうかのフラグ

    void Start()
    {
        // InspectorでアタッチされたオブジェクトをIGameDirectorとしてキャスト
        if (gameDirectorObject is IGameDirector director)
        {
            gameDirector = director;
        }
        else
        {
            Debug.LogError("Assigned object does not implement IGameDirector.");
        }

        ResetCountdown();
    }

    // 画面をタップしたらカウントダウンを開始
    void Update()
    {
        if ((!isCountingDown && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))) // マウスクリックまたはタップ
        {
            StartCoroutine(StartCountdown());
            isCountingDown = true;  // フラグを立ててカウントダウンが二重に始まらないようにする
        }
    }

    // カウントダウンを初期状態にリセットするメソッド
    public void ResetCountdown()
    {
        countdown = 3;  // カウントをリセット
        startUI.gameObject.SetActive(true);  // StartUIを表示
        //countdownText.text = "Tap Screen";  // 「Tap Screen」を表示
        isCountingDown = false;  // カウントダウン中フラグをリセット
    }


    IEnumerator StartCountdown()
    {

        // カウントダウン開始
        while (countdown > 0)
        {
            // 数字のフォントサイズを700に設定
            //countdownText.fontSize = 700;
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1.0f);  // 1秒待機
            countdown--;
        }

        //countdownText.fontSize = 230;
        countdownText.text = "Start!";
        yield return new WaitForSeconds(1.0f);

        // カウントダウンUIを非表示
        startUI.gameObject.SetActive(false);

        // カウントダウンが終了したらゲーム開始
        //mashGameDirector.StartGame();
        gameDirector?.StartGame();  // ディレクターのStartGameを呼び出し


    }
}
