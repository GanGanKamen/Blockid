using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    bool muteki;
    public int CoreLife = 4;
    public GameObject core, core1, core2;
    public Canvas can;
    public GameObject damageImage;
    public TutorialCtrl tc;

    // Start is called before the first frame update
    void Awake()
    {
        CoreLife = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(CoreLife <= 0)
        {
            SoundManager.PlayS(gameObject, "SE_Core_Destory");
            tc.StopCoreSE();
            Destroy(gameObject);
            LogToCSV.Log_GameOver();
            SceneManager.LoadScene("GameOver");
            
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(muteki == false && collision.gameObject.tag == "Enemy"&&collision.gameObject.GetComponent<Enemy_Bomb>() == null)
        {
            CoreLife --;
            if (CoreLife > 1)
            {
                SoundManager.PlayS(gameObject, "SE_Core_Hit");
            }
            else
            {
                SoundManager.PlayS(gameObject, "SE_Core_Hit_Final");
            }
            StartCoroutine(Muteki());
            CoreBreak();
            GameObject da = Instantiate(damageImage, can.transform);
            da.transform.parent = can.transform;
            Destroy(collision.gameObject);
            
        }
    }

    void CoreBreak()
    {
        switch (CoreLife)
        {
            case 3:
                core.SetActive(false);
                core1.SetActive(true);
                break;
            case 2:
                core1.SetActive(false);
                core2.SetActive(true);
                break;
        }
    }

    IEnumerator Muteki()
    {
        muteki = true;
        yield return new WaitForSeconds(5f);
        muteki = false;
        yield break;
        
    }
}
