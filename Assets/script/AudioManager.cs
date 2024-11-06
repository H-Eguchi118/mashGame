using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource leftFootSound;//左足音
    [SerializeField] private AudioSource rightFootSound;//右足音
    [SerializeField] private AudioSource jumpSound;//ジャンプ音
    [SerializeField] private AudioSource enemyHitSound;//敵との衝突音

    [SerializeField] private AudioSource runningGoalSound;//ゴール音
    [SerializeField] private AudioSource buttonSound;//ボタン音

    [SerializeField] private AudioSource runningBgm;//ランニングシーンBGM
    [SerializeField] private AudioSource countBgm;//カウントシーンBGM
    [SerializeField] private AudioSource selectBgm;//カウントシーンBGM

    private void Start()
    {

    }

    public void PlayLeftFootSound()
    {
        if (leftFootSound != null)
        {
            leftFootSound.Play();
        }
    }
    public void PlayRightFootSound()
    {
        if (rightFootSound != null)
        {
            rightFootSound.Play();
        }
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            jumpSound.Play();
        }
    }

    public void PlayEnemyHitSound()
    {
        if (enemyHitSound != null)
        {
            enemyHitSound.Play();
        }
    }

    public void PlayRunningGoalSound()
    {
        if (runningGoalSound != null)
        {
            runningGoalSound.Play();
        }

    }

    public void PlayButtonSound()
    {
        if (buttonSound != null)
        {
            buttonSound.Play();
        }
    }

    public void PlayRunningBgm()
    {
        if (runningBgm != null)
        {
            runningBgm.Play();
        }
    }
    public void StopRunningBgm()
    {
        if (runningBgm != null)
        {
            runningBgm.Stop();
        }
    }

    public void PlayCountBgm()
    {
        if (countBgm != null)
        {
            countBgm.Play();
        }
    }

    public void PlaySelectBgm()
    {
        if (selectBgm != null)
        {
            selectBgm.Play();
        }
    }

}