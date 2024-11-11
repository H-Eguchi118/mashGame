using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // �f�[�^�ۑ�
    public void SaveItemData(int flowersScore, int rareFlowersScore, int bouquetScore)
    {
        PlayerPrefs.SetInt("flowersScore", flowersScore);
        PlayerPrefs.SetInt("rareFlowersScore", rareFlowersScore);
        PlayerPrefs.SetInt("bouquetScore", bouquetScore);
        PlayerPrefs.Save(); // �f�[�^��ۑ�
        Debug.Log("�A�C�e���f�[�^��ۑ����܂���");
    }

    // �f�[�^���[�h
    public void LoadItemData(out int flowersScore, out int rareFlowersScore, out int bouquetScore)
    {
        flowersScore = PlayerPrefs.GetInt("flowersScore", 0);
        rareFlowersScore = PlayerPrefs.GetInt("rareFlowersScore", 0);
        bouquetScore = PlayerPrefs.GetInt("bouquetScore", 0);

        Debug.Log("�A�C�e���f�[�^��ǂݍ��݂܂���");
    }

    public void SaveTimeData(float time)
    {
        PlayerPrefs.SetFloat("time", time);
        Debug.Log("�^�C���f�[�^��ۑ����܂���");
    }

    public void LoadTimeData(out float time)
    {
        time = PlayerPrefs.GetFloat("time", 0.0f);
        Debug.Log("�^�C���f�[�^��ǂݍ��݂��܂���");
    }

    // �X�R�A�f�[�^��ۑ����郁�\�b�h
    public void SaveTotalMoneyData(int totalMoney)
    {
        PlayerPrefs.SetInt("totalMoney", totalMoney);
        PlayerPrefs.Save();
        Debug.Log("���v���z�f�[�^��ۑ����܂���");
    }

    // �X�R�A�f�[�^��ǂݍ��ރ��\�b�h
    public int LoadTotalMoneyData()
    {
        int totalMoney = PlayerPrefs.GetInt("totalMoney", 0);
        Debug.Log("���v���z�f�[�^��ǂݍ��݂܂���");
        return totalMoney;
    }
}
