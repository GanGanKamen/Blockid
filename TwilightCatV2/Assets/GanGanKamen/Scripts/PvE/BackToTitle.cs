using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour
{
    bool isChange;
    // Start is called before the first frame update
    void Start()
    {
        
        SoundManager.PlayS(gameObject, "SE_UI_Clear",0);
        SoundManager.PlayS(gameObject, "BGM_Thanks", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange == false)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            {
                StartCoroutine(titleReturn());
            }
        }
    }
    public void back()
    {
        if (isChange == false)
        {
            StartCoroutine(titleReturn());
        }
    }
    IEnumerator titleReturn()
    {
        isChange = true;
        SoundManager.PlayS(gameObject, "SE_UI_Comfirm", 2);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("PvETitle");
        yield break;
    }
}
