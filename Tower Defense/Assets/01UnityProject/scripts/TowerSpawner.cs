using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private int towerBuildGold = 50;    // Ÿ�� �Ǽ��� ���Ǵ� ���
    [SerializeField]
    private EnemySpawner enemySpawner;  // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����..
    [SerializeField]
    private PlayerGold playerGold;      // Ÿ�� �Ǽ� �� ��� ���Ҹ� ����..

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        //Ÿ�� �Ǽ� ���� ���� Ȯ��
        // 1. Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� X
        if(towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }
        //Ÿ���� �Ǽ��Ǿ� �����Ƿ� ����
        tile.IsBuildTower = true;
        // Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerGold.CurrentGold -= towerBuildGold;
        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ� (Ÿ�Ϻ��� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerPrefab, position, Quaternion.identity);
        // Ÿ�� ���⿡ enemySpawner ���� ����
        TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
        if (towerWeapon != null)
        {
            towerWeapon.Setup(enemySpawner);
        }
    }
    
}
