using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // シーン管理用のライブラリを追加

public class RunGameDirector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float time;

    void Start()
    {
        time = 0;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = time.ToString("F1");

    }


}
