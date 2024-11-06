using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // シーン管理用のライブラリを追加

public class RunGameDirector : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI goalText;
    private float time;
    private bool isTimerRunning = true;//タイマーが動作しているか

    void Start()
    {
        time = 0;
        goalText.gameObject.SetActive(false);
        _audioManager.PlayRunningBgm();

    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            time += Time.deltaTime;
            timerText.text = time.ToString("F1");
        }

    }

    public void StopTimer()
    {
        isTimerRunning = false;
        goalText.gameObject.SetActive(true);
        _audioManager.StopRunningBgm();

    }


}
