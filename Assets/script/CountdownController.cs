using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdown = 3;  // Unity���璲���ł���悤��public��
    public TextMeshProUGUI countdownText;
    public Canvas startUI;          // �J�E���g�_�E���p��UI

    private GameDirector gameDirector;  // �Q�[���̊J�n���Ǘ�����X�N���v�g�̎Q��

    void Start()
    {
        // GameDirector�X�N���v�g���擾���Ċ֘A�t��
        gameDirector = FindObjectOfType<GameDirector>();

        startUI.gameObject.SetActive(true);
        countdownText.text = "Tap Screen";

    }

    // ��ʂ��^�b�v������J�E���g�_�E�����J�n
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // �}�E�X�N���b�N�܂��̓^�b�v
        {
            StartCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown()
    {

        // �J�E���g�_�E���J�n
        while (countdown > 0)
        {
            // �����̃t�H���g�T�C�Y��700�ɐݒ�
            countdownText.fontSize = 700;
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1.0f);  // 1�b�ҋ@
            countdown--;
        }

        countdownText.fontSize = 230;
        countdownText.text = "Start!";
        yield return new WaitForSeconds(1.0f);

        // �J�E���g�_�E�����I��������Q�[���J�n
        gameDirector.StartGame();

        // �J�E���g�_�E��UI���\��
        startUI.gameObject.SetActive(false);
    }
}
