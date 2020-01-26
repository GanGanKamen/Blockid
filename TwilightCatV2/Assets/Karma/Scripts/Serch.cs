using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serch : MonoBehaviour
{
    GameObject E_object;
    Enemy_Normal EN;

    // Start is called before the first frame update
    void Start()
    {
        EN = gameObject.transform.parent.gameObject.GetComponent<Enemy_Normal>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            EN.isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            EN.isActive = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            EN.TargetPos = other.gameObject.transform.position;

        }
    }
}
