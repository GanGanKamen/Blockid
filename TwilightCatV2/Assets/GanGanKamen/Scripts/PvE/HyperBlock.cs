using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperBlock : MonoBehaviour
{
    public enum Status
    {
        Nomal,
        Rush,
        Ganner,
        Bakudan,
    }
    public Status status;
    public CharacterrCtrl master;
    public bool isMove,canNotCollect;
    public int blockLife = 3;
    float MaxLife;
    private SkinnedMeshRenderer skinned;
    public bool isCancel; //解除始まったかどうか
    private float blendCount;
    // Start is called before the first frame update
    private void Awake()
    {
        skinned = GetComponent<SkinnedMeshRenderer>();
        MaxLife = blockLife;
    }

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterrCtrl>();
        blendCount = 100;
        if (canNotCollect == false)
        {
            SoundManager.PlayS(gameObject, "SE_Blocked");
            StartCoroutine(collect());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove == true && isCancel == false)
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.position = Vector3.Lerp(transform.position, master.transform.position + master.transform.up * 2, Time.deltaTime * 10);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.2f, 0.2f, 0.2f), Time.deltaTime * 10);
        }
        CancelToBall();
    }

    public IEnumerator BackToMaster() //回収される
    {
        isMove = true;
        yield return new WaitForSeconds(0.5f);
        switch (status)
        {
            case Status.Nomal:
                master.normalNum += 1;
                break;
            case Status.Rush:
                master.rushNum += 1;
                break;
            case Status.Bakudan:
                master.bakudanNum += 1;
                break;
            case Status.Ganner:
                master.gannerNum += 1;
                break;
        }
        Destroy(gameObject);
    }

    private void CancelToBall() //ブロック解除される
    {
        if (isCancel == true)
        {
            blendCount -= 500f * Time.deltaTime;
            skinned.SetBlendShapeWeight(0, blendCount);
        }

        if (blendCount <= 0)
        {
            switch (status)
            {
                case Status.Nomal:
                    Instantiate(SystemCtrl.nomalCancel, transform.position, transform.rotation);
                    break;
                case Status.Bakudan:
                    Instantiate(SystemCtrl.bakudanCancel, transform.position, transform.rotation);
                    break;
                case Status.Rush:
                    Instantiate(SystemCtrl.rushCancel, transform.position, transform.rotation);
                    break;
            }
            Destroy(gameObject);
        }

    }

    public void BlendShape()
    {
        skinned.SetBlendShapeWeight(0, 100 * (blockLife/ MaxLife));
        if(blockLife <= 0)
        {
            isCancel = true;
            blendCount = 0;
        }
    }
    IEnumerator collect()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(BackToMaster());
        yield break;
    }
}
