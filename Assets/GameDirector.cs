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

    public TextMeshProUGUI timeUI;  // TextMeshPro�p��UI�e�L�X�g    
    public TextMeshProUGUI countUI;

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // �O�񉟂��ꂽ�{�^�����L�^

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (isGameStarted && time > 0)
        {
            UpdateTime();   // ���Ԃ̍X�V����
            CheckKeyInput();   // �L�[�{�[�h���͂̊m�F����
            CheckGameOver();   // �Q�[���I�[�o�[����

        }
    }

    // ���Ԃ����炵��UI���X�V���鏈��
    void UpdateTime()
    {
        time -= Time.deltaTime;
        timeUI.text = time.ToString("F1");
    }

    // �L�[�{�[�h���͂��������ꍇ�̏���
    void CheckKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastButtonPressed != "L")
        {
            count++;
            lastButtonPressed = "L";
            UpdateCountUI();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            count++;
            lastButtonPressed = "R";
            UpdateCountUI();
        }
    }

    // �{�^��A�������ꂽ�Ƃ��̏���
    public void OnButtonLPressed()
    {
        if (lastButtonPressed != "L")
        {
            count++;
            lastButtonPressed = "L";
            UpdateCountUI();
        }
    }

    // �{�^��B�������ꂽ�Ƃ��̏���
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

    // �Q�[���I�[�o�[���m�F���鏈��
    void CheckGameOver()
    {
        if (time <= 0)
        {
            GameOver();
        }
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
    }
}
