using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [Header("移動先")] public Vector3 destinationPoint;
    private CharacterrCtrl character = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character")
        {
            character = other.gameObject.GetComponent<CharacterrCtrl>();
            StartCoroutine(WarpToPoint());
        }
    }

    private IEnumerator WarpToPoint()
    {
        character.CanNotCtrl();
        character.body.SetActive(false);
        yield return new WaitForSeconds(1f);
        character.transform.position = destinationPoint;
        character.body.SetActive(true);
        character.canCtrl = true;
        yield break;
    }
}
