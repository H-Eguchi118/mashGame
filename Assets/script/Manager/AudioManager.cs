using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public StompGame stompGame;
    public RunGame runGame;
    public OtherAudio otherAudio;


    private void Start()
    {

    }

    public void PlayLeftFootSound()
    {
        if (runGame.leftFootSound != null)
        {
            runGame.leftFootSound.Play();
        }
    }
    public void PlayRightFootSound()
    {
        if (runGame.rightFootSound != null)
        {
            runGame.rightFootSound.Play();
        }
    }

    public void PlayJumpSound()
    {
        if (runGame.jumpSound != null)
        {
            runGame.jumpSound.Play();
        }
    }

    public void PlayEnemyHitSound()
    {
        if (runGame.enemyHitSound != null)
        {
            runGame.enemyHitSound.Play();
        }
    }

    public void PlayRunningGoalSound()
    {
        if (runGame.runningGoalSound != null)
        {
            runGame.runningGoalSound.Play();
        }

    }

    public void PlayDecisionButtonSound()
    {
        if (otherAudio.decisionBtn != null)
        {
            otherAudio.decisionBtn.Play();
        }
    }
    public void PlayCancelButtonSound()
    {
        if (otherAudio.cancelBtn != null)
        {
            otherAudio.cancelBtn.Play();
        }
    }

    public void PlayBuyButtonSound()
    {
        if (otherAudio.buyBtn != null)
        {
            otherAudio.buyBtn.Play();
        }
    }


    public void PlayRunGameBgm()
    {
        if (runGame.runGameBGM != null)
        {
            runGame.runGameBGM.Play();
        }
    }
    public void StopRunGameBgm()
    {
        if (runGame.runGameBGM != null)
        {
            runGame.runGameBGM.Stop();
        }
    }

    public void PlayStompGameBgm()
    {
        if (stompGame.stompGameBGM != null)
        {
            stompGame.stompGameBGM.Play();
        }
    }
    public void StopStompGameBgm()
    {
        if (stompGame.stompGameBGM != null)
        {
            stompGame.stompGameBGM.Stop();
        }
    }


    public void PlayShopBgm()
    {
        if (otherAudio.shopBGM != null)
        {
            otherAudio.shopBGM.Play();
        }
    }

    public void StopShopBgm()
    {
        if (otherAudio.shopBGM != null)
        {
            otherAudio.shopBGM.Stop();
        }
    }

    public void StopStartBgm()
    {
        if (otherAudio.startBGM != null)
        {
            otherAudio.startBGM.Stop();
        }
    }




    public void PlayCountFinishSound()
    {
        if (stompGame.stompFinishSound != null)
        {
            stompGame.  stompFinishSound.Play();
        }
    }

    public void StopCountFinishSound()
    {
        if (stompGame.stompFinishSound != null)
        {
            stompGame.stompFinishSound.Stop();
        }
    }

    public void PlayStompSE()
    {
        stompGame.stompSE.Play();
    }


}
[System.Serializable]
public class OtherAudio
{
    public AudioSource startBGM;//スタートシーンBGM
    public AudioSource shopBGM;//ショップBGM

    public AudioSource decisionBtn;
    public AudioSource cancelBtn;
    public AudioSource buyBtn;

}

[System.Serializable]
public class RunGame
{

    public AudioSource runGameBGM;//ランゲームBGM
    public AudioSource leftFootSound;//左足音
    public AudioSource rightFootSound;//右足音
    public AudioSource jumpSound;//ジャンプ音
    public AudioSource enemyHitSound;//敵との衝突音
    public AudioSource runningGoalSound;//ゴール音


}

[System.Serializable]
public class StompGame
{
    public AudioSource stompGameBGM;//カウントゲームBGM
    public AudioSource stompFinishSound;//カウントフィニッシュUI音
    public AudioSource stompSE;//踏む音

}