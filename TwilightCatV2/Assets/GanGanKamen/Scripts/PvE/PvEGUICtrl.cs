using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PvEGUICtrl : MonoBehaviour
{
    [SerializeField] private CharacterrCtrl character;
    [SerializeField] private Transform lifeIconParent;
    [SerializeField] private int characterLifes;
    [SerializeField] private Text[] texts;
    [SerializeField] private GameObject[] marks;
    [SerializeField] private Image[] BlockGauges;
    public Image[] BlockNeedGauges;
    public Text countN, countR, countB;
    public Core co;
    

    // Start is called before the first frame update
    private void Awake()
    {
        characterLifes = co.CoreLife;
        
    }

    void Start()
    {
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].SetActive(false);
        }
        marks[0].SetActive(true); 
        //co = GameObject.FindGameObjectWithTag("Core").GetComponent<Core>();
        for (int i = 0; i < characterLifes; i++)
        {
            GameObject icon = Instantiate(SystemCtrl.lifeIcon, lifeIconParent.position,Quaternion.identity);
            icon.transform.parent = lifeIconParent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LifeCheck();
        //GameOver();
        BlockNum();
    }

    private void LifeCheck()
    {
        if(characterLifes < co.CoreLife)
        {
            for(int i = 0;i< co.CoreLife - characterLifes; i++)
            {
                GameObject icon = Instantiate(SystemCtrl.lifeIcon, lifeIconParent.position, Quaternion.identity);
                icon.transform.parent = lifeIconParent;
            }
            characterLifes = co.CoreLife;
        }
        else if(characterLifes > co.CoreLife&&co.CoreLife >= 0)
        {
            for(int i=0;i< characterLifes - co.CoreLife; i++)
            {
                Destroy(lifeIconParent.GetChild(i).gameObject);
            }
            characterLifes = co.CoreLife;
        }
    }

    private void GameOver()
    {
        if (co.CoreLife <= 0&&co.CoreLife > -10)
        {
            //LogToCSV.Log_GameOver();
            //SceneManager.LoadScene("GameOver");
            co.CoreLife = -15;
            StartCoroutine(character.Respawn());
        }
        
    }
    
    private void BlockNum()
    {
        //texts[0].text = character.normalNum.ToString()+"/3";
        //texts[1].text = character.rushNum.ToString();
        //texts[2].text = character.bakudanNum.ToString();
        countN.text = "×"+character.normalNum.ToString();
        countR.text = "×" + character.rushNum.ToString();
        countB.text = "×" + character.bakudanNum.ToString();
        BlockGauges[0].fillAmount = character.normalNum / 20f;
        BlockGauges[1].fillAmount = character.bakudanNum / 20f;
        BlockGauges[2].fillAmount = character.rushNum / 20f;

        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].SetActive(false);
        }
        switch (character.selectedBlock)
        {
            case CharacterrCtrl.SelectedBlock.Nomal:
                marks[0].SetActive(true);
                BlockNeedGauges[0].fillAmount = 3 / 20f;
                BlockNeedGauges[1].fillAmount = 0;
                BlockNeedGauges[2].fillAmount = 0;
                break;
            case CharacterrCtrl.SelectedBlock.Tank:
                marks[2].SetActive(true);
                BlockNeedGauges[0].fillAmount = 5 / 20f;
                BlockNeedGauges[1].fillAmount = 1 / 20f;
                BlockNeedGauges[2].fillAmount = 1 / 20f;
                break;
            case CharacterrCtrl.SelectedBlock.Segway:
                marks[1].SetActive(true);
                BlockNeedGauges[0].fillAmount = 5 / 20f;
                BlockNeedGauges[1].fillAmount = 1 / 20f;
                BlockNeedGauges[2].fillAmount = 0;
                break;
        }
    }

    private void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
}

