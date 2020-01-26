using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap2 : MonoBehaviour
{
    public SlowTrap st;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Enemy_Normal>() != null)
            {
                other.gameObject.GetComponent<Enemy_Normal>().speedmultiple = st.SpeedMultiple;

            }
            if (other.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {

                other.gameObject.GetComponent<Enemy_NewNormal>().speedmultiple = st.SpeedMultiple;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Enemy_Normal>() != null)
            {
                other.gameObject.GetComponent<Enemy_Normal>().speedmultiple = 1;

            }
            if (other.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {

                other.gameObject.GetComponent<Enemy_NewNormal>().speedmultiple = 1;

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && st.isAlive == false)
        {
            if (other.gameObject.GetComponent<Enemy_Normal>() != null)
            {
                other.gameObject.GetComponent<Enemy_Normal>().speedmultiple = 1;

            }
            if (other.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {

                other.gameObject.GetComponent<Enemy_NewNormal>().speedmultiple = 1;

            }
        }
    }
}
