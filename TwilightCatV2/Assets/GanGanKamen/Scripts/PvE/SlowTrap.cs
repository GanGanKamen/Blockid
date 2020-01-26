using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    public bool muteki,isAlive = true;
    public int Life = 4;
    public float SpeedMultiple = 0.2f;
    public SlowTrap2 st;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayS(st.gameObject, "SE_Spark_Engine", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Life <= 0&&isAlive == true)
        {
            StartCoroutine(Dest());
        }
    }

    
    private void OnCollisionStay(Collision collision)
    {
        if (muteki == false && collision.gameObject.tag == "Enemy")
        {
            Life--;
            StartCoroutine(Muteki());
        }
    }

    IEnumerator Muteki()
    {
        muteki = true;
        yield return new WaitForSeconds(2f);
        muteki = false;
        yield break;

    }
    IEnumerator Dest()
    {
        isAlive = false;
        SoundManager.PlayS(gameObject, "SE_Spark_Destory");
        SoundManager.StopS(st.gameObject);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        yield break;
    }
}
