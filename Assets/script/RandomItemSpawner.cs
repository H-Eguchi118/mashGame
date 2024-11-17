using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField]private Camera mainCamera; // ���C���J�����i�C���X�y�N�^�Őݒ�j

    public List<GameObject> bluePrefabs; // �Ԃ�Prefab���X�g
    public List<GameObject> glayPrefabs; // �D�Ԃ�Prefab���X�g
    public List<GameObject> orangePrefabs; // ��Ԃ�Prefab���X�g
    public List<GameObject> whitePrefabs; // ���Ԃ�Prefab���X�g

    public float spawnOffset = 2.0f; // �J�����[���班���O���ɐ�������I�t�Z�b�g

    public float blueFixedY = 0.0f; // ���Ԃ�Y���W
    public float glayFixedY = 0.0f; // �D�Ԃ�Y���W
    public float orangeFixedY = 0.0f; // ��F�Ԃ�Y���W
    public float whiteFixedY = 0.0f; // �����Ԃ�Y���W

    public Vector2 spawnIntervalRange = new Vector2(0.5f, 1.5f); // �����_���Ȑ����Ԋu�͈̔́i�b�j

    void Start()
    {
        // �����_���������J�n
        StartCoroutine(SpawnItemsRandomly());
    }

    IEnumerator SpawnItemsRandomly()
    {
        while (true)
        {
            // �����_���Ȏ��Ԃ�ҋ@
            float waitTime = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(waitTime);

            // �A�C�e���𐶐�
            SpawnBlueFlower();
        }
    }

    private void SpawnBlueFlower()
    {
        // �J�����̉E�[�̍��W���v�Z���A�����ʒu������
        float spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x + spawnOffset;
        Vector3 spawnPosition = new Vector3(spawnX, blueFixedY, 0);

        // �����_���ȃA�C�e����I��
        GameObject selectedPrefab = bluePrefabs[Random.Range(0, bluePrefabs.Count)];

        // �A�C�e���𐶐�
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
