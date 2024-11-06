using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float moveSpeed;//エネミーのスピード
    public Transform leftPoint;//左端の位置
    public Transform rightPoint;//右端の位置

    private bool movingRight = true;//エネミーの進行方向


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, rightPoint.position) < 0.1f)
            {
                movingRight = false;
                Flip();

            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, moveSpeed * Time.deltaTime);

            //左端に到達たら右に向かせる
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    //えねみーの向きを反転させる
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;

    }
}
