using System.Collections;
using UnityEngine;
using Boomerang2DFramework.Framework.AudioManagement;
using UnityEngine.Tilemaps;

public class PlayerVisualsController : MonoBehaviour
{
    public Sprite standingSprite;
    public Sprite rightFootSprite;
    public Sprite leftFootSprite;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Item _item;


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
            Debug.Log("ゴールしました");
            PlayGoalSound();
            // _runGameDirector.StopTimer(); // 必要に応じてタイマーを管理

            //音楽が鳴り終わったらシーン切り替え
        }

        //それぞれのアイテムのトリガー
        // if (other.gameObject.CompareTag("Flower"))
        if (other.gameObject.tag == "Flower")
        {
            Debug.Log($"衝突: {other.gameObject.name}"); // 衝突しているオブジェクトの名前を表示

            _item.GetFlower();
            Destroy(other.gameObject);
            Debug.Log("花を取りました");
        }

        //if (other.gameObject.tag == "FlightItem")
        //{
        //    _item.GetFightItem();
        //}



    }





}
