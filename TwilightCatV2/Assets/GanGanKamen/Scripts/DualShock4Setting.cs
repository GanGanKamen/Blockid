using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DualShock4Setting : MonoBehaviour
{
    [Range(1, 4)]public int player1Num;
    [Range(1, 4)]public int player2Num;
    public Text num1,num2;
    public Text message;
    public GameObject controller1, controller2;
    public GameObject check1, check2;
    public bool player1OK, player2OK;
    public bool canIntoNextScene;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(Check());
    }

    // Update is called once per frame
    void Update()
    {
        if (Dualshock4.CircleDown(player1Num))
        {
            player1OK = true;
            controller1.SetActive(false);
            check1.SetActive(true);
        }
        if (Dualshock4.CircleDown(player2Num))
        {
            player2OK = true;
            controller2.SetActive(false);
            check2.SetActive(true);
        }
        if (Dualshock4.OptionsDown(0) && canIntoNextScene == true)
        {
            SceneManager.LoadScene("Double");
        }
        num1.text = player1Num.ToString() + "P";
        num2.text = player2Num.ToString() + "P";
    }

    private IEnumerator Check()
    {
        message.text = "○ボタンを押してください！";
        while(player1OK == false || player2OK == false)
        {
            yield return null;
        }
        canIntoNextScene = true;
        message.text = "Optionsボタンではじめる！";
        yield break;
    }

    public void OnClick(int playerNum) //ナンバーの文字クリックしたらナンバリングが変化
    {
        switch (playerNum)
        {
            case 1:
                if(player1Num < 4)
                {
                    player1Num += 1;
                }
                else
                {
                    player1Num = 1;
                }
                break;
            case 2:
                if (player2Num < 4)
                {
                    player2Num += 1;
                }
                else
                {
                    player2Num = 1;
                }
                break;
            default:
                break;
        }
    }
}
