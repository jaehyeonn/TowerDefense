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

    private void Update()   // ESCŰ�� ��������
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)     //ObjectDetector Class���� ���콺 Ŭ������ Ÿ���� �������� �� ȣ�� Ÿ�� ���� UI�� Ȱ��ȭ�ϰ� �츮�� ������ Ÿ���� ������ ����ϴ� �Լ�
    {
        // ����ؾߵǴ� Ÿ�� ������ �޾ƿͼ� ����
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        // Ÿ�� ���� Panel ON
        gameObject.SetActive(true);
        // Ÿ�� ������ ����
        UpdateTowerData();
        // Ÿ�� ������Ʈ �ֺ��� ǥ�õǴ� Ÿ�� ���ݹ��� Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPanel() // ESCŰ�� �������� Update ���� ȣ�� 
    {
        // Ÿ�� ���� Panel Off
        gameObject.SetActive(false);
        // Ÿ�� ���ݹ��� Sprite Off
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
