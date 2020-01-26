using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPvECtrl : MonoBehaviour
{
    [Header("プレイヤーオブジェクト")] public GameObject player;

    [Header("ゲームパッドナンバー")] [Range(0, 4)] public int dualshock4Num;
    [Space(30)]

    public CharacterrCtrl playerCharacter;
    public Vector3 aimPoint;  //aim.rectTransform.positionを別のクラスに持っていく
    public Vector3 aimPos;
    public Image aim;
    public float cameraResetTime;
    public bool isCameraReset; //カメラリセット処理中
    public Transform playerCamera;
    public float yaw, pitch;
    public bool RideOnTank;
    public GameObject Tank,TankLife;
    public GameObject[] TankLifeIcon;
    int SelectBlockNum;
    public bool inTutorial;
    public Animator anim;
    public PvEGUICtrl guiCtrl;


    // Start is called before the first frame update
    private void Awake()
    {
        playerCharacter = player.GetComponent<CharacterrCtrl>();
        isCameraReset = false;
        anim = playerCharacter.body.GetComponent<Animator>();
    }

    void Start()
    {
        pitch = playerCharacter.body.transform.eulerAngles.x + 10f;
        yaw = playerCharacter.body.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        aimPoint = aim.rectTransform.position;
        aimPos = (new Vector3(aimPoint.x, aimPoint.y - 0.4f, aimPoint.z)
            - playerCharacter.muzzle.position);//照準方向ベクトル
        if (playerCharacter.canCtrl == true && RideOnTank == false&&inTutorial == false)
        {
            KeyCtrl();
        }

        if (isCameraReset == true)
        {
            //playerCharacter.lookat.rotation = Quaternion.Lerp(playerCharacter.lookat.rotation, playerCharacter.body.transform.rotation, cameraResetTime);
            pitch = playerCharacter.body.transform.eulerAngles.x + 10f;
            yaw = playerCharacter.body.transform.eulerAngles.y;
            isCameraReset = false;
            
        }
        if (isCameraReset == false && inTutorial == false)
        {
            //playerCharacter.lookat.eulerAngles += new Vector3(-Dualshock4.RightStick(dualshock4Num).y * 3f, Dualshock4.RightStick(dualshock4Num).x * 10f,0);
            pitch += Input.GetAxis("Vertical1") * 100 * Time.deltaTime;
            yaw += Input.GetAxis("Horizontal1") * 200 * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -20f, 50f);


        }
        
        playerCharacter.lookat.eulerAngles = new Vector3(pitch, yaw, 0);
    }

    private void KeyCtrl()
    {
        //playerCharacter.GuardMove(Dualshock4.RightStick(dualshock4Num).y);
        if (playerCharacter.isAttack == false)
        {
            CharaMove();
        }

        

        if (Input.GetButtonDown("Fire1"))
        {
            playerCharacter.Jump();
        }
        if (Input.GetButtonDown("Trigger2"))
        {
            playerCharacter.CreatBlocks();
        }

        /*if (Input.GetButtonDown("Shoulder2"))
        {
            playerCharacter.BlockCancel();
        }*/
        if (Input.GetButtonDown("Fire3"))
        {
            //playerCharacter.CreatBlocksUnder();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(playerCharacter.Attack());
        }
        /*if (Input.GetButtonDown("Trigger2"))
        {
            playerCharacter.StartShoot();
        }
        else if (Input.GetButton("Trigger2"))
        {
            playerCharacter.Shooting();
        }
        else if (Input.GetButtonUp("Trigger2"))
        {
            playerCharacter.ShootOver();
        }*/
        if (Input.GetButtonDown("Trigger1"))
        {
            //playerCharacter.BlockCollect();
            //playerCharacter.Tank();
        }
        /*if (Input.GetAxisRaw("DirectionalX") > 0)
        {
            playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Bakudan);
        }
        else if (Input.GetAxisRaw("DirectionalX") < 0)
        {
            playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Rush);
        }
        else if (Input.GetAxisRaw("DirectionalY") > 0)
        {
            playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Nomal);
        }
        else if (Input.GetAxisRaw("DirectionalY") < 0)
        {
            playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Tank);
        }*/
        if (Input.GetButtonDown("Shoulder1"))
        {
            if(SelectBlockNum >0)
            {
                SelectBlockNum--;
                BlockSelectOnGui();
            }
            else
            {
                SelectBlockNum = 2;
                BlockSelectOnGui();
            }
        }
        if (Input.GetButtonDown("Shoulder2"))
        {
            if (SelectBlockNum < 2)
            {
                SelectBlockNum++;
                BlockSelectOnGui();
            }
            else
            {
                SelectBlockNum = 0;
                BlockSelectOnGui();
            }
        }
    }
    /*
    private IEnumerator CameraReset()
    {
        if (playerCharacter.isShoot == true)
        {
            yield break;
        }
        isCameraReset = true;
        yield return new WaitForSeconds(1f);
        isCameraReset = false;
    }
    */
    private void CharaMove() //スプラトゥーン式の移動
    {
        var cameraFoward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1).normalized);
        var cameraRight = Vector3.Scale(playerCamera.transform.right, new Vector3(1, 0, 1).normalized);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Run", true);
            var direction = Input.GetAxis("Horizontal") * cameraRight + Input.GetAxis("Vertical") * cameraFoward;
            playerCharacter.CharacterMove(direction);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        /*if(Dualshock4.RightStick(dualshock4Num).x != 0 || Dualshock4.RightStick(dualshock4Num).y != 0)
        {
            var direction = Dualshock4.RightStick(dualshock4Num).x * cameraRight + Dualshock4.RightStick(dualshock4Num).y * cameraFoward;
            playerCharacter.body.transform.localRotation = Quaternion.LookRotation(direction);
        }*/
    }
    private void BlockSelectOnGui()
    {
        SoundManager.PlayS(gameObject, "SE_UI_Recipe_Select");
        switch (SelectBlockNum)
        {
            case 0:
                playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Nomal);
                break;
            case 1:
                playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Segway);
                break;
            case 2:
                playerCharacter.BlockSelect(CharacterrCtrl.SelectedBlock.Tank);
                break;
        }
    }

    public void tankLife()
    {
        for (int x = 0; x < TankLifeIcon.Length; x++)
        {
            TankLifeIcon[x].SetActive(false);
        }
        for (int n = 0;n< Tank.GetComponent<Tank>().TankLife; n++)
        {
            TankLifeIcon[n].SetActive(true);
        }
           
    }

    
}
