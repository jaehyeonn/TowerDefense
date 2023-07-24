using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    
}

// ����ȭ �ϸ� Inspector View ���� Ŭ���� ������ ���� �������� ������ �� �ִ�.
[System.Serializable]             // ����ü, Ŭ������ ����ȭ �ϴ� ��� �޸𸮻� �����ϴ� ������Ʈ ������ string �Ǵ� byte ������ ���·� �����ϴ� ��
public struct Wave
{
    public float spawnTime;          // ���� ���̺� �� ���� �ֱ�
    public int maxEnemyCount;        // ���� ���̺� �� ���� ����
    public GameObject[] enemyPrefab; // ���� ���̺� �� ���� ����
}
