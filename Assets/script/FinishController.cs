using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class FinishController : MonoBehaviour
{
    public Canvas finishUI;               // �I����ʂ�Canvas
    public TextMeshProUGUI finishText;    // �I�����b�Z�[�W�p��Text
    public TextMeshProUGUI countText;     // �ŏI�J�E���g��\������Text
    public TextMeshProUGUI highScoreText; // �n�C�X�R�A��\������Text
    public TextMeshProUGUI newText; // �n�C�X�R�A��\������Text

    private int count;                   // �Q�[���̍ŏI�J�E���g
    private int highScore;               // �n�C�X�R�A
    private string filePath;             // JSON�t�@�C���̃p�X

    void Start()
    {
        filePath = Application.persistentDataPath + "/scoreData.json";  // �ۑ���t�@�C���p�X��ݒ�
        finishUI.gameObject.SetActive(false);  // �Q�[���J�n���͏I����ʂ��\��
        newText.gameObject.SetActive(false);   // ������Ԃł́unew Record!�v���\��
        LoadHighScore();                       // �ۑ����ꂽ�n�C�X�R�A��ǂݍ���
    }

    // �Q�[�����I�������Ƃ��ɌĂ΂�郁�\�b�h
    public void ShowFinishUI(int finalCount)
    {
        finishUI.gameObject.SetActive(true);  // �I����ʂ�\��
        count = finalCount;
        finishText.text = "FINISH!";
        countText.text = "Score: " + count.ToString();

        // �n�C�X�R�A�̍X�V����
        if (count > highScore)
        {
            highScore = count;
            newText.text = "new Record!";
            newText.gameObject.SetActive(true);  // �unew Record!�v��\��
            StartCoroutine(BlinkNewRecordText()); // �_�ŃR���[�`�����J�n

            SaveHighScore();  // �V�����n�C�X�R�A��ۑ�
        }

        highScoreText.text = "High Score: " + highScore.ToString(); // �n�C�X�R�A��\��
    }

    // �n�C�X�R�A��JSON�t�@�C���ɕۑ����鏈��
    void SaveHighScore()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.highScore = highScore;

        string json = JsonUtility.ToJson(scoreData);  // JSON�ɕϊ�
        File.WriteAllText(filePath, json);  // �t�@�C���ɏ�������
    }

    // JSON�t�@�C������n�C�X�R�A��ǂݍ��ޏ���
    void LoadHighScore()
    {
        if (File.Exists(filePath))  // �t�@�C�������݂���ꍇ
        {
            string json = File.ReadAllText(filePath);  // �t�@�C����ǂݍ���
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);  // JSON����I�u�W�F�N�g�ɕϊ�
            highScore = scoreData.highScore;
        }
        else
        {
            highScore = 0;  // �t�@�C�������݂��Ȃ��ꍇ�̓n�C�X�R�A��0�ɏ�����
        }
    }

    // �unew Record!�v��_�ł�����R���[�`��
    IEnumerator BlinkNewRecordText()
    {
        while (true)
        {
            newText.enabled = !newText.enabled;  // �\���Ɣ�\����؂�ւ���
            yield return new WaitForSeconds(0.5f);  // 0.5�b���Ƃɐ؂�ւ�
        }
    }

    // RETRY�{�^���������ꂽ���̏���
    public void OnRetryButtonPressed()
    {
        GameDirector gameDirector = FindObjectOfType<GameDirector>();
        gameDirector.PushRetryBtn(); // �Q�[���̃��X�^�[�g���Ăяo��
    }
}
