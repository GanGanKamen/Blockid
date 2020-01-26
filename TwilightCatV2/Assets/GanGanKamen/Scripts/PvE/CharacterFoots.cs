using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFoots : MonoBehaviour
{
    public bool onGround; //地面判定用コライダーにアタッチ
    public int createNum;
    public bool canCreate;
    public Vector3 pointPos;
    // Start is called before the first frame update
    void Start()
    {
        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Ground"|| other.gameObject.tag == "Block")&&onGround == false)
        {
            onGround = true;
            SoundManager.PlayS(gameObject);
        }
        if(other.gameObject.tag == "CreatePoint")
        {
            CreatPoint point = other.gameObject.GetComponent<CreatPoint>();
            createNum = point.blockNum;
            pointPos = point.transform.position;
            canCreate = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Block")
        {
            onGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Block")
        {
            onGround = false;
        }
        if (other.gameObject.tag == "CreatePoint")
        {
            canCreate = false;
        }
    }
}
