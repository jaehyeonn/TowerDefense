using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHPSliderPrefab;          // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform;               // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    [SerializeField]
    private Transform[] wayPoints;                   // ���� ���������� �̵� ���
    [SerializeField]
    private PlayerHP playerHP;                       // �÷��̾� ü�� ������Ʈ
    [SerializeField]
    private PlayerGold playerGold;                   // �÷��̾� ��� ������Ʈ
    private Wave currentWave;                        // ���� ���̺� ����
    private int currentEnemyCount;                   // ���� ���̺꿡 �����ִ� �� ���� (���̺� ���۽� max�� ����, �� ��� �� -1)
    private List<Enemy> enemyList;                   // ���� �ʿ� �����ϴ� ��� ���� ����

    // ���� ������ ������ Enemyspawner ���� �ϱ� ������ set�� �ʿ� ����.
    public List<Enemy> EnemyList => enemyList;
    // ���� ���̺꿡 �����ִ���, �ִ� �� ����
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
    }

    public void StartWave(Wave wave)
    {
        // �Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        // ���� ���̺��� �ִ� �� ���ڸ� ����
        currentEnemyCount = currentWave.maxEnemyCount;
        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        ///���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        //while(true)
        // ���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab);  // �� ������Ʈ ����
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ����
            int enemyIndex = Random.Range(0, currentWave.enemyPrefab.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefab[enemyIndex]);
;            Enemy enemy = clone.GetComponent<Enemy>();    // ��� ������ ���� enemy ������Ʈ

            // this�� �� �ڽ� (�ڽ��� EnemySpawner ����)
            enemy.Setup(this, wayPoints);                 // waypoint  ������ �Ű������� setup() ȣ��
            enemyList.Add(enemy);                         // ����Ʈ�� ��� ������ �� ���� ����

            SpawnEnemyHPSlider(clone);                    // �� ü���� ��Ÿ���� Slider UI ���� �� ����

            // ���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime);   // spawnTime �ð� ���� ���
            // �� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave.spawnTime);   // spawnTime ���� ���
        }
    }
    
    public void DestroyEnemy(EnemyDestroyType type,Enemy enemy, int gold)
    {
        // ���� ��ǥ�������� �������� ��
        if(type == EnemyDestroyType.Arrive)
        {
            // �÷��̾� ü�� -1
            playerHP.TakeDamage(1);
        }
        // ���� �÷��̾��� �߻�ü���� ������� ��
        else if(type == EnemyDestroyType.Kill)
        {
            // �� ������ ���� ��� �� ��� ŉ��
            playerGold.CurrentGold += gold;
        }

        // ���� ����� ������ ���� ���̺��� ���� �� ���� ���� (UI ǥ�ÿ�)
        currentEnemyCount--;
        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //�� ������Ʈ ����
        Destroy(enemy.gameObject);
    }
    
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        // Tip. UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        // Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
