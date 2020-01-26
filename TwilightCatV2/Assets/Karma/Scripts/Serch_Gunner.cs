using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serch_Gunner : MonoBehaviour
{
    GameObject E_object;
    Enemy_Gunner EG;

    // Start is called before the first frame update
    void Start()
    {
        EG = gameObject.transform.parent.GetComponent<Enemy_Gunner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            EG.isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            EG.isActive = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            
            EG.TargetPos = other.gameObject.transform.position;

        }
    }
}
