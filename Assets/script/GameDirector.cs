using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    public float time = 30.0f;
    public int count = 0;  // �X�^�[�g���Ƀ��Z�b�g�����
    public int score = 0;

    public TextMeshProUGUI timeUI;  // TextMeshPro�p��UI�e�L�X�g    
    public TextMeshProUGUI countUI;

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // �O�񉟂��ꂽ�{�^�����L�^

    void Update()
    {
        if (isGameStarted && time > 0)
        {
            UpdateTime();   // ���Ԃ̍X�V����
            CheckKeyInput();   // �L�[�{�[�h���͂̊m�F����
            CheckGameOver();   // �Q�[���I�[�o�[����
        }
    }

    // �Q�[���J�n���ɌĂяo�����
    public void StartGame()
    {
        isGameStarted = true;
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
}
