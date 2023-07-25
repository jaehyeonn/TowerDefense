using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate;                        // 타워 정보 (공격력, 공격속도 등 )
    [SerializeField]
    private GameObject projectilePrefab;                        // 발사체 프리팹
    [SerializeField]
    private Transform spawnPoint;                                // 발사체 생성 위치
   
    private int level = 0;                                       // 타워 레벨
    private WeaponState weaponState = WeaponState.SearchTarget;  // 타워 무기의 상태
    private Transform attackTarget = null;                       // 공격 대상
    private SpriteRenderer spriteRenderer;                       // 타워 오브젝트 이미지 변경용
    private EnemySpawner enemySpawner;                           // 게임에 존재하는 적 정보 흭득용
    private PlayerGold playerGold;                               // 플레이어의 골드 정보 흭득 및 설정
    private Tile ownerTile;                                       // 현재 타워가 배치되어 있는 타일

    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float Damage => towerTemplate.weapon[level].damage;
    public float Rate => towerTemplate.weapon[level].rate;
    public float Range => towerTemplate.weapon[level].range;
    public int Level => level + 1;
    public int MaxLevel => towerTemplate.weapon.Length;

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;

        //최초 상태를 WeaponState.SearchTarget 으로 설정
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        // 이전에 재생중이던 상태 종료
        StopCoroutine(weaponState.ToString());
        // 상태 변경
        weaponState = newState;
        //새로운 상태 저장
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
    //    if (attackTarget != null)
    //    {
    //        RotateToTarget();
    //    }
    }

    //private void RotateToTarget()
    //{
    //    // 원점으로부터의 거리와 수평축으로부터터의 각도를 이용해 위치를 구하는 극 좌표계 이용
    //    // 각도 = arctan(y/x)
    //    // x, y 범위값 구하기
    //    float dx = attackTarget.position.x - transform.position.x;
    //    float dy = attackTarget.position.y - transform.position.y;
    //    // x, y 변위값을 바탕으로 각도 구하기
    //    // 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
    //    float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, degree);
    //}

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
            float closestDistSqr = Mathf.Infinity;
            // EnemySpawner의 EnemyList 애 있는 현재 맵에 존재하는 모든 적 검사
            for (int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // 현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                if (distance <= towerTemplate.weapon[level].range && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target이 있는지 검사 (다른 발사체에 의해 제거, Goal 지점까지 이동해 삭제 등)
            if (attackTarget == null)                   //attackTarget이 null이면 SerachTarget으로 변경
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 2. target이 공격 범위 안에 있는지 검사 (공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > towerTemplate.weapon[level].range)                  // 적이 타워의 공격 범위를 벗어나도 SearchTarget으로 변경
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 3. attackRate(공격주기) 시간만큼 대기
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

            // 4. 공격 (발사체 생성)
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        // 생성된 발사체에게 공격대상 (attackTarget) 정보 제공
        clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage);
    }

    public bool Upgrade()
    {
        // 타워 업그레이드에 필요한 골드가 충분한지 검사  골드가 부족하면 flase를 반환해 타워 업그레이드 실패 알림
        if(playerGold.CurrentGold < towerTemplate.weapon[level+1].cost)
        {
            return false;
        }
        // 업글이 가능하면...
        // 타워 레벨 증가
        level++;
        // 타워 외형 변경 (Sprite)
        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;
        // 골드 차감
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;

        return true;
    }

    public void Sell()
    {
        // 골드 증가
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        // 현재 타일에 다시 타워 건설이 가능하도록 설정
        ownerTile.IsBuildTower = false;
        // 타워 파괴
        Destroy(gameObject);
    }
}
