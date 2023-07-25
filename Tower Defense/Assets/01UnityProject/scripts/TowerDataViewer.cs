using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

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
    [SerializeField]
    private Button buttonUpgrade;  // Ÿ�� ������ �ִ밡 �Ǹ� ��ư ��Ȱ��ȭ

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
        imageTower.sprite = currentTower.TowerSprite;
        textDamage.text = "Damage : " + currentTower.Damage;
        textRate.text = "Rate : " + currentTower.Rate;
        textRange.text = "Range : " + currentTower.Range;
        textLevel.text = "Level : " + currentTower.Level;

        // ���׷��̵尡 �Ұ��������� ��ư ��Ȱ��ȭ
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
    }   
    
    public void OnClickEventTowerUpgrade()
    {
        // Ÿ�� �������̵� �õ� (����:true, ����:false)
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            // Ÿ���� ���׷��̵� �Ǿ��� ������ Ÿ�� ���� ����
            UpdateTowerData();
            // Ÿ�� �ֺ��� ���̴� ���ݹ����� ����
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            // Ÿ�� ���ۿ� �ʿ��� ����� �����ϴٰ� ���
        }
    }
}