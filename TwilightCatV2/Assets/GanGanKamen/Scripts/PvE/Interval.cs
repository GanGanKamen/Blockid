using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interval : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            /*if (other.gameObject.GetComponent<Enemy_Normal>() != null)
            {
                other.gameObject.GetComponent<Enemy_Normal>().DamageFromKick(15);

            }
            if (other.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {

                other.gameObject.GetComponent<Enemy_NewNormal>().DamageFromKick(15);

            }
            if (other.gameObject.GetComponent<Enemy_Gunner>() != null)
            {

                other.gameObject.GetComponent<Enemy_Gunner>().DamageFromKick(15);

            }
            if (other.gameObject.GetComponent<GunnerWeapon>() != null)
            {

                other.gameObject.GetComponent<GunnerWeapon>().DamageFromKick(15);


            }
            if (other.gameObject.GetComponent<Enemy_Bomb>() != null)
            {

                other.gameObject.GetComponent<Enemy_Bomb>().DamageFromKick(15);

            }*/
            if (other.gameObject.GetComponent<GunnerWeapon>() != null)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
            
        }
    }
}
