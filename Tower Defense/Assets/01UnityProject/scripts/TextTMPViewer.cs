using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;     // Text - TextMeshPro UI [플레이어의 체력]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;   // Text - TextMeshPro UI [플레이어의 골드]
    [SerializeField]
    private PlayerHP playerHP;                // 플레이어의 체력 정보
    [SerializeField]
    private PlayerGold playerGold;            // 플레이어의 골드 정보

    private void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;  //text에 플레이어 현재체력 / 최대체력 나오게하기
        textPlayerGold.text = playerGold.CurrentGold.ToString(); 
    }
}
