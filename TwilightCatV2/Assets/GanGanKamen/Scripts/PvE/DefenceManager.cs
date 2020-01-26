using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DefenceManager : MonoBehaviour
{
    public GameObject[] SpawnPointN;
    public GameObject[] SpawnPointG;
    public GameObject Normal, Rush, Gunner, Gunner2, GunnerEffect, Warning;
    public GameObject sikaku;
    public GameObject destroyobj, destroyEffect;
    float gameTime, Interval, Interval2 = 30, Interval3;
    bool isSpawn, wave3Rush, wave1Normal;
    int Stage, NSPoint, GSPoint, spawnNum = 0, gunnnerNum, wave3Normal;
    public static int[] GunnerAlive;
    public Text tex, tex2;


    // Start is called before the first frame update
    void Start()
    {
        gameTime = 65;
        Stage = 0;
        GunnerAlive = new int[4];
        for (int n = 0; n < GunnerAlive.Length; n++)
        {
            GunnerAlive[n] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        Interval -= Time.deltaTime;
        Interval2 -= Time.deltaTime;
        Interval3 -= Time.deltaTime;
        Timer();
        switch (Stage)
        {
            case 0://ウェーブ初めの処理
                if (gameTime <= 60)
                {
                    Stage = 1;

                }
                break;
            case 1://Wave1
                if (gameTime >= 30)
                {
                    if (Interval < 0 && gameTime >= 40)
                    {
                        GameObject gm;
                        if (wave1Normal == false)
                        {
                            gm = Instantiate(Normal, SpawnPointN[0].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            wave1Normal = true;
                        }
                        else
                        {
                            gm = Instantiate(Normal, SpawnPointN[1].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            wave1Normal = false;
                        }

                        Interval = 3;
                    }
                    if (gameTime <= 35)
                    {
                        if (Warning.activeSelf == false)
                        {
                            Warning.SetActive(true);
                        }

                    }

                }
                else
                {
                    if (Warning.activeSelf == true)
                    {
                        Warning.SetActive(false);
                    }
                    if (Interval < 0)
                    {
                        GameObject gm;
                        if (gameTime > 30)
                        {
                            gm = Instantiate(Normal, SpawnPointN[0].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            gm.GetComponent<Enemy_NewNormal>().speed = 3;
                        }
                        else if (gameTime > 20)
                        {
                            gm = Instantiate(Normal, SpawnPointN[1].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            gm.GetComponent<Enemy_NewNormal>().speed = 3;
                        }
                        else if (gameTime > 10)
                        {
                            gm = Instantiate(Normal, SpawnPointN[0].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            gm.GetComponent<Enemy_NewNormal>().speed = 3;
                        }

                        Interval = 1.7f;
                    }
                    if (gameTime <= 0)
                    {
                        gameTime = 10;
                        Stage = 2;
                        destroyobj.SetActive(true);
                        Instantiate(destroyEffect, destroyobj.transform);
                    }
                }
                break;
            case 2://インターバル：1
                if (gameTime <= 0)
                {
                    destroyobj.SetActive(false);
                    gameTime = 70;
                    Stage = 3;
                }
                break;
            case 3://Wave2
                if (gameTime >= 40)
                {
                    if (Interval < 0 && gameTime >= 50)
                    {
                        if (spawnNum == 0)
                        {
                            GameObject gm = Instantiate(Normal, SpawnPointN[1].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            spawnNum = 1;
                        }
                        else if (spawnNum == 1)
                        {
                            GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            spawnNum = 2;
                        }
                        else
                        {
                            GameObject gm = Instantiate(Normal, SpawnPointN[3].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            spawnNum = 0;
                        }

                        Interval = 5;
                    }
                    if (Interval2 < 0 && gameTime >= 66)
                    {

                        SpawnG(1, 5);

                    }
                    /*if (Interval2 < 0 && gameTime > 55 && gameTime < 65)
                    {
                        SpawnG(3, 10);
                    }*/
                    if (gameTime <= 45)
                    {
                        if (Warning.activeSelf == false)
                        {
                            Warning.SetActive(true);
                        }

                    }
                }
                else if (gameTime >= 10)
                {
                    if (Warning.activeSelf == true)
                    {
                        Warning.SetActive(false);
                    }
                    if (gameTime >= 35 && Interval < 0)
                    {
                        GameObject gm = Instantiate(Normal, SpawnPointN[1].transform);
                        gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                        gm.GetComponent<Enemy_NewNormal>().speed = 3;

                        Interval = 1.5f;

                    }
                    else if (gameTime >= 30 && Interval < 0)
                    {

                        GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                        gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                        gm.GetComponent<Enemy_NewNormal>().speed = 3;
                        Interval = 1.5f;

                    }
                    else if (gameTime >= 20 && Interval < 0)
                    {
                        GameObject gm = Instantiate(Normal, SpawnPointN[3].transform);
                        gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                        gm.GetComponent<Enemy_NewNormal>().speed = 3;
                        Interval = 1.5f;
                    }
                    else if (gameTime >= 10 && Interval < 0)
                    {
                        GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                        gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                        gm.GetComponent<Enemy_NewNormal>().speed = 3;
                        Interval = 1.5f;
                    }
                    if (Interval2 < 0 && gameTime <= 25)
                    {
                        SpawnG(2, 28);
                    }

                }
                if (gameTime <= 0)
                {
                    destroyobj.SetActive(true);
                    gameTime = 10;
                    Stage = 4;
                    Instantiate(destroyEffect, destroyobj.transform);
                }
                break;
            case 4://インターバル2
                if (gameTime <= 0)
                {
                    destroyobj.SetActive(false);
                    gameTime = 70;
                    Interval3 = 10;
                    Stage = 5;
                    spawnNum = 0;
                }
                break;
            case 5://Wave3
                if (gameTime >= 40)
                {
                    if (Interval < 0 && gameTime >= 50)
                    {
                        GameObject gm;
                        switch (spawnNum)
                        {
                            case 0:
                                gm = Instantiate(Normal, SpawnPointN[0].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                spawnNum = 1;
                                break;
                            case 1:
                                gm = Instantiate(Normal, SpawnPointN[1].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                spawnNum = 2;
                                break;
                            case 2:
                                gm = Instantiate(Normal, SpawnPointN[2].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                spawnNum = 3;
                                break;
                            case 3:
                                gm = Instantiate(Normal, SpawnPointN[3].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                spawnNum = 0;
                                break;
                        }

                        Interval = 5;
                    }
                    if (Interval2 < 0 && gameTime >= 66)
                    {

                        SpawnG(0, 5);

                    }
                    /*if (Interval2 < 0 && gameTime > 60 && gameTime <= 65)
                    {
                        SpawnG(2, 10);
                    }*/
                    if (Interval3 < 0)
                    {
                        if (gameTime > 59)
                        {
                            GameObject gm = Instantiate(Rush, SpawnPointN[3].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                        }
                        /*else if (gameTime > 53)
                        {
                            GameObject gm = Instantiate(Rush, SpawnPointN[2].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                        }
                        else if (gameTime > 48)
                        {
                            GameObject gm = Instantiate(Rush, SpawnPointN[1].transform);
                            gm.transform.Translate(0, 0, Random.Range(-2.5f, 2.5f));
                        }*/

                        Interval3 = 15;
                    }
                    if (gameTime <= 45)
                    {
                        if (Warning.activeSelf == false)
                        {
                            Warning.SetActive(true);
                        }

                    }

                }
                else if (gameTime >= 10)
                {
                    if (Warning.activeSelf == true)
                    {
                        Warning.SetActive(false);
                    }
                    if (Interval <= 0)
                    {
                        if (gameTime >= 35)
                        {
                            GameObject gm = Instantiate(Normal, SpawnPointN[0].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            gm.GetComponent<Enemy_NewNormal>().speed = 3;
                            Interval = 1f;
                        }
                        else if (gameTime >= 30)
                        {
                            if (wave3Normal == 0)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[1].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 1;
                            }
                            else
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 0;
                            }
                            Interval = 1.5f;
                        }
                        else if (gameTime >= 25)
                        {
                            if (wave3Normal == 0)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 1;
                            }
                            else
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[0].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 0;
                            }
                            Interval = 1.5f;
                        }
                        else if (gameTime >= 20)
                        {
                            if (wave3Normal == 0)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[3].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 1;
                            }
                            else
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[1].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 0;
                            }
                            Interval = 1.5f;
                        }
                        else if (gameTime >= 15)
                        {
                            if (wave3Normal == 0)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[0].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 1;
                            }
                            else if (wave3Normal == 1)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[1].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 2;
                            }
                            else
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 0;
                            }
                            Interval = 1.5f;
                        }
                        else if (gameTime >= 10)
                        {
                            if (wave3Normal == 0)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[0].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 1;
                            }
                            else if (wave3Normal == 1)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[1].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 2;
                            }
                            else if (wave3Normal == 2)
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[2].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 3;
                            }
                            else
                            {
                                GameObject gm = Instantiate(Normal, SpawnPointN[3].transform);
                                gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                                gm.GetComponent<Enemy_NewNormal>().speed = 3;
                                wave3Normal = 0;
                            }
                            Interval = 1.5f;
                        }
                    }

                    if (Interval3 < 0)
                    {
                        if (gameTime >= 35)
                        {
                            GameObject gm = Instantiate(Rush, SpawnPointN[0].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            wave3Rush = true;
                        }
                        else if (gameTime >= 25)
                        {
                            GameObject gm = Instantiate(Rush, SpawnPointN[2].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            wave3Rush = true;
                        }
                        else if (gameTime >= 15)
                        {
                            GameObject gm = Instantiate(Rush, SpawnPointN[1].transform);
                            gm.transform.Translate(Random.Range(-2.5f, 2.5f), 0, 0);
                            wave3Rush = true;
                        }

                        Interval3 = 10;
                    }


                    /*if (Interval2 < 0 && gameTime >= 35)
                    {

                        SpawnG(0, 10);

                    }*/
                    if (Interval2 < 0 && gameTime > 25 && gameTime <= 30)
                    {
                        SpawnG(2, 10);
                    }
                }

                if (gameTime < 0)
                {
                    destroyobj.SetActive(true);
                    Instantiate(destroyEffect, destroyobj.transform);
                    gameTime = 10;
                    Stage = 6;
                }
                break;
            case 6:
                if (gameTime < 0)
                {
                    LogToCSV.Log_GameClear();
                    SceneManager.LoadScene("GameClear");
                }
                break;
        }
    }

    void SpawnN()
    {
        Interval = 10;
        StartCoroutine(NormalSpawn());

    }

    void SpawnG(int x, int y)
    {
        Interval2 = y;
        StartCoroutine(GunnerMark(x));

    }
    void SpawnR()
    {
        Interval3 = 20;
        int z = Random.Range(0, 3);
        Instantiate(Rush, SpawnPointN[z].transform);
    }
    void SpawnG2()
    {
        Interval2 = 40;
        StartCoroutine(GunnerMark2());
    }

    void Timer()
    {
        int b = (int)gameTime;
        tex.text = "" + (b - ((b / 60) * 60));
        tex2.text = "0" + b / 60;
        if (b - ((b / 60) * 60) < 10)
        {
            tex.text = "0" + (b - ((b / 60) * 60));
            if (b - ((b / 60) * 60) < 0)
            {
                tex.text = "" + (b - ((b / 60) * 60));
            }
        }
        if (b < 0)
        {
            tex.text = "00";
            tex2.text = "00";
        }
    }

    IEnumerator GunnerMark(int y)
    {
        if (GunnerAlive[0] + GunnerAlive[1] + GunnerAlive[2] + GunnerAlive[3] > 3)
        {
            yield break;
        }
        GunnerAlive[y] = 1;
        GameObject a = Instantiate(GunnerEffect, SpawnPointG[y].transform);
        yield return new WaitForSeconds(5f);
        Destroy(a);
        GameObject gunner = Instantiate(Gunner, SpawnPointG[y].transform);
        gunner.GetComponent<Enemy_Gunner>().PosNum = y;
        SoundManager.PlayS(gunner, "SE_Gunner_Spawn");
        yield break;
    }
    IEnumerator NormalSpawn()
    {
        int x = Random.Range(0, 4);
        Instantiate(Normal, SpawnPointN[x].transform);
        yield return new WaitForSeconds(1f);
        x = Random.Range(0, 4);
        Instantiate(Normal, SpawnPointN[x].transform);
        yield break;
    }
    IEnumerator GunnerMark2()
    {
        if (GunnerAlive[0] + GunnerAlive[1] + GunnerAlive[2] + GunnerAlive[3] > 3)
        {
            yield break;
        }
        int y = Random.Range(0, 4);
        while (GunnerAlive[y] == 1)
        {
            y = Random.Range(0, 4);
        }
        GunnerAlive[y] = 1;
        GameObject a = Instantiate(GunnerEffect, SpawnPointG[y].transform);
        yield return new WaitForSeconds(6f);
        Destroy(a);
        GameObject gunner2 = Instantiate(Gunner2, SpawnPointG[y].transform);
        gunner2.GetComponent<Enemy_Gunner>().PosNum = y;
        SoundManager.PlayS(gunner2, "SE_Gunner_Spawn");
        yield break;
    }
}
