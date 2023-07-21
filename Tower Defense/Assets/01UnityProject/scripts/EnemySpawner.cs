using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;     //적 프리팹
    [SerializeField]
    private float spawnTime;            //적 생성 주기
    [SerializeField]
    private Transform[] wayPoints;      //현재 스테이지의 이동 경로

    private void Awake()
    {
        //적 생성 코루틴 함수 호출
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        //int loopCount = 0; 무한루프 오류 고치기
        while(true)
        {
            GameObject clone = Instantiate(enemyPrefab);  //적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>();    //방금 생성된 적의 enemy 컴포넌트

            enemy.Setup(wayPoints);                       //waypoint  정보를 매개변수로 setup() 호출

            yield return new WaitForSeconds(spawnTime);   //spawnTime 시간 동안 대기
            //loopCount++;무한루프 오류 고치기
            //Debug.LogFormat("loopCount {0}", loopCount);무한루프 오류 고치기
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
