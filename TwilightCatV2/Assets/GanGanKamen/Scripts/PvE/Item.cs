using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Category
    {
        hp,
        beamLevel
    }
    public Category category;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 cameraP = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        transform.LookAt(cameraP);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character")
        {
            switch (category)
            {
                case Category.beamLevel:
                    BeamLevelUp(other.gameObject.GetComponent<CharacterrCtrl>());
                    break;
                case Category.hp:
                    HpRecovery(other.gameObject.GetComponent<CharacterrCtrl>());
                    break;
            }
        }
    }

    private void HpRecovery(CharacterrCtrl characterr)
    {
        if(characterr.hp < 7)
        {
            characterr.hp = 7;
            Destroy(gameObject);
        }
    }

    private void BeamLevelUp(CharacterrCtrl characterr)
    {
        if(characterr.beamLevel < 3)
        {
            characterr.beamLevel += 1;
            Destroy(gameObject);
        }
    }
}
