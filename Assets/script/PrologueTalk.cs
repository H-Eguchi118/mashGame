using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrologueTalk : MonoBehaviour
{
    [System.Serializable]
    public class Conversation
    {
        public bool isPlayer;  // trueならプレイヤーの発言、falseなら相手の発言
        public string text;    // 各発言内容
    }

    [SerializeField] private Image playerFrame;
    [SerializeField] private Image otherFrame;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI otherText;
    [SerializeField] private Button nextButton;

    // 会話のリスト
    [SerializeField] private List<Conversation> conversations;
    private int currentIndex = 0;  // 現在の会話インデックス

    void Start()
    {
        UpdateTalk();
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    // 会話の内容を更新
    void UpdateTalk()
    {
        // 会話が終わったら次の処理へ（例: シーン移動）
        if (currentIndex >= conversations.Count)
        {
            Debug.Log("会話が終了しました");
            return;
        }

        // 現在の会話データを取得
        Conversation currentConversation = conversations[currentIndex];

        // プレイヤーか相手かで吹き出しを切り替え
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

    // 次の会話へ進むボタン処理
    void OnNextButtonClick()
    {
        currentIndex++;
        UpdateTalk();
    }
}
