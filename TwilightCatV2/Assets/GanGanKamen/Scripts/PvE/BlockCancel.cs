using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCancel : MonoBehaviour
{
    public enum Categoly
    {
        Nomal,
        Rush,
        Bakudan,
        Ganner
    }
    public Categoly categoly;
    public CharacterrCtrl master;
    [SerializeField] private SphereCollider bombCol;
    [SerializeField] private GameObject testBomb;
    // Start is called before the first frame update
    private void Awake()
    {
        master = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterrCtrl>();
    }

    void Start()
    {
        switch (categoly)
        {
            case Categoly.Bakudan:
                StartCoroutine(Bakudan());
                break;
            case Categoly.Rush:
                StartCoroutine(Rush());
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Rush()
    {
        transform.rotation = master.body.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000f, ForceMode.Force);
        Destroy(gameObject,5f);
    }

    private IEnumerator Bakudan()
    {
        yield return new WaitForSeconds(1f);
        bombCol.enabled = true;
        //testBomb.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy"&& categoly == Categoly.Rush) //体当たり
        {
            if (collision.gameObject.GetComponent<Enemy_Normal>() != null)
            {
                collision.gameObject.GetComponent<Enemy_Normal>().DamageFromKick(5);
            }
            
            else if(collision.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {
                collision.gameObject.GetComponent<Enemy_NewNormal>().DamageFromKick(5);
            }

            else if(collision.gameObject.GetComponent<Enemy_Bomb>() != null)
            {
                collision.gameObject.GetComponent<Enemy_Bomb>().DamageFromKick(5);
            }
            else if(collision.gameObject.GetComponent<Enemy_Gunner>() != null)
            {
                //
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && categoly == Categoly.Bakudan)
        {
            if(other.gameObject.GetComponent<Enemy_Normal>()!= null)
            {
                other.gameObject.GetComponent<Enemy_Normal>().DamageFromKick(5);
            }
            else if(other.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {
                other.gameObject.GetComponent<Enemy_NewNormal>().DamageFromKick(5);
            }
            else if(other.gameObject.GetComponent<Enemy_Bomb>() != null)
            {
                other.gameObject.GetComponent<Enemy_Bomb>().DamageFromKick(5);
            }
            else if(other.gameObject.GetComponent<Enemy_Gunner>() != null)
            {
                other.gameObject.GetComponent<Enemy_Gunner>().DamageFromKick(5);
            }
        }
        if(other.gameObject.tag == "Wall" && categoly == Categoly.Bakudan)
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(categoly == Categoly.Rush&&other.gameObject.tag == "Foots")//玉乗り
        {
            other.gameObject.transform.parent.transform.position = new Vector3(transform.position.x, other.transform.parent.position.y, transform.position.z);
        }
    }
}
