using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro���g�����߂ɕK�v

public class GameDirector : MonoBehaviour
{
    public float time = 30.0f;
    public int startTime = 3;
    public int count = 0;
    public int score = 0;

    public TextMeshProUGUI timeUI;  // TextMeshPro�p��UI�e�L�X�g    public GameObject gameStartUI;
    public TextMeshProUGUI countUI;
    //public TextMeshProUGUI scoreUI;

   // private bool isGameClear = false;
    private bool isGameStarted = false;

    private string lastButtonPressed = ""; // �O�񉟂��ꂽ�{�^�����L�^

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeUI.text =  time.ToString("F1");

        // �Q�[�����J�n����Ă���ꍇ�A���Ԃ����炷������ǉ�
        if (isGameStarted && time > 0)
        {

            time -= Time.deltaTime;
           // UpdateTimeUI();


            if (time <= 0)
            {
                GameOver();
            }
        }
    }

    // Button A�������ꂽ�Ƃ��̏���
    public void OnButtonLPressed()
    {
        if (lastButtonPressed != "L")
        {
            count++;
            lastButtonPressed = "L";
            //UpdateCountUI();
        }
    }

    // Button B�������ꂽ�Ƃ��̏���
    public void OnButtonRPressed()
    {
        if (lastButtonPressed != "R")
        {
            count++;
            lastButtonPressed = "R";
           UpdateCountUI();
        }
    }

    // �J�E���g��\������UI�̍X�V
    void UpdateCountUI()
    {
        countUI.text = count.ToString();
    }

    // �c�莞�Ԃ�\������UI�̍X�V
    void UpdateTimeUI()
    {
        timeUI.text = "Time: " + time.ToString("F1");
    }

    // �Q�[���I�[�o�[����
    void GameOver()
    {
        isGameStarted = false;
        Debug.Log("Game Over! Final Count: " + count);
    }

    // �Q�[���J�n���̏���
    public void StartGame()
    {
        isGameStarted = true;
        time = 30.0f; // �Q�[���J�n���Ɏ��Ԃ����Z�b�g
        count = 0; // �J�E���g�����Z�b�g
        lastButtonPressed = ""; // �ŏ��͂ǂ̃{�^����������Ă��Ȃ����
        UpdateCountUI();
       // UpdateTimeUI();
    }
}
