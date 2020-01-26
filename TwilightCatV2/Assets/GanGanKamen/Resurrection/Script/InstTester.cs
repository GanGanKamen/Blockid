using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstTester : MonoBehaviour
{
    //動作確認用スクリプト
    TEffectInst TEI;
    // Start is called before the first frame update
    void Start()
    {
        TEI = GetComponent<TEffectInst>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TEI.TEInstance();
        }
    }
}
