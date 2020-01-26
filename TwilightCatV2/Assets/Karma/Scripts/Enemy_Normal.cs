using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Normal : MonoBehaviour
{
    [HideInInspector]
    public GunnerWeapon gw;
    private float MaxHP;//計算用
    [HideInInspector]
    public bool isActive,isStaned,isInvincible,IsReady,isStartStan,isRest;//状態 プレイヤーを発見、スタン、無敵時間、突進準備
    float dis;//プレイヤーの方向
    [HideInInspector]
    public Vector3 TargetPos;//プレイヤーの座標
    Vector3 direction;//正規化したプレイヤーとの方向のベクトル
    Rigidbody rb;
    public SkinnedMeshRenderer mesh,mesh2;
    [SerializeField] public int EnemyLife, speed,speedDef, AttackDamage;//初期体力と速度
    public float knockBackRange, speedmultiple = 1;
    [Header("この数値よりプレイヤーに接近した場合に加速開始")]
    [SerializeField]float AttackRange;
    int count;
    Renderer render;
    public GameObject body,Eye;
    private bool dashnow;
    IEnumerator dashIE;
    public Texture eye_open, eye_close;
   

    // Start is called before the first frame update
    void Start()
    {
        
        speedDef = speed;
        rb = gameObject.GetComponent<Rigidbody>();
        MaxHP = EnemyLife;
        StartCoroutine(startStan());
        render = body.GetComponent<Renderer>();
        
        dashIE = dash();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive == true&& isStaned == false && IsReady == false &&dashnow == false)
        {
            Move();
           
        }
    }

    private void Move()//移動処理
    {
        direction = TargetPos - transform.position;
        dis = direction.magnitude;
        direction = direction / dis;
        rb.velocity = direction * speed* speedmultiple * Time.deltaTime;
        if (dis < AttackRange && isRest == false)
        {
            rb.velocity = new Vector3(0,0,0);
            dashIE = null;
            dashIE = dash();
            StartCoroutine(dashIE);
        }

    }

    public void DamageFromKick(int x)
    {
        EnemyLife -= x;
        BlendShape();
        StartCoroutine(Invicible());
        rb.velocity = Vector3.zero;
        rb.AddForce(-direction * speed * 3);
        speed = speedDef / 2;//速度を変更
        if (IsReady == true)
        {
            rb.velocity = new Vector3(0, 0, 0);
            render.material.color = Color.white;
            StopCoroutine(dashIE);
            dashIE = null;
            dashIE = dash();
            IsReady = false;
        }
        
        if (EnemyLife <= 0)//死亡
        {
            if (gw != null)
            {
                gw.SummonCount++;
            }
            Instantiate(SystemCtrl.rushBlock, transform.position, Quaternion.identity);
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(gameObject);

        }
    }

    public void DamageFromShot(int x)
    {
        EnemyLife -= x;
        speed = speedDef / 2;
        BlendShape();
        if (IsReady == true&& isRest == false)
        {
            rb.velocity = new Vector3(0, 0, 0);
            render.material.color = Color.white;
            StopCoroutine(dashIE);
            dashIE = null;
            dashIE = dash();
            IsReady = false;
        }
        if (EnemyLife <= 0)//死亡
        {
            if (gw != null)
            {
                gw.SummonCount++;
            }
            Instantiate(SystemCtrl.rushBlock, transform.position, Quaternion.identity);
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
        if (collision.gameObject.tag == "Character")//プレイヤーへの攻撃
        {
            //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            CharacterrCtrl character = collision.gameObject.GetComponent<CharacterrCtrl>();
            if (character.invincible == false)
            {
                if (collision.gameObject.GetComponent<PlayerPvECtrl>().RideOnTank == true)
                {
                    collision.gameObject.GetComponent<PlayerPvECtrl>().Tank.GetComponent<Tank>().TankLife -= AttackDamage;
                    collision.gameObject.GetComponent<PlayerPvECtrl>().tankLife();
                    SoundManager.PlayS(collision.gameObject, "SE_Damage");
                    StartCoroutine(character.Invincible());
                    StartCoroutine(Stan(2f));
                }
                else
                {
                    SoundManager.PlayS(collision.gameObject, "SE_Damage");
                    //character.hp -= AttackDamage;
                    StartCoroutine(character.Invincible());
                    direction = TargetPos - transform.position;
                    dis = direction.magnitude;
                    direction = direction / dis;
                    collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * knockBackRange, 0, direction.z * knockBackRange);
                    StartCoroutine(Stan(2f));
                }
            }
            
        }else if(collision.gameObject.tag == "Block")//ブロックの破壊
        {
            HyperBlock hb = collision.gameObject.GetComponent<HyperBlock>();
            hb.blockLife--;
            hb.BlendShape();
            if (hb.canNotCollect == true && hb.blockLife <= 0)
            {
                Destroy(collision.gameObject);
            }
            if (isStartStan == true)
            {
                if (gw != null)
                {
                    gw.SummonCount++;
                }
                Destroy(this.gameObject);
            }
            rb.velocity = new Vector3(0, 0, 0);
            StartCoroutine(Stan(5f));
        }
    }

    

    IEnumerator Stan(float time)//ブロック破壊時のスタン処理
    {
        Eye.GetComponent<Renderer>().material.SetTexture("_MainTex",eye_close);
        rb.velocity = Vector3.zero;
        isStaned = true;
        yield return new WaitForSeconds(time);
        isStaned = false;
        Eye.GetComponent<Renderer>().material.SetTexture("_MainTex", eye_open);
    }
    IEnumerator Invicible()//蹴りを喰らった際の無敵時間
    {
        isInvincible = true;
        isStaned = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
        isStaned = false;
    }

    IEnumerator dash()
    {
        IsReady = true ;
        render.material.color = Color.magenta;
        SoundManager.PlayS(gameObject, "SE_Rush_Change");
        yield return new WaitForSeconds(3f);
        IsReady = false;
        dashnow = true;
        direction = TargetPos - transform.position;
        dis = direction.magnitude;
        direction = direction / dis;
        rb.velocity = direction * speed/10* speedmultiple;
        render.material.color = Color.red;
        SoundManager.PlayS(gameObject, "SE_Rush_Attack");
        yield return new WaitForSeconds(3f);
        dashnow = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(Rest());


    }
    IEnumerator startStan()
    {
        isStartStan = true;
        StartCoroutine(Stan(4f));
        yield return new WaitForSeconds(1.5f);
        isStartStan = false;

    }
    IEnumerator Rest()
    {
        isRest = true;
        yield return new WaitForSeconds(5);
        render.material.color = Color.white;
        isRest = false;
    }

}
