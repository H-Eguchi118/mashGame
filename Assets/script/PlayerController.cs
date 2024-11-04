using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    [Header("Movement Settings")]
    public float baseRunForce;        // �ŏ��̑���́i�C���X�y�N�^�[�Őݒ�j
    public float maxRunForce;        // �ő�̑���́i�C���X�y�N�^�[�Őݒ�j
    public float decayRate;           // �������i�C���X�y�N�^�[�Őݒ�j
    public float accelerationFactor;  // �����̉e���𒲐�����W���i�������قǂ����������j

    private float currentRunForce;      // ���݂̉�����
    private string lastButtonPressed = ""; // �O�񉟂��ꂽ�{�^�����L�^
    private float lastInputTime = 0f;      // �Ō�Ƀ{�^���������ꂽ����
    private float lastInterval = 0f;       // �A�ł̑������L�^

    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        currentRunForce = baseRunForce;  // �����l����{�̑���͂ɐݒ�
    }

    void Update()
    {
        GorstRunning();
        DecelerateOverTime();
    }

    // ���݂ɘA�ł��ĉE�����ɉ������Ȃ��瑖�铮��
    public void GorstRunning()
    {
        // �����܂��͉E��󂪉�����A�O��̓��͂ƈقȂ�{�^���������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastButtonPressed != "L")
        {
            lastButtonPressed = "L";
            RunRight();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastButtonPressed != "R")
        {
            lastButtonPressed = "R";
            RunRight();
        }
    }

    // �E�����ɑ��邽�߂̗͂������郁�\�b�h
    void RunRight()
    {
        // �A�ő��x�̌v�Z
        float currentTime = Time.time;
        float interval = currentTime - lastInputTime;

        // �A�ł������ق� currentRunForce ���������傫������
        if (interval > 0)
        {
            // accelerationFactor ���g���ĉ����̉e����}����
            float scaledInterval = interval * accelerationFactor;
            currentRunForce = Mathf.Lerp(currentRunForce, maxRunForce, scaledInterval);
        }

        // �͂�������
        rigid2D.AddForce(new Vector2(currentRunForce, 0), ForceMode2D.Impulse);

        // �Ō�ɓ��͂����������Ԃ��X�V
        lastInputTime = currentTime;
    }

    // ��莞�ԘA�ł��Ȃ��Ƃ��ɏ��X�Ɍ�������
    void DecelerateOverTime()
    {
        if (Time.time - lastInputTime > 0.5f)  // 0.5�b�ԓ��͂��Ȃ��ꍇ�����J�n
        {
            currentRunForce = Mathf.Max(currentRunForce - decayRate * Time.deltaTime, baseRunForce);  // �ŏ��l�𒴂��Ȃ��悤��
        }
    }
}
