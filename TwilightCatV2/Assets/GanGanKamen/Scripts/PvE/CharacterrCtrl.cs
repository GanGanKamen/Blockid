using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonCamera;

public class CharacterrCtrl : MonoBehaviour
{
    [Header("移動速度")] public float speed;
    [Header("銃口")] public Transform muzzle;
    [Header("キャラクターの色")] public Color charaColor;
    [Header("近距離攻撃クールダウン")] public float coolDown;
    [Header("遠距離攻撃クールダウン")] public float beamCoolDown;
    [Header("無敵時間")] [SerializeField] private float invincibleTime = 1f;
    [Header("溜め間隔")] [SerializeField] private float shootInterval = 20f;
    public enum Status
    {
        Normal,
        Build
    }
    public Status status;
    public bool canCtrl;
    public GameObject body; //キャラクターモデル
    public Transform lookat; //カメラ向き
    public CharacterFoots foots; //足当たり判定
    public CameraController camera;
    private float zoom;
    private float nowZoom;
    public bool isAiming;
    private Rigidbody rb;
    private Vector3 characterPos;
    private bool canShoot;
    public Vector3 aimPoint;
    public Vector3 aimPos;  //PlayerCtrlから値を持ってくる
    public bool isAttack;
    public GameObject attackObj;
    public bool canAttack;
    public int animCount;//アニメーションで利用します
    public int hp;
    public GameObject beamCharge, charged; //ビーム溜めると溜め完了のエフェクト
    Animator anim;
    private Vector3 prePos; //移動判定
    public FrontCheck front;
    [SerializeField] private float recoveryCount; //エネルギー自動回復
    public bool invincible;//無敵状態
    public int normalNum, rushNum, bakudanNum, gannerNum;
    private bool isShoot; //遠距離攻撃関連
    private float nowShootCount;
    private int nowChargeLevel;
    private bool chargeTrigger;
    public int beamLevel;
    TEffectInst tEffect;
    

    public enum SelectedBlock
    {
        Nomal,
        Tank,
        Segway
    }
    public SelectedBlock selectedBlock; //選択されているブロックの種類
    // Start is called before the first frame update
    private void Awake()
    {
        tEffect = gameObject.GetComponent<TEffectInst>();
        rb = GetComponent<Rigidbody>();
        characterPos = GetComponent<Transform>().position;
        attackObj.GetComponent<Blockid>().category = Blockid.Category.Melee;
    }

    void Start()
    {
        anim = body.GetComponent<Animator>();
        anim.SetBool("Victory", false);
        status = Status.Normal;
        zoom = 1;
        nowZoom = zoom;
        prePos = transform.position;
        canCtrl = true;
        recoveryCount = -1;
        canAttack = true;
        canShoot = true;
        beamLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("OnGround", foots.onGround);
        anim.SetBool("Throw", false);
        anim.SetBool("Blockize", false);

        if (GetComponent<PlayerPvECtrl>())
        {
            aimPoint = GetComponent<PlayerPvECtrl>().aimPoint;
            aimPos = GetComponent<PlayerPvECtrl>().aimPos;
        }
        if (nowZoom != zoom)
        {
            nowZoom = Mathf.Lerp(nowZoom, zoom, 0.2f);
        }
        camera.offsetVector = new Vector3(0, 0.1f, nowZoom);
        
        
    }

    public void BlockCancel() //ブロック解除
    {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            block.GetComponent<HyperBlock>().isCancel = true;
        }
    }

    public void BlockCollect() //ブロック回収
    {
        
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        int collectedNum = 0;
        foreach (GameObject blockObj in blocks)
        {
            
            HyperBlock hb = blockObj.GetComponent<HyperBlock>();
            if (hb.isMove == false&&hb.canNotCollect == false)
            {

                collectedNum += 1;
                StartCoroutine(blockObj.GetComponent<HyperBlock>().BackToMaster());
            }
            
        }
        if(collectedNum > 0)
        {
            SoundManager.PlayS(gameObject, "SE_PickUp_1");
            StartCoroutine(BlockCollectSoundPlay());
        }
    }

    private IEnumerator BlockCollectSoundPlay()
    {
        yield return new WaitForSeconds(0.5f);
        
        yield break;
    }

    public void CharacterMove(Vector3 direction)  //移動
    {
        
        if (isShoot == false && isAiming == false)
        {

            transform.Translate(direction * Time.deltaTime * speed);
            if (GetComponent<PlayerPvECtrl>() != null)
            {
                if (GetComponent<PlayerPvECtrl>().isCameraReset == false)
                {
                    body.transform.localRotation = Quaternion.LookRotation(direction);
                }
            }
        }
        else
        {
            transform.Translate(direction * Time.deltaTime * speed);
            body.transform.localRotation = Quaternion.Euler(0, lookat.eulerAngles.y, lookat.eulerAngles.z);
        }
        

    }

    public void Jump() //ジャンプ
    {
        if (foots.onGround == true)
        {
            anim.SetBool("Fly", true);
            rb.AddForce(0, 240f, 0);
            SoundManager.PlayS(gameObject, "SE_Jump");
            StartCoroutine(wait());
            
            
        }
    }

    public void Aiming()  //照準
    {
        isAiming = true;
        zoom = 2f;
    }

    public void AimOver()
    {
        isAiming = false;
        zoom = 1;
    }
    
    public void Shooting() //ビームをためる
    {
        if (canShoot == false||chargeTrigger == false)
        {
            return;
        }
        //Aiming();
        if(nowShootCount < shootInterval)
        {
            beamCharge.SetActive(true);
        }
        
        isShoot = true;
        anim.SetBool("Blockize", true);
        animCount++;
        if (animCount == 20)
        {
            anim.SetFloat("BlockizeBlend", 0);
        }
        if ((int)nowShootCount == shootInterval&&beamLevel >= 2)
        {
            beamCharge.SetActive(false);
            charged.SetActive(true);
            nowChargeLevel = 1;
        }
        else
        {
            charged.SetActive(false);
            nowChargeLevel = 0;
            nowShootCount += (10 * Time.deltaTime);
        }
    }

    public void StartShoot()
    {
        if(canShoot == false)
        {
            return;
        }
        chargeTrigger = true;
        SoundManager.PlayS(gameObject, "SE_Change");
    }

    public void ShootOver() //溜めたビームを発射する
    {
        if(chargeTrigger == false)
        {
            return;
        }
        if (canShoot == true)
        {
            canShoot = false;
            beamCharge.SetActive(false);
            charged.SetActive(false);
            nowShootCount = 0;
            body.transform.localRotation = Quaternion.Euler(0, lookat.eulerAngles.y, lookat.eulerAngles.z);
            SoundManager.StopS(gameObject,0);
            GameObject beam;
            switch (nowChargeLevel)
            {
                case 0:
                    beam = Instantiate(SystemCtrl.beam, muzzle.position, Quaternion.LookRotation(aimPos));
                    Blockid blockid = beam.GetComponent<Blockid>();
                    blockid.category = Blockid.Category.Beam;
                    break;
                case 1:
                    beam = Instantiate(SystemCtrl.doublebeam, muzzle.position, Quaternion.LookRotation(aimPos));
                    Blockid blockid1 = beam.GetComponent<Blockid>();
                    blockid1.category = Blockid.Category.Beam;
                    break;
            }
            SoundManager.PlayS(gameObject, "SE_Laser");
            nowChargeLevel = 0;
            isShoot = false;
            chargeTrigger = false;
            anim.SetBool("Blockize", false);
            Invoke("EnergyRecovery", beamCoolDown);
        }
    }

    public void BlockSelect(SelectedBlock selected)
    {
        selectedBlock = selected;
    }

    public void CreatBlocks()//ブロックを置く
    {
        if (front.isCollide == true || foots.onGround == false)
        {
            return;
        }
        GameObject blockObj;
        
        switch (selectedBlock)
        {
            case SelectedBlock.Nomal:
                if (normalNum >= 3)
                {
                    blockObj = Instantiate(SystemCtrl.WallBlock, front.transform.position, front.transform.rotation);
                    normalNum -= 3;
                    SoundManager.PlayS(blockObj, "SE_Create");
                    
                }
                break;
            case SelectedBlock.Tank:
                if (normalNum >= 5&&bakudanNum >0&&rushNum>0)
                {
                    blockObj = Instantiate(SystemCtrl.Tank, front.transform.position+front.transform.forward+front.transform.up, front.transform.rotation);
                    normalNum -= 5;
                    bakudanNum -= 1;
                    rushNum -= 1;
                    SoundManager.PlayS(blockObj, "SE_Tank_Create");

                }
                break;
            case SelectedBlock.Segway:
                if(normalNum >=5&&bakudanNum > 0)
                {
                    blockObj = Instantiate(SystemCtrl.Trap, front.transform.position + front.transform.forward + front.transform.up, front.transform.rotation);
                    normalNum -= 5;
                    bakudanNum -= 1;
                    SoundManager.PlayS(blockObj, "SE_Spark_Create");
                }
                /*if (bakudanNum > 0)
                {
                    blockObj = Instantiate(SystemCtrl.bakudanBlock, front.transform.position, front.transform.rotation);
                    bakudanNum -= 1;
                    SoundManager.PlayS(blockObj);
                }*/
                break;
        }
    }

    private void EnergyRecovery() //ビームエネルギー自動回復
    {
        canShoot = true;
    }

    public IEnumerator Attack()  //近距離攻撃
    {
        if (foots.onGround == false || isAttack == true || status == Status.Build || canAttack == false||invincible == true)
        {
            yield break;
        }
        invincible = false;
        canAttack = false;
        isAttack = true;
        
        SoundManager.PlayS(gameObject, "SE_Swoosh");
        anim.SetTrigger("Kick");
        yield return new WaitForSeconds(0.1f);
        attackObj.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        invincible = false;
        attackObj.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
        invincible = false;
        invincible = false;
        yield break;
    }

    public void CreatBlocksUnder()
    {
        if(foots.canCreate == false||foots.onGround == false||normalNum < foots.createNum)
        {
            return;
        }
        transform.position = new Vector3(foots.pointPos.x, transform.position.y, foots.pointPos.z) + transform.up * foots.createNum;
        GameObject[] blockObjs = new GameObject[foots.createNum];
        for (int i = 0; i < foots.createNum; i++)
        {
            blockObjs[i] = Instantiate(SystemCtrl.nomalBlock2, 
                new Vector3(foots.pointPos.x, foots.pointPos.y + 0.5f, foots.pointPos.z)+Vector3.up * i, Quaternion.identity);

        }
        normalNum -= foots.createNum;
    }

    public void CanNotCtrl()
    {
        ShootOver();
        canCtrl = false;
    }

    public IEnumerator Invincible()
    {
        if(invincible == true)
        {
            yield break;
        }
        anim.SetTrigger("Damage");
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
        yield break;
    }
    public IEnumerator Respawn()
    {
        canCtrl = false;
        anim.SetBool("Run", false);
        anim.SetBool("Down", true);
        SoundManager.PlayS(gameObject, "SE_Player_Down");
        yield return new WaitForSeconds(8f);
        tEffect.TEInstance();
        Debug.Log("8f");
        hp = 4;
        anim.ResetTrigger("Damage");
        anim.SetBool("Down", false);
        canCtrl = true;
        SoundManager.PlayS(gameObject, "SE_Player_Revival");
        yield break;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Fly", false);
        yield break;
    }
    public void Tank()
    {
        if (front.isCollide == true || foots.onGround == false)
        {
            return;
        }
        Instantiate(SystemCtrl.Tank, front.transform.position + front.transform.forward + front.transform.up, front.transform.rotation);
    }
}
