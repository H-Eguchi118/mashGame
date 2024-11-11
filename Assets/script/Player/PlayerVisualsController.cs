using System.Collections;
using UnityEngine;
using Boomerang2DFramework.Framework.AudioManagement;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerVisualsController : MonoBehaviour
{
    public Sprite standingSprite;
    public Sprite rightFootSprite;
    public Sprite leftFootSprite;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Item _item;
    [SerializeField] private RunGameDirector _runGameDirector;



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
            StartCoroutine(GoToShopScene());

            //ゴールキャンバスを表示する
            // StartCoroutine(_runGameDirector.SetGoalCanvas());

            //動作を無効化
            GetComponent<RunPlayerController>().isGoalIn = true;

            _runGameDirector.StopTimer(); // 必要に応じてタイマーを管理

            // Item スクリプトの flightRimitTime を停止させる
            _item.StopFlightTimer();
        }

        //それぞれのアイテムのトリガー
        if (other.gameObject.tag == "Flower")
        {
            Debug.Log($" {other.gameObject.name}+を取りました"); // 衝突しているオブジェクトの名前を表示

            _item.GetFlower();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "RareFlower")
        {
            Debug.Log($" {other.gameObject.name}+を取りました"); // 衝突しているオブジェクトの名前を表示

            _item.GetRareFlower();
            Destroy(other.gameObject);
        }


        if (other.gameObject.tag == "FlightItem")
        {
            Debug.Log($" {other.gameObject.name}+を取りました"); // 衝突しているオブジェクトの名前を表示

            _item.GetFightItem();
            Destroy(other.gameObject);

        }

    }
    private IEnumerator GoToShopScene()
    {
        yield return new WaitForSeconds(4.0f);//4秒待機
        SceneManager.LoadScene("ShopScene");  // ShopSceneに遷移
    }
}
