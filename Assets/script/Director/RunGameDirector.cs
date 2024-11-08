using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // シーン管理用のライブラリを追加

public class RunGameDirector : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    //[SerializeField] private Item _item;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField]private TextMeshProUGUI goalText;

    //public TextMeshProUGUI flowersScoreText;//花の所持数のテキスト
    //public TextMeshProUGUI rimitTimeText;//フライト制限時間のテキスト



    private float time;
    private bool isTimerRunning = true;//タイマーが動作しているか

    void Start()
    {
        time = 0;
        goalText.gameObject.SetActive(false);
        _audioManager.PlayRunningBgm();
        //flowersScoreText.gameObject.SetActive(true);
        //flowersScoreText.text = _item.flowersScore.ToString();


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
