using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bomb : MonoBehaviour
{
    public float diffusionTime = 0.5f;
    private float MaxHP;
    [HideInInspector]
    public bool isInvincible, isStartStan;//状態 無敵時間
    Rigidbody rb;
    float dis, DestroyTime;
    bool TriggerEntered,isExplode;
    Vector3 direction;
    private SkinnedMeshRenderer mesh;
    [SerializeField] public int EnemyLife, AttackDamage;//初期体力
    public float BombTime;
    [SerializeField]
    SphereCollider BombCol,bodycol;
    [SerializeField] GameObject TestObject;//範囲視認用オブジェクト
    public ParticleSystem pa;
    public GameObject rope;

    private bool sePlay = false;
    private bool countDown;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        mesh = gameObject.GetComponent<SkinnedMeshRenderer>();
        MaxHP = EnemyLife;
        BombTime = 0;
        StartCoroutine(startStan());
    }

    // Update is called once per frame
    void Update()
    {
        BombTime += Time.deltaTime;
        if(countDown == false && BombTime >= 5)
        {
            countDown = true;
            SoundManager.PlayS(gameObject, "SE_Bomb_Countdown",1);
        }
        if (BombTime > 8)
        {
            if(TriggerEntered == true)
            {
                DestroyTime -= Time.deltaTime;
                if(DestroyTime < 0)
                {
                    //Destroy(gameObject);
                }
            }
            if(sePlay == false)
            {
                sePlay = true;
                SoundManager.PlayS(gameObject,"SE_Bomb_Explosion",0);
            }
            if (isExplode == false)
            {
                StartCoroutine(Bomb());
            }
        }
    }

    public void DamageFromKick(int x)
    {
            EnemyLife -= x;
            BlendShape();
            StartCoroutine(Invicible());
            if (EnemyLife <= 0)//死亡
            {
                Instantiate(SystemCtrl.bakudanBlock, transform.position, Quaternion.identity);
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
            Instantiate(SystemCtrl.bakudanBlock, transform.position, Quaternion.identity);
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(gameObject);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block" && isStartStan == true)
        {

            Destroy(collision.gameObject);
            Destroy(this.gameObject);

        }
    }

    private void BlendShape()
    {
        mesh.SetBlendShapeWeight(0, 100-100 * (EnemyLife / MaxHP));
        //mesh2.SetBlendShapeWeight(0, 100 * (EnemyLife / MaxHP));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BombTime > 8 && other.gameObject.tag == "Character")
        {
            CharacterrCtrl character = other.gameObject.GetComponent<CharacterrCtrl>();
            if(character.invincible == false)
            {
                if (other.gameObject.GetComponent<PlayerPvECtrl>().RideOnTank == true)
                {
                    other.gameObject.GetComponent<PlayerPvECtrl>().Tank.GetComponent<Tank>().TankLife -= AttackDamage;
                    other.gameObject.GetComponent<PlayerPvECtrl>().tankLife();
                    SoundManager.PlayS(other.gameObject, "SE_Damage");
                    StartCoroutine(character.Invincible());

                }
                else
                {
                    SoundManager.PlayS(other.gameObject, "SE_Damage");
                    //character.hp -= AttackDamage;
                    StartCoroutine(character.Invincible());
                }
            }
            

        }
        else if (BombTime > 8 && other.gameObject.tag == "Block"||other.gameObject.tag == "SlowTrap")
        {
            if(TriggerEntered == false)
            {
                DestroyTime = 0.48f;
            }
            TriggerEntered = true;
            Destroy(other.gameObject);
        }

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


            if (EnemyLife <= 0)//死亡
            {
                Instantiate(SystemCtrl.block, transform.position, Quaternion.identity);
                FXCtrl.FXLoad("BlockChange", transform.position);
                Destroy(gameObject);
            }*/


    }
    IEnumerator Invicible()//蹴りを喰らった際の無敵時間
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }

    IEnumerator Bomb()
    {
        isExplode = true;
        BombCol.enabled = true;
        Destroy(rb);
        bodycol.enabled = false;
        //TestObject.SetActive(true);//範囲確認用コード

        pa.Play();

        yield return new WaitForSeconds(diffusionTime);
        BombCol.radius = 2;
        //TestObject.transform.localScale = new Vector3(4, 4, 4);
        yield return new WaitForSeconds(diffusionTime);
        BombCol.radius = 3;
        //TestObject.transform.localScale = new Vector3(6, 6, 6);
        mesh.enabled = false;
        rope.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    IEnumerator startStan()
    {
        isStartStan = true;
        yield return new WaitForSeconds(1.5f);
        isStartStan = false;

    }
}
