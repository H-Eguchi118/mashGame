using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorstCharaController : MonoBehaviour
{
    public Image characterImage; // キャラクターのスプライトを表示するUI Image
    public Sprite standingSprite; // 立ち絵のスプライト
    public Sprite rightFootSprite; // 右足上げのスプライト
    public Sprite leftFootSprite; // 左足上げのスプライト

    private enum CharacterState { Standing, RightFoot, LeftFoot }
    private CharacterState currentState = CharacterState.Standing; // 現在のスプライトの状態

    void Start()
    {
        // 初期スプライトを設定
        characterImage.sprite = standingSprite;

        // characterImageのRectTransformを取得
        RectTransform rectTransform = characterImage.GetComponent<RectTransform>();

        // サイズを設定 (width, height)
        rectTransform.sizeDelta = new Vector2(500, 500); // 画像のサイズを500x500に設定

        // ポジションを設定
        rectTransform.anchoredPosition = new Vector2(0, 0); // 座標 (0, 0) に設定
    }

    public void ViewGorstR()
    {
        // 右足上げのスプライトに切り替え
        if (currentState != CharacterState.RightFoot)
        {
            characterImage.sprite = rightFootSprite; // 右足上げ
            currentState = CharacterState.RightFoot; // 状態を更新
        }
    }

    public void ViewGorstL()
    {
        // 左足上げのスプライトに切り替え
        if (currentState != CharacterState.LeftFoot)
        {
            characterImage.sprite = leftFootSprite; // 左足上げ
            currentState = CharacterState.LeftFoot; // 状態を更新
        }
    }

    // 立ち絵に戻すメソッドを追加
    public void ViewStanding()
    {
        characterImage.sprite = standingSprite; // 立ち絵に戻す
        currentState = CharacterState.Standing; // 状態を更新
    }
}
