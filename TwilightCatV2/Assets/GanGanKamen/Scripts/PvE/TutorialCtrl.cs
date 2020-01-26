using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCtrl : MonoBehaviour
{
    public GameObject Dummy, Dummy2, Dummy3, Dummy4, Kabe, Timer;
    public GameObject[] MassageWindows, MassageWindowsEN, MassageWindowBefore;
    public PlayerPvECtrl pveCtrl;
    public CharacterrCtrl characterCtrl;
    public DefenceManager dm;
    public int Process, countLR, selectNum;

    private int preProcess;//process経過確認用
    public GameObject bgmObj, CoreSE, CatSE;//bgmを鳴らすGameObj
    // Start is called before the first frame update
    private void Awake()
    {
        Process = -3;
    }
    void Start()
    {
        pveCtrl.inTutorial = true;
        preProcess = Process;
        countLR = 0;
        SoundManager.PlayS(bgmObj, "BGM_Tutorial");
        //SoundManager.PlayS(gameObject, "SE_UI_Start");
        SoundManager.PlayS(CoreSE, "SE_Core", true);
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessChange();
        switch (Process)
        {
            case -4:
                MassageWindowBefore[4].SetActive(false);
                MassageWindowBefore[5].SetActive(false);
                MassageWindowBefore[6].SetActive(false);
                MassageWindowBefore[7].SetActive(false);
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = -5;
                    MassageWindowBefore[3].SetActive(false);
                    SoundManager.StopS(bgmObj);
                    SoundManager.PlayS(bgmObj, "BGM_Stage");
                    Timer.SetActive(true);
                    pveCtrl.inTutorial = false;
                    Kabe.SetActive(false);
                    dm.gameObject.SetActive(true);
                    GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
                    foreach (GameObject obj in blocks)
                    {
                        Destroy(obj);
                    }
                }
                break;
            case -3:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = -2;
                    MassageWindowBefore[0].SetActive(false);
                    MassageWindowBefore[1].SetActive(true);
                    MassageWindowBefore[6].SetActive(true);
                    MassageWindowBefore[5].SetActive(true);
                }
                break;
            case -2:
                if (Input.GetButtonDown("Fire2"))
                {
                    SoundManager.PlayS(gameObject, "SE_UI_Message");
                    if (selectNum == 0)
                    {
                        MassageWindowBefore[1].SetActive(false);
                        MassageWindowBefore[4].SetActive(false);
                        MassageWindowBefore[5].SetActive(false);
                        MassageWindowBefore[6].SetActive(false);
                        MassageWindowBefore[7].SetActive(false);
                        MassageWindowBefore[2].SetActive(true);
                        Process = -1;
                    }
                    else
                    {
                        MassageWindowBefore[1].SetActive(false);
                        MassageWindowBefore[4].SetActive(false);
                        MassageWindowBefore[5].SetActive(false);
                        MassageWindowBefore[6].SetActive(false);
                        MassageWindowBefore[7].SetActive(false);
                        MassageWindowBefore[3].SetActive(true);
                        Process = -4;
                    }
                    

                }
                if (Input.GetAxis("Horizontal1") != 0)
                {
                    if (Input.GetAxis("Horizontal1") < -0.2f)
                    {
                        selectNum = 0;
                        MassageWindowBefore[4].SetActive(false);
                        MassageWindowBefore[5].SetActive(true);
                        MassageWindowBefore[7].SetActive(false);
                        MassageWindowBefore[6].SetActive(true);
                    }
                    else if (Input.GetAxis("Horizontal1") > 0.2f)
                    {
                        selectNum = 1;
                        MassageWindowBefore[4].SetActive(true);
                        MassageWindowBefore[5].SetActive(false);
                        MassageWindowBefore[6].SetActive(false);
                        MassageWindowBefore[7].SetActive(true);
                    }
                }
                if(Input.GetAxisRaw("DirectionalX") < 0)
                {
                    selectNum = 0;
                    MassageWindowBefore[4].SetActive(false);
                    MassageWindowBefore[5].SetActive(true);
                    MassageWindowBefore[7].SetActive(false);
                    MassageWindowBefore[6].SetActive(true);
                }
                else if(Input.GetAxisRaw("DirectionalX") > 0)
                {
                    selectNum = 1;
                    MassageWindowBefore[4].SetActive(true);
                    MassageWindowBefore[5].SetActive(false);
                    MassageWindowBefore[6].SetActive(false);
                    MassageWindowBefore[7].SetActive(true);
                }

                break;
            case -1:
                MassageWindowBefore[4].SetActive(false);
                MassageWindowBefore[5].SetActive(false);
                MassageWindowBefore[6].SetActive(false);
                MassageWindowBefore[7].SetActive(false);
                if (Input.GetButtonDown("Fire2"))
                {
                    MassageWindowBefore[2].SetActive(false);
                    MassageWindows[0].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                    Process = 0;
                }
                break;
            case 0:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 1;
                    MassageWindows[0].SetActive(false);
                    MassageWindows[1].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 1:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 2;
                    MassageWindows[1].SetActive(false);
                    MassageWindows[2].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 2:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 3;
                    MassageWindows[2].SetActive(false);
                    MassageWindows[3].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 3:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 4;
                    MassageWindows[3].SetActive(false);
                    MassageWindows[4].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 4:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 5;
                    MassageWindows[4].SetActive(false);
                    MassageWindows[5].SetActive(true);
                    Dummy.SetActive(true);
                    Dummy2.SetActive(true);
                    Dummy3.SetActive(true);
                    Dummy4.SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 5:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 6;
                    MassageWindows[5].SetActive(false);
                    MassageWindows[6].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 6:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 7;
                    MassageWindows[6].SetActive(false);
                    MassageWindows[7].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 7:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 8;
                    MassageWindows[7].SetActive(false);
                    MassageWindows[8].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 8:
                /*pveCtrl.inTutorial = false;
                if (Dummy == null)
                {
                    Process = 9;
                    MassageWindows[8].SetActive(false);
                    MassageWindows[9].SetActive(true);
                    pveCtrl.inTutorial = true;
                    pveCtrl.anim.SetBool("Run", false);
                }*/
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 9;
                    MassageWindows[8].SetActive(false);
                    MassageWindows[9].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 9:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 10;
                    MassageWindows[9].SetActive(false);
                    MassageWindows[10].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 10:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 11;
                    MassageWindows[10].SetActive(false);
                    MassageWindows[11].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 11:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 12;
                    MassageWindows[11].SetActive(false);
                    MassageWindows[12].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 12:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 13;
                    MassageWindows[12].SetActive(false);
                    MassageWindows[13].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 13:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 14;
                    MassageWindows[13].SetActive(false);
                    MassageWindows[14].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 14:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 15;
                    MassageWindows[14].SetActive(false);
                    MassageWindows[15].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 15:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 16;
                    MassageWindows[15].SetActive(false);
                    MassageWindows[16].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");


                }
                break;
            case 16:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 17;
                    MassageWindows[16].SetActive(false);
                    MassageWindows[17].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 17:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 18;
                    MassageWindows[17].SetActive(false);
                    MassageWindows[18].SetActive(true);
                    Destroy(Dummy2);
                    Destroy(Dummy3);
                    Destroy(Dummy4);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 18:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 19;
                    MassageWindows[18].SetActive(false);
                    MassageWindows[19].SetActive(true);

                }
                break;
            case 19:
                pveCtrl.inTutorial = false;
                if (Dummy == null)
                {
                    Process = 20;
                    MassageWindows[19].SetActive(false);
                    MassageWindows[20].SetActive(true);
                    pveCtrl.inTutorial = true;
                    pveCtrl.anim.SetBool("Run", false);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 20:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 21;
                    MassageWindows[20].SetActive(false);
                    MassageWindows[21].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 21:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 22;
                    MassageWindows[21].SetActive(false);
                    MassageWindows[22].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 22:
                
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 24;
                    MassageWindows[22].SetActive(false);
                    MassageWindows[24].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 23:
                //
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 24;
                    MassageWindows[23].SetActive(false);
                    MassageWindows[24].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 24:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 26;
                    MassageWindows[24].SetActive(false);
                    MassageWindows[26].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 25:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 26;
                    MassageWindows[25].SetActive(false);
                    MassageWindows[26].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 26:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 27;
                    MassageWindows[26].SetActive(false);
                    MassageWindows[27].SetActive(true);
                }
                break;
            case 27:
                pveCtrl.inTutorial = false;
                if (Input.GetButtonDown("Shoulder1") || Input.GetButtonDown("Shoulder2"))
                {
                    countLR++;
                }
                if (countLR > 1)
                {
                    pveCtrl.inTutorial = true;
                    Process = 28;
                    MassageWindows[27].SetActive(false);
                    MassageWindows[28].SetActive(true);
                    pveCtrl.anim.SetBool("Run", false);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 28:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 29;
                    MassageWindows[28].SetActive(false);
                    MassageWindows[29].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 29:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 30;
                    MassageWindows[29].SetActive(false);
                    MassageWindows[30].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 30:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 31;
                    MassageWindows[30].SetActive(false);
                    MassageWindows[31].SetActive(true);
                    characterCtrl.normalNum = 3;
                }
                break;
            case 31:
                pveCtrl.inTutorial = false;
                if (characterCtrl.normalNum <= 0)
                {
                    pveCtrl.inTutorial = true;
                    Process = 32;
                    MassageWindows[31].SetActive(false);
                    MassageWindows[32].SetActive(true);
                    pveCtrl.anim.SetBool("Run", false);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");

                }
                break;
            case 32:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 33;
                    MassageWindows[32].SetActive(false);
                    MassageWindows[33].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 33:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 35;
                    MassageWindows[33].SetActive(false);
                    MassageWindows[35].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 34:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 35;
                    MassageWindows[34].SetActive(false);
                    MassageWindows[35].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 35:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 36;
                    MassageWindows[35].SetActive(false);
                    MassageWindows[36].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 36:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 37;
                    MassageWindows[36].SetActive(false);
                    MassageWindows[37].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 37:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 38;
                    MassageWindows[37].SetActive(false);
                    MassageWindows[38].SetActive(true);
                    SoundManager.PlayS(CatSE, "SE_UI_Cat");
                }
                break;
            case 38:
                if (Input.GetButtonDown("Fire2"))
                {
                    Process = 39;
                    MassageWindows[38].SetActive(false);
                    SoundManager.StopS(bgmObj);
                    SoundManager.PlayS(bgmObj, "BGM_Stage");
                    MassageWindows[19].SetActive(false);
                    Timer.SetActive(true);
                    pveCtrl.inTutorial = false;
                    Kabe.SetActive(false);
                    dm.gameObject.SetActive(true);
                    GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
                    foreach (GameObject obj in blocks)
                    {
                        Destroy(obj);
                    }


                }
                break;
            case 39:

                break;
            case 40:
                if (Input.GetButtonDown("Fire2"))
                {
                    SoundManager.StopS(bgmObj);
                    SoundManager.PlayS(bgmObj, "BGM_Stage");
                    Process = 21;
                    MassageWindows[19].SetActive(false);
                    Timer.SetActive(true);
                    pveCtrl.inTutorial = false;
                    Kabe.SetActive(false);
                    dm.gameObject.SetActive(true);
                    GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
                    foreach (GameObject obj in blocks)
                    {
                        Destroy(obj);
                    }
                }
                break;
        }
    }

    private void ProcessChange()//processが変わった瞬間
    {
        if (preProcess != Process)
        {
            preProcess = Process;
            SoundManager.PlayS(gameObject, "SE_UI_Message");
        }
    }
    public void StopCoreSE()
    {
        SoundManager.StopS(CoreSE);
    }
}
