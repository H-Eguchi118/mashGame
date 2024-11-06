using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float moveSpeed;//�G�l�~�[�̃X�s�[�h
    public Transform leftPoint;//���[�̈ʒu
    public Transform rightPoint;//�E�[�̈ʒu

    private bool movingRight = true;//�G�l�~�[�̐i�s����


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

            //���[�ɓ��B����E�Ɍ�������
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    //���˂݁[�̌����𔽓]������
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;

    }
}
