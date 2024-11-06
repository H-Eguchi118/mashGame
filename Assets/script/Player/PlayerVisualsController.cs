using System.Collections;
using UnityEngine;
using Boomerang2DFramework.Framework.AudioManagement;

public class PlayerVisualsController : MonoBehaviour
{
    public Sprite standingSprite;
    public Sprite rightFootSprite;
    public Sprite leftFootSprite;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioManager _audioManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standingSprite;
    }

    public void UpdateFootSprite(string foot)
    {
        if (foot == "L")
        {
            spriteRenderer.sprite = leftFootSprite;
            _audioManager.PlayLeftFootSound();
        }
        else if (foot == "R")
        {
            spriteRenderer.sprite = rightFootSprite;
            _audioManager.PlayRightFootSound();
        }
    }

    public void PlayJumpSound()
    {
        _audioManager.PlayJumpSound();
    }

    public void PlayEnemyHitSound()
    {
        _audioManager.PlayEnemyHitSound();
    }

    public void PlayGoalSound()
    {
        _audioManager.PlayRunningGoalSound();
    }

    public void PlayDamageSound()
    {
        _audioManager.PlayEnemyHitSound();

    }

    //エネミーとぶつかったときの点滅処理
    public IEnumerator BlinkSprite()
    {
        float blinkDuration = 2.0f;
        float blinkInterval = 0.2f;
        float blinkTime = 0f;

        while (blinkTime < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            blinkTime += blinkInterval;
        }
        spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Goal")
        {
            PlayGoalSound();
            // _runGameDirector.StopTimer(); // 必要に応じてタイマーを管理
        }
    }
}
