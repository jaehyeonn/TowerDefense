using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;     // Text - TextMeshPro UI [�÷��̾��� ü��]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;   // Text - TextMeshPro UI [�÷��̾��� ���]
    [SerializeField]
    private PlayerHP playerHP;                // �÷��̾��� ü�� ����
    [SerializeField]
    private PlayerGold playerGold;            // �÷��̾��� ��� ����

    private void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;  //text�� �÷��̾� ����ü�� / �ִ�ü�� �������ϱ�
        textPlayerGold.text = playerGold.CurrentGold.ToString(); 
    }
}
