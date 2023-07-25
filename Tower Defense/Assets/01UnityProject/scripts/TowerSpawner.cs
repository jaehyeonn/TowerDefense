using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] towerTemplate;         // Ÿ�� ���� (���ݷ�, ���ݼӵ� ��)
    [SerializeField]
    private EnemySpawner enemySpawner;           // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����..
    [SerializeField]
    private PlayerGold playerGold;               // Ÿ�� �Ǽ� �� ��� ���Ҹ� ����..
    [SerializeField]
    private SystemTextViewer systemTextViewer;   // �� ����, �Ǽ� �Ұ��� ���� �ý��� �޽��� ���
    private bool isOnTowerButton = false;        // Ÿ�� �Ǽ� ��ư�� �������� üũ
    private GameObject followTowerClone = null;  // �ӽ� Ÿ�� ��� �Ϸ� �� ������ ���� �����ϴ� ����
    private int towerType;                       // Ÿ�� �Ӽ�

    public void ReadyToSpawnTower(int Type)
    {
        towerType = Type;

        // ��ư�� �ߺ��ؼ� ������ ���� �����ϱ� ���� �ʿ�
        if (isOnTowerButton == true)
        {
            return;
        }

        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� x
        if (towerTemplate[towerType].weapon[0].cost > playerGold.CurrentGold)
        {
            // ��尡 �����ؼ� Ÿ�� �Ǽ��� �Ұ����ϴٰ� ���
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }

        // Ÿ�� �Ǽ� ��ư�� �����ٰ� ����
        isOnTowerButton = true;
        // ���콺�� ���󳪴ϴ� �ӽ� Ÿ�� ����
        followTowerClone = Instantiate(towerTemplate[towerType].followTowerPrefab);
        // Ÿ�� �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnTowerCancelSystem");
    }
    public void SpawnTower(Transform tileTransform)
    {
        // Ÿ�� �Ǽ� ��ư�� ������ ���� Ÿ�� �Ǽ� ����
        if (isOnTowerButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        // 2.���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ�x
        if (tile.IsBuildTower == true)
        {
            // ���� ��ġ�� Ÿ�� �Ǽ��� �Ұ����ϴٰ� ���
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        // �ٽ� Ÿ�� �Ǽ� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ���� ����
        isOnTowerButton = false;
        // Ÿ���� �Ǽ��Ǿ� �����Ƿ� ����
        tile.IsBuildTower = true;
        // Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost;
        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ� (Ÿ�Ϻ��� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, position, Quaternion.identity);
        // Ÿ�� ���⿡ enemySpawner ���� ����
        TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
        if (towerWeapon != null)
        {
            towerWeapon.Setup(enemySpawner, playerGold, tile);
        }

        // Ÿ���� ��ġ�߱� ������ ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        Destroy(followTowerClone);
        // Ÿ�� �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            // ESCŰ �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �Ǽ� ���
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
