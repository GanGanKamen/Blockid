using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCheck : MonoBehaviour
{
    [SerializeField] private CharacterrCtrl character;
    public Vector3 startPos;
    public bool isCollide;
    private BoxCollider collider;
    int countobj;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(character.selectedBlock == CharacterrCtrl.SelectedBlock.Nomal)
        {
            collider.size = new Vector3(1.5f, 1, 1);
        }
        else
        {
            collider.size = Vector3.one;
        }*/
        
    }

    private void OnTriggerStay(Collider other)
    {
        /*if(other.gameObject.layer != 2||other.gameObject.tag =="Enemy" || other.gameObject.tag == "Block")
        {
            isCollide = true;
        }*/
        if(other.gameObject.tag == "Core")
        {
            isCollide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Core")
        {
            isCollide = false;
        }
    }
}
