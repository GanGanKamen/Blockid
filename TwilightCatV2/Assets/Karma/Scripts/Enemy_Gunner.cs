using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gunner : MonoBehaviour
{
    public float MaxHP;
    [HideInInspector]
    public bool isInvincible,isActive,isDead = false;//状態 無敵時間
    Rigidbody rb;
    public int Type = 0;
    [HideInInspector]
    public Vector3 TargetPos;
    public SkinnedMeshRenderer mesh,mesh2;
    [SerializeField] public int EnemyLife;//初期体力と速度[
    [SerializeField] GameObject dodai,bodyObj;
    public int PosNum;

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Block");
        rb = gameObject.GetComponent<Rigidbody>();
        MaxHP = EnemyLife;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false&&Type == 0&& isActive == true)
        {
            gameObject.transform.LookAt(new Vector3(TargetPos.x, gameObject.transform.position.y, TargetPos.z));
        }
        if (EnemyLife <= 0&& isDead == false)//死亡
        {
            Instantiate(SystemCtrl.nomalBlock, transform.position, Quaternion.identity);
            DefenceManager.GunnerAlive[PosNum] = 0;
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(dodai);
            Destroy(bodyObj);
            Destroy(gameObject.GetComponent<SphereCollider>());
            isDead = true;
            
        }
    }


    public void DamageFromKick(int x)
    {
        EnemyLife -= x;
        BlendShape();
        StartCoroutine(Invicible());

    }

    public void DamageFromShot(int x)
    {
        EnemyLife -= x;
        BlendShape();
    }

    public void BlendShape()
    {
        mesh.SetBlendShapeWeight(0, 100-100 * (EnemyLife / MaxHP));
        mesh2.SetBlendShapeWeight(0, 100-100 * (EnemyLife / MaxHP));
        /*if (EnemyLife >= 10)
        {
            mesh.SetBlendShapeWeight(0, 0);
            mesh2.SetBlendShapeWeight(0, 0);
        }
        else if (EnemyLife >= 5)
        {
            mesh.SetBlendShapeWeight(0, 50);
            mesh2.SetBlendShapeWeight(0, 50);
        }
        else
        {
            mesh.SetBlendShapeWeight(0, 100);
            mesh2.SetBlendShapeWeight(0, 100);
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(-1, 1), 2, Random.Range(-1, 1));
        }
    }
    private void OnTriggerStay(Collider other)
    {

        /*if (other.gameObject.tag == "Attack" && isInvincible == false)
        {
            if (other.gameObject.name == "Attack")//近接か遠距離かを判別
            {
                EnemyLife -= 5;
                StartCoroutine(Invicible());
                mesh.SetBlendShapeWeight(0, mesh.GetBlendShapeWeight(0) - (100 / MaxHP));
            }
            else
            {
                EnemyLife -= 30;
            }


            

        }*/
        

    }
    public IEnumerator Invicible()//蹴りを喰らった際の無敵時間
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
}
