using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueTalk : MonoBehaviour
{
    [System.Serializable]
    public class Conversation
    {
        public bool isPlayer;  // true�Ȃ�v���C���[�̔����Afalse�Ȃ瑊��̔���
        [TextArea] public string text;    // �e�������e(TextArea�����邱�ƂŒʏ�̉��s�ł����͂ł���悤��)
    }

    [SerializeField] private Image playerFrame;
    [SerializeField] private Image otherFrame;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI otherText;
    [SerializeField] private Button nextButton;

    // ��b�̃��X�g
    [SerializeField] private List<Conversation> conversations;
    private int currentIndex = 0;  // ���݂̉�b�C���f�b�N�X

    void Start()
    {
        UpdateTalk();
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    // ��b�̓��e���X�V
    void UpdateTalk()
    {
        // ��b���I������玟�̏����ցi��: �V�[���ړ��j
        if (currentIndex >= conversations.Count)
        {
            Debug.Log("��b���I�����܂���");
            nextButton.gameObject.SetActive(false);  // �{�^�����\��
            StartCoroutine(GoToRunningScene());      // �V�[���J�ڂ̃R���[�`�����J�n
            return;
        }

        // ���݂̉�b�f�[�^���擾
        Conversation currentConversation = conversations[currentIndex];

        // �v���C���[�����肩�Ő����o����؂�ւ�
        if (currentConversation.isPlayer)
        {
            playerFrame.gameObject.SetActive(true);
            otherFrame.gameObject.SetActive(false);
            playerText.text = currentConversation.text;
        }
        else
        {
            playerFrame.gameObject.SetActive(false);
            otherFrame.gameObject.SetActive(true);
            otherText.text = currentConversation.text;
        }
    }

    // ���̉�b�֐i�ރ{�^������
    void OnNextButtonClick()
    {
        currentIndex++;
        UpdateTalk();
    }

    // �V�[���J�ڗp�̃R���[�`��
    private IEnumerator GoToRunningScene()
    {
        yield return new WaitForSeconds(3);  // 3�b�ҋ@
        SceneManager.LoadScene("RunningScene");  // "RunningScene"�ɑJ��
    }
}
