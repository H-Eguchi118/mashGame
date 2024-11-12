using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using TreeEditor; // シーン管理用のライブラリを追加

public class RunGameDirector : MonoBehaviour
{

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Item _item;
    [SerializeField] private SaveLoadManager _saveLoadManager;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI goalText;
    [SerializeField] private GoalUI goalUI;  // GoalCanvasのUI要素をまとめたもの

    private float time;
    private bool isTimerRunning = true;//タイマーが動作しているか


    void Start()
    {
        time = 0;
        goalText.gameObject.SetActive(false);
        //GoalCanvas.gameObject.SetActive(false);
        goalUI.goalCanvas.enabled = false;
        _audioManager.PlayRunningBgm();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            time += Time.deltaTime;
            timeText.text = time.ToString("F1");
        }

    }

    private void SaveTimeData()
    {
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.Save(); // データを保存
        Debug.Log("タイムデータを保存しました");
    }

    public void LoadTimeData()
    {
        _saveLoadManager.LoadTimeData(out time);
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        goalText.gameObject.SetActive(true);
        _audioManager.StopRunningBgm();
        SaveTimeData();

    }

    public IEnumerator SetGoalCanvas()
    {
        Debug.Log("ゴールキャンバスを呼び出しています");
        yield return new WaitForSeconds(4.0f);//4秒待機

        goalUI.goalCanvas.enabled = true;
        Debug.Log("ゴールキャンバスを表示しました");

        goalUI.flowersText.text = "Flower：" + _item.flowersScore;  //花の最終所持数
        goalUI.bouquetText.text = "Bouquet：" + _item.bouquetScore;  //ブーケの最終所持数
        goalUI.timeText.text = "Time：" + time;  // 最終タイム

    }

}
