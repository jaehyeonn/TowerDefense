using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TowerAttackRange towerAttackRange;

    private TowerWeapon currentTower;


    private void Awake()
    {
        OffPanel();
    }

    private void Update()   // ESC키를 눌렀을때
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)     //ObjectDetector Class에서 마우스 클릭으로 타워를 선택했을 때 호출 타워 정보 UI를 활성화하고 우리가 선택한 타워의 정보를 출력하는 함수
    {
        // 출력해야되는 타워 정보를 받아와서 저장
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        // 타워 정보 Panel ON
        gameObject.SetActive(true);
        // 타워 정보를 갱신
        UpdateTowerData();
        // 타워 오브젝트 주변에 표시되는 타워 공격범위 Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPanel() // ESC키를 눌렀을때 Update 에서 호출 
    {
        // 타워 정보 Panel Off
        gameObject.SetActive(false);
        // 타워 공격범위 Sprite Off
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        textDamage.text = "Damage : " + currentTower.Damage;
        textRate.text = "Rate : " + currentTower.Rate;
        textRange.text = "Range : " + currentTower.Range;
        textLevel.text = "Level : " + currentTower.Level;
    }    
}
