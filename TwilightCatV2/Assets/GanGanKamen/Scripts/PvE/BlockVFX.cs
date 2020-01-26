using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ブロックが集まってくる時の演出エフェクト
public class BlockVFX : MonoBehaviour
{
    //public Character master;
    public Transform target;
    public MeshRenderer meshRenderer;
    private Vector3 destination;
    private float disOfMaster;
    private Vector3 startPos;
    // Start is called before the first frame update
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        startPos = transform.position;
        destination = target.position + target.up;
        SoundManager.PlayS(gameObject);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        disOfMaster = Mathf.Abs(target.position.magnitude - startPos.magnitude);
        transform.position = Vector3.Lerp(transform.position,destination ,disOfMaster *Time.deltaTime * 10);
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, Mathf.Lerp(1f, 0.2f, Time.deltaTime * 10));
        transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 0.4f,Time.deltaTime * 10);
    }
}
