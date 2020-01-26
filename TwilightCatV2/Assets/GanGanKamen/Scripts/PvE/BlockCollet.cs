using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ブロック回収用コライダー
public class BlockCollet : MonoBehaviour
{
    public CharacterrCtrl master;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Ignore";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            HyperBlock hyperBlock = other.gameObject.GetComponent<HyperBlock>();
            if (hyperBlock.status == HyperBlock.Status.Nomal)
            {
                GameObject obj = Instantiate(SystemCtrl.blockVFX, hyperBlock.transform.position, hyperBlock.transform.rotation); //ブロックが集まってくる演出をインスタント
                obj.GetComponent<BlockVFX>().target = master.transform;
                Destroy(other.gameObject);
            }

        }
    }
}
