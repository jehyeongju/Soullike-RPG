using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject bossHpBar;
    public int MaxEnemy = 20;
    public int CountEnemy = 0;
    public bool isSpawn;
    bool orcTwKilled = false;
    bool updateCheck = true;
    public bool isBoss = false;
    public static EnemySpawn inst;
    public Transform parent;
    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        StartCoroutine(RandomRespawn_Coroutine());
        inst = this;
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    public GameObject enemy;

    public GameObject Boss;
    private void Start()
    {
        
        
    }
    private void Update()
    {
       BossSpawn();
        
        if (OrcScore.orcKilled == 20 && updateCheck)
        {
            orcTwKilled = true;
            updateCheck = false;
            Debug.Log("Boss1");
            
        }
        

    }

    IEnumerator RandomRespawn_Coroutine()
    {
        for (int i = 0; i < MaxEnemy; i++)
        {
            if(CountEnemy == MaxEnemy)
            {

            }
           
            else if(isBoss == false)
            {
                yield return new WaitForSeconds(5f);

                // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
                GameObject instantCapsul = Instantiate(enemy, Return_RandomPosition(), Quaternion.identity, parent);
            }
            
        }


    }
    
    void BossSpawn()
    {
        if(orcTwKilled)
        {
            Debug.Log("Boss2");
            GameObject instantBoss = Instantiate(Boss, Return_RandomPosition(),Quaternion.identity, parent);
            orcTwKilled = false;
            isBoss = true;
            bossHpBar.SetActive(true);
        }

    }
    public void PlusEnemy()
    {
        CountEnemy++;
    }
    public void MinusEnemy()
    {
        CountEnemy--;
    }
}

