using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvESystemCtrl : MonoBehaviour
{
    public int oneTime;
    public float posXMin, posXMax;
    public float posZMin, posZMax;
    public CharacterrCtrl character;
    // Start is called before the first frame update
    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterrCtrl>();
    }

    void Start()
    {
        //WaveLoop();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    private void WaveLoop()
    {
        StartCoroutine(WaitForNext());
    }

    private IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(oneTime);
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 20)
        {
            int randomX = Random.Range(6, 20);
            for (int i = 0; i < randomX; i++)
            {
                float randomPosX = Random.Range(posXMin, posXMax);
                float randomPosZ = Random.Range(posZMin, posZMax);
                //GameObject enemy = Instantiate(SystemCtrl.enemy1, new Vector3(randomPosX, 1.5f, randomPosZ), Quaternion.identity);
            }
        }
        WaveLoop();
    }

    private void GameOver()
    {
        if(character.hp <= 0)
        {
            SceneManager.LoadScene("Result");
        }
    }
}
