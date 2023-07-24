using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    
}

// 직렬화 하면 Inspector View 에서 클래스 내부의 변수 정보들을 수정할 수 있다.
[System.Serializable]             // 구조체, 클래스를 직렬화 하는 명령 메모리상에 존재하는 오브젝트 정보를 string 또는 byte 데이터 형태로 변형하는 것
public struct Wave
{
    public float spawnTime;          // 현재 웨이브 적 생성 주기
    public int maxEnemyCount;        // 현재 웨이브 적 등장 숫자
    public GameObject[] enemyPrefab; // 현재 웨이브 적 등장 종류
}
