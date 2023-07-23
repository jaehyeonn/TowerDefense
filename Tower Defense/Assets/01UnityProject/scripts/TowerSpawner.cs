using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private EnemySpawner enemySpawner;  // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����..

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        //Ÿ�� �Ǽ� ���� ���� Ȯ��
        // 1. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� X 
        if(tile.IsBuildTower == true)
        {
            return;
        }
        //Ÿ���� �Ǽ��Ǿ� �����Ƿ� ����
        tile.IsBuildTower = true;
        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�
        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
        // Ÿ�� ���⿡ enemySpawner ���� ����
        //clone.GetComponent<TowerSpawner>().Setup(enemySpawner);
        TowerWeapon towerWeapon = clone.GetComponent<TowerWeapon>();
        if (towerWeapon != null)
        {
            towerWeapon.Setup(enemySpawner);
        }
    }
    
}
