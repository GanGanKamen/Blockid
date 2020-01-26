using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayS(gameObject,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")|| Input.GetButtonDown("Fire2"))
        {
            SoundManager.PlayS(gameObject, 1);
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
