using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;     //�� ������
    [SerializeField]
    private float spawnTime;            //�� ���� �ֱ�
    [SerializeField]
    private Transform[] wayPoints;      //���� ���������� �̵� ���

    private void Awake()
    {
        //�� ���� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        //int loopCount = 0; ���ѷ��� ���� ��ġ��
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab);  //�� ������Ʈ ����
            Enemy enemy = clone.GetComponent<Enemy>();    //��� ������ ���� enemy ������Ʈ

            enemy.Setup(wayPoints);                       //waypoint  ������ �Ű������� setup() ȣ��

            yield return new WaitForSeconds(spawnTime);   //spawnTime �ð� ���� ���
            //loopCount++;���ѷ��� ���� ��ġ��
            //Debug.LogFormat("loopCount {0}", loopCount);���ѷ��� ���� ��ġ��
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
