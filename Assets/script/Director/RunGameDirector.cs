using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RunGameDirector : MonoBehaviour, IGameDirector
{

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Item _item;
    [SerializeField] private SaveLoadManager _saveLoadManager;
   

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI goalText;

    private float time;
    private bool isTimerRunning = true;//タイマーが動作しているか
    private bool isGameStarted = false;

    public string targetSceneName = null;



    void Start()
    {
        time = 0;
        goalText.gameObject.SetActive(false);
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            if (isTimerRunning)
            {
                time += Time.deltaTime;
                timeText.text = time.ToString("F1");
            }
        }
        

    }
    public void StartGame()
    {
        isGameStarted = true;

        // Ranゲーム専用の開始処理
        Debug.Log("Ran Game Started");

        _audioManager.PlayRunGameBgm();

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
        _audioManager.StopRunGameBgm();
        SaveTimeData();
        SaveSceneName();

    }

    public void SaveSceneName()
    {
        _saveLoadManager.SaveSceneName(SceneManager.GetActiveScene().name);

    }
}
