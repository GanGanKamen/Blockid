using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatPoint : MonoBehaviour
{
    public int blockNum;
    [SerializeField] private GameObject windowImage;
    [SerializeField] private TextMesh text;
    private CharacterrCtrl characterr;
    // Start is called before the first frame update
    void Start()
    {
        characterr = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterrCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
        TextChange();
    }

    private void LookAtCamera()
    {
        Vector3 cameraP = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        windowImage.transform.LookAt(cameraP);
    }

    private void TextChange()
    {
        if(characterr.normalNum < blockNum)
        {
            text.text = "×" + blockNum.ToString();
        }
        else
        {
            text.text = "△ボタン";
        }
    }
}
