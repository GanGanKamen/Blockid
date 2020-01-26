using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    public Collider col;
    public ParticleSystem particle;
    bool isExplode;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        isExplode = true;
        col.enabled = true;
        particle.Play();
        StartCoroutine(Explode());
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isExplode == true)
        {
            SoundManager.PlayS(other.gameObject, "SE_Tank_Hit");
            if (other.gameObject.GetComponent<Enemy_Normal>() != null)
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

            }
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield break;
    }
}
