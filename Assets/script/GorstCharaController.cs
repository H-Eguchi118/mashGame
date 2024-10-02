using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorstCharaController : MonoBehaviour
{
    public Image characterImage; // �L�����N�^�[�̃X�v���C�g��\������UI Image
    public Sprite standingSprite; // �����G�̃X�v���C�g
    public Sprite rightFootSprite; // �E���グ�̃X�v���C�g
    public Sprite leftFootSprite; // �����グ�̃X�v���C�g

    private enum CharacterState { Standing, RightFoot, LeftFoot }
    private CharacterState currentState = CharacterState.Standing; // ���݂̃X�v���C�g�̏��

    void Start()
    {
        // �����X�v���C�g��ݒ�
        characterImage.sprite = standingSprite;

        // characterImage��RectTransform���擾
        RectTransform rectTransform = characterImage.GetComponent<RectTransform>();

        // �T�C�Y��ݒ� (width, height)
        rectTransform.sizeDelta = new Vector2(500, 500); // �摜�̃T�C�Y��500x500�ɐݒ�

        // �|�W�V������ݒ�
        rectTransform.anchoredPosition = new Vector2(0, 0); // ���W (0, 0) �ɐݒ�
    }

    public void ViewGorstR()
    {
        // �E���グ�̃X�v���C�g�ɐ؂�ւ�
        if (currentState != CharacterState.RightFoot)
        {
            characterImage.sprite = rightFootSprite; // �E���グ
            currentState = CharacterState.RightFoot; // ��Ԃ��X�V
        }
    }

    public void ViewGorstL()
    {
        // �����グ�̃X�v���C�g�ɐ؂�ւ�
        if (currentState != CharacterState.LeftFoot)
        {
            characterImage.sprite = leftFootSprite; // �����グ
            currentState = CharacterState.LeftFoot; // ��Ԃ��X�V
        }
    }

    // �����G�ɖ߂����\�b�h��ǉ�
    public void ViewStanding()
    {
        characterImage.sprite = standingSprite; // �����G�ɖ߂�
        currentState = CharacterState.Standing; // ��Ԃ��X�V
    }
}
