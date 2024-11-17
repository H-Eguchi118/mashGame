using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorstCharaController : MonoBehaviour
{
    [SerializeField] private MashGameDirector _mashGameDirector;

    public Image characterImage; // キャラクターのスプライトを表示するUI Image
    public Sprite standingSprite; // 立ち絵のスプライト
    public Sprite rightFootSprite; // 右足上げのスプライト
    public Sprite leftFootSprite; // 左足上げのスプライト

    public Image pankiziImage; //パン生地を表示するUI　Image
    public Sprite pankiziSprite; // パン生地のスプライト
    public Sprite pankiziLeftSprite; // 左踏み時スプライト
    public Sprite pankiziRightSprite; // 右踏み時のスプライト

    public Image lastImage;
    public Sprite lastSprite1;
    public Sprite lastSprite2;

    private enum CharacterState { Standing, RightFoot, LeftFoot }
    private CharacterState currentState = CharacterState.Standing; // 現在のスプライトの状態
    private enum PankiziState { Normal, RightPan, LeftPan }
    private PankiziState currentPanState = PankiziState.Normal; // 現在のスプライトの状態

    private enum LastState { Last1, Last2 }
    private LastState currentLastState = LastState.Last1;

    void Start()
    {
        // 初期スプライトを設定
        currentState = CharacterState.Standing;
        currentPanState = PankiziState.Normal;
        lastImage.gameObject.SetActive(false);

        // characterImageのRectTransformを取得
        RectTransform rectTransform = characterImage.GetComponent<RectTransform>();

    }

    public void ViewGorstR()
    {
        // 右足上げのスプライトに切り替え
        if (currentState != CharacterState.RightFoot)
        {
            characterImage.sprite = rightFootSprite; // 右足上げ
            currentState = CharacterState.RightFoot; // 状態を更新

            pankiziImage.sprite = pankiziRightSprite;
            currentPanState = PankiziState.RightPan;

            if (_mashGameDirector.time < 10)
            {
                lastImage.gameObject.SetActive(true);

                lastImage.sprite = lastSprite1;
                currentLastState = LastState.Last1;
            }
        }
    }

    public void ViewGorstL()
    {
        // 左足上げのスプライトに切り替え
        if (currentState != CharacterState.LeftFoot)
        {
            characterImage.sprite = leftFootSprite; // 左足上げ
            currentState = CharacterState.LeftFoot; // 状態を更新

            pankiziImage.sprite = pankiziLeftSprite;
            currentPanState = PankiziState.LeftPan;

            if (_mashGameDirector.time < 10)
            {
                lastImage.gameObject.SetActive(true);

                lastImage.sprite = lastSprite2;
                currentLastState = LastState.Last2;
            }

        }
    }

    // 立ち絵に戻すメソッドを追加
    public void ViewStanding()
    {
        characterImage.sprite = standingSprite; // 立ち絵に戻す
        currentState = CharacterState.Standing; // 状態を更新
    }

}
