using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void FXLoad(string name,Vector3 pos) //一回再生のパーティクルはこれ
    {
        GameObject fx = Instantiate(Resources.Load<GameObject>("FX/" + name), pos, Quaternion.identity);
        Destroy(fx, fx.GetComponent<ParticleSystem>().duration);
    }

    static public void FXLoad(string name, Vector3 pos,float destoryTime)//ループ系のパーティクルは再生時間を決める
    {
        GameObject fx = Instantiate(Resources.Load<GameObject>("FX/" + name), pos, Quaternion.identity);
        Destroy(fx, destoryTime);
    }
}
