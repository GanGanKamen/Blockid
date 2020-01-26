using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("チュートリアルメッセージ")] [SerializeField] private string[] messages;
    [SerializeField] private GameObject[] stops;

    [SerializeField] private Text tutorialText;
    [SerializeField] private GameObject msgWindow;
    [SerializeField] private GameObject[] ganner1;
    [SerializeField] private GameObject[] ganner2;
    [SerializeField] private GameObject text1,text2,text3,text4;

    private CharacterrCtrl characterr;
  
    // Start is called before the first frame update
    void Start()
    {
        characterr = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterrCtrl>();
        for(int i = 0; i < stops.Length; i++)
        {
            stops[i].SetActive(true);
        }
        //enemy1Col.enabled = false;
        //enemy2Col.enabled = false;
        msgWindow.SetActive(false);
        for(int i = 0; i < ganner1.Length; i++)
        {
            ganner1[i].tag = "Untagged";
        }
        for (int i = 0; i < ganner2.Length; i++)
        {
            ganner2[i].tag = "Untagged";
        }
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartTutorial()
    {
        LogToCSV.Log_CountStart();
        msgWindow.SetActive(true);
        tutorialText.text = messages[0];
        while(characterr.transform.position.z < -35f)
        {
            yield return null;
        }
        tutorialText.text = messages[1];
        while(characterr.normalNum < 3)
        {
            yield return null;
        }
        tutorialText.text = messages[2];
        for (int i = 0; i < ganner1.Length; i++)
        {
            ganner1[i].tag = "Enemy";
            text1.SetActive(true);
            text3.SetActive(false);
        }
        while(ganner1[0].GetComponent<Enemy_Gunner>().isDead == false)
        {
            yield return null;
        }
        stops[0].SetActive(false);
        tutorialText.text = messages[3];
        while(characterr.bakudanNum == 0)
        {
            yield return null;
        }
        tutorialText.text = messages[4];
        for (int i = 0; i < ganner2.Length; i++)
        {
            ganner2[i].tag = "Enemy";
            text2.SetActive(true);
            text4.SetActive(false);
        }

        while (ganner2[0].GetComponent<Enemy_Gunner>().isDead == false)
        {
            yield return null;
        }

        tutorialText.text = messages[5];
        stops[1].SetActive(false);
        while(characterr.transform.position.z < 40f)
        {
            yield return null;
        }
        msgWindow.SetActive(false);
    }
}
