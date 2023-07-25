using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate;    // Ÿ�� ���� (���ݷ�, ���ݼӵ� ��)
    [SerializeField]
    private EnemySpawner enemySpawner;      // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����..
    [SerializeField]
    private PlayerGold playerGold;          // Ÿ�� �Ǽ� �� ��� ���Ҹ� ����..

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        //Ÿ�� �Ǽ� ���� ���� Ȯ��
        // 1. Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� X
        if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        {
            return;
        }
        //Ÿ���� �Ǽ��Ǿ� �����Ƿ� ����
        tile.IsBuildTower = true;
        // Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerGold.CurrentGold -= towerTemplate.weapon[0].cost;
        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ� (Ÿ�Ϻ��� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity);
        // Ÿ�� ���⿡ enemySpawner ���� ����
        TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
        if (towerWeapon != null)
        {
            towerWeapon.Setup(enemySpawner, playerGold);
        }
    }
    
}
