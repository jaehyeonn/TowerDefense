using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    private int currentGold = 100;

    public int CurrentGold              ///외부에서 접근이 가능하도록 set, get 프로퍼티 만듬
    {
        set => currentGold = Mathf.Max(0, value);
        get => currentGold;
    }
}
