using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // �V�[���Ǘ��p�̃��C�u������ǉ�

public class GameDirector : MonoBehaviour
{
    public float time = 30.0f;
    public int count = 0;  // �X�^�[�g���Ƀ��Z�b�g�����
    public int score = 0;

    public TextMeshProUGUI timeUI;  // TextMeshPro�p��UI�e�L�X�g    
    public TextMeshProUGUI countUI;
    public Button retryButton;  // Retry�{�^���̎Q��
    public Button selectButton;  // Select�{�^���̎Q��
    public GameObject finishUI; // FinishUI �̃p�l��
    public GameObject startUI;  // StartUI �̃p�l��

    private bool isGameStarted = false;
    private string lastButtonPressed = ""; // �O�񉟂��ꂽ�{�^�����L�^

    public GorstCharaController gorstCharaController;  // �Q�[���̊J�n���Ǘ�����X�N���v�g�̎Q��
    public CountdownController countdownController;  // CountdownController�̎Q��

    private void Start()
    {
        gorstCharaController = FindObjectOfType<GorstCharaController>();
        if (gorstCharaController == null)
        {
            Debug.LogError("GorstCharaController is not found.");
        }
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

    // �Q�[���J�n���ɌĂяo�����
    public void StartGame()
    {
        isGameStarted = true;
        count = 0; // �J�E���g�����Z�b�g
        score = 0; // �X�R�A�����Z�b�g
        time = 3.0f; // ���Ԃ����Z�b�g

        lastButtonPressed = ""; // �Ō�ɉ����ꂽ�{�^���̋L�^�����Z�b�g
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
            gorstCharaController.ViewGorstL();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            count++;
            lastButtonPressed = "R";
            UpdateCountUI();
            gorstCharaController.ViewGorstR();

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
            time = 0;
            GameOver();
        }
    }

    // �Q�[���I�[�o�[����
    void GameOver()
    {
        isGameStarted = false;
        Debug.Log("Game Over! Final Count: " + count);

        FinishController finishController = FindObjectOfType<FinishController>();
        finishController.ShowFinishUI(count);  // �Q�[���I�����ɃX�R�A��\��


        finishUI.SetActive(true); // FinishUI ��\��
        startUI.SetActive(false); // StartUI ���\��

    }


    // RETRY�{�^���������ꂽ���̏���
    public void PushRetryBtn()
    {
        Debug.Log("Retry button pressed");

        // CountdownController��������Ԃɖ߂�
        if (countdownController != null)
        {
            countdownController.ResetCountdown();  // �J�E���g�_�E���̃��Z�b�g
        }
        else
        {
            Debug.LogError("CountdownController is not found.");
        }

        // �Q�[����ʂ�UI�ݒ�̃��Z�b�g
        finishUI.SetActive(false);  // FinishUI���\��
        startUI.SetActive(true);    // StartUI���ĕ\��
    }

    public void PushSetectBtn()
    {
        gorstCharaController.ViewMoving();
        SceneManager.LoadScene("SelectScene");  // SelectScene�Ɉړ�

    }

}