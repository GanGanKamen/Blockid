using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NewNormal : MonoBehaviour
{
    [HideInInspector]
    private float MaxHP;//計算用
    public GunnerWeapon gw;
    [HideInInspector]
    public bool isActive, isStaned, isInvincible;//状態 プレイヤーを発見、スタン、無敵時間、突進準備
    [HideInInspector]
    Rigidbody rb;
    public SkinnedMeshRenderer mesh,mesh2;
    [SerializeField] public int EnemyLife, AttackDamage;//初期体力と速度
    public float speed, speedDef;
    [Header("mode = 1：動かない")]
    public int mode;
    public float speedmultiple = 1;
    Vector3 vec;



    // Start is called before the first frame update
    void Start()
    {
        speedDef = speed;
        rb = gameObject.GetComponent<Rigidbody>();
        MaxHP = EnemyLife;
        if (mode == 0)
        {
            //rb.AddForce(Random.Range(-speed, speed), 0, Random.Range(-speed, speed), ForceMode.Impulse);
            vec = new Vector3(0 - transform.position.x, 0, 0 - transform.position.z).normalized;

            //rb.AddForce(vec.normalized*speed,ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 0&&isStaned == false)
        {
            rb.velocity = new Vector3(vec.x * speed* speedmultiple, rb.velocity.y, vec.z * speed* speedmultiple);
        }
    }



    public void DamageFromKick(int x)
    {
        EnemyLife -= x;
        BlendShape();
        StartCoroutine(Invicible());
        

        if (EnemyLife <= 0)//死亡
        {
            if (gw != null)
            {
                gw.SummonCount++;
            }
            Instantiate(SystemCtrl.nomalBlock, transform.position, Quaternion.identity);
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(gameObject);

        }
    }

    public void DamageFromShot(int x)
    {
        EnemyLife -= x;
        BlendShape();
        if (EnemyLife <= 0)//死亡
        {
            if (gw != null)
            {
                gw.SummonCount++;
            }
            Instantiate(SystemCtrl.nomalBlock, transform.position, Quaternion.identity);
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(gameObject);

        }
    }

    public void StayInShot()
    {
        speed = 0;
    }

    public void ExitShot()
    {
        speed = speedDef;
    }

    private void BlendShape()
    {
        mesh.SetBlendShapeWeight(0, 100-100 * (EnemyLife / MaxHP));
        mesh2.SetBlendShapeWeight(0, 100-100 * (EnemyLife / MaxHP));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character"&&mode ==0)//プレイヤーへの攻撃
        {
            //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            CharacterrCtrl character = collision.gameObject.GetComponent<CharacterrCtrl>();
            if (character.invincible == false)
            {
                if (collision.gameObject.GetComponent<PlayerPvECtrl>().RideOnTank == true)
                {
                    collision.gameObject.GetComponent<PlayerPvECtrl>().Tank.GetComponent<Tank>().TankLife -= AttackDamage;
                    collision.gameObject.GetComponent<PlayerPvECtrl>().tankLife();
                    if(character.gameObject.GetComponent<PlayerPvECtrl>().RideOnTank == true) SoundManager.PlayS(collision.gameObject, "SE_Tank_Damage");
                    else    SoundManager.PlayS(collision.gameObject, "SE_Damage");
                    StartCoroutine(character.Invincible());
                    StartCoroutine(Stan(2f));
                }
                else
                {
                    //character.hp -= AttackDamage;
                    SoundManager.PlayS(collision.gameObject, "SE_Damage");
                    StartCoroutine(character.Invincible());
                    StartCoroutine(Stan(2f));
                }
            }

        }
        else if (collision.gameObject.tag == "Block"&&isStaned == false)//ブロックの破壊
        {
            HyperBlock hb = collision.gameObject.GetComponent<HyperBlock>();
            hb.blockLife--;
            hb.BlendShape();
            StartCoroutine(Stan(5f));
            rb.velocity = -rb.velocity;
            if(hb.canNotCollect == true&&hb.blockLife <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }
    IEnumerator Stan(float time)//ブロック破壊時のスタン処理
    {
        //SoundManager.PlayS(gameObject, "SE_Enemy_Down", true);
        rb.velocity = Vector3.zero;
        isStaned = true;
        yield return new WaitForSeconds(time);
        isStaned = false;
        //SoundManager.StopS(gameObject);
    }
    IEnumerator Invicible()//蹴りを喰らった際の無敵時間
    {
        isInvincible = true;
        isStaned = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
        isStaned = false;
    }

}
