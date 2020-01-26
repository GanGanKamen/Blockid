using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TEffectInst : MonoBehaviour
{
    [Header("実際に使用する際はエフェクトを出したい")]
    [Header("キャラクターにこのCsをアタッチ")]
    [Space(48)]
    public GameObject _TEffectPrefab;
    [Space(12)]
    [Header("出現数調整(3段階)")]
    [SerializeField] [Range(1, 3)] private int EffectNumMax = 1;
    int EffectNum;

    private void Start()
    {
        EffectNum = EffectNumMax;
    }

    public void TEInstance()
    {
        var Teffects = _TEffectPrefab;
        Instantiate(Teffects,this.gameObject.transform);
        EffectNum--;
        if (EffectNum > 0)
        {
            Invoke("TEInstance", 0.1f);
        }
        else
        {
            EffectNum = EffectNumMax;
        }

    }
}
