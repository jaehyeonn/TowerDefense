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
    private List<Enemy> enemyList;      //���� �ʿ� �����ϴ� ��� ���� ����

    // ���� ������ ������ Enemyspawner ���� �ϱ� ������ set�� �ʿ� ����.
    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
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

            enemy.Setup(this, wayPoints);                       //waypoint  ������ �Ű������� setup() ȣ��
            enemyList.Add(enemy);                         //����Ʈ�� ��� ������ �� ���� ����

            yield return new WaitForSeconds(spawnTime);   //spawnTime �ð� ���� ���
            //loopCount++;���ѷ��� ���� ��ġ��
            //Debug.LogFormat("loopCount {0}", loopCount);���ѷ��� ���� ��ġ��
        }
    }
    
    public void DestroyEnemy(Enemy enemy)
    {
        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //�� ������Ʈ ����
        Destroy(enemy.gameObject);
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
