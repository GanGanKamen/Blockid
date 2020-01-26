using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerWeapon : MonoBehaviour
{
    Enemy_Gunner eg;
    float Count;
    public float interval = 10;
    public int SummonCount = 4;
    public GameObject Normal, Bomb, shotpos,Normal01;
    GameObject bullet;
    public GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        eg = gameObject.transform.parent.gameObject.GetComponent<Enemy_Gunner>();
        Count = interval;
    }

    // Update is called once per frame
    void Update()
    {
        Count += Time.deltaTime;
        if (eg.isActive == true && eg.isDead == false && Count > interval&&eg.Type == 0)
        {
            shot();
        }
        else if (eg.Type == 1 && eg.isDead == false && Count > interval && eg.isActive == true)
        {
            bullet = Instantiate(Normal01, transform.TransformPoint(0, 0, 1.5f), shotpos.transform.rotation);
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().AddForce((shotpos.transform.position - transform.position) * 5, ForceMode.Impulse);
            Count = 0;
        }
        if(eg.isDead == true)
        {
            Destroy(gameObject);
        }
    }

    void shot()
    {
        
        SoundManager.PlayS(gameObject, "SE_Gunner_Shot");
        if (SummonCount > 0)
        {

            bullet = Instantiate(Normal, transform.TransformPoint(0, 0, 1.5f), shotpos.transform.rotation);
            bullet.transform.parent = null;
            if (bullet.GetComponent<Enemy_Normal>())
            {
                bullet.GetComponent<Enemy_Normal>().gw = this;
            }else if (bullet.GetComponent<Enemy_NewNormal>())
            {
                bullet.GetComponent<Enemy_NewNormal>().gw = this;
                bullet.GetComponent<Enemy_NewNormal>().mode = 1;
            }
            if ((eg.TargetPos - transform.position).magnitude < 20)
            {
                ShotForce(new Vector3(eg.TargetPos.x, eg.TargetPos.y + 1f, eg.TargetPos.z));
                marker.transform.position = new Vector3(eg.TargetPos.x, eg.TargetPos.y, eg.TargetPos.z);
                StartCoroutine(shotMark());
            }
            else
            {
                bullet.GetComponent<Rigidbody>().AddForce((eg.TargetPos - transform.position).normalized * 10,ForceMode.Impulse);
                bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0,4,0), ForceMode.Impulse);
            }
            

            SummonCount--;
            Count = 0;

        }
        else
        {
            bullet = Instantiate(Bomb, transform.TransformPoint(0, 0, 1.5f), shotpos.transform.rotation);
            bullet.transform.parent = null;
            if ((eg.TargetPos - transform.position).magnitude < 20)
            {
                ShotForce(eg.TargetPos);
                StartCoroutine(shotMark());
            }
            else
            {
                bullet.GetComponent<Rigidbody>().AddForce((eg.TargetPos - transform.position).normalized * 10, ForceMode.Impulse);
                bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
            }
            Count = 0;
        }
    }

    public void DamageFromKick(int x)
    {
        if (eg.isDead == true)
        {
            //Instantiate(SystemCtrl.nomalBlock, transform.position, Quaternion.identity);
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(gameObject);
        }
        else if (eg.isInvincible == false)
        {
            eg.EnemyLife -= x;
            eg.StartCoroutine(eg.Invicible());
            eg.BlendShape();
        }
    }

    public void DamageFromShot(int x)
    {
        if (eg.isDead == true)
        {
            //Instantiate(SystemCtrl.nomalBlock, transform.position, Quaternion.identity);
            FXCtrl.FXLoad("BlockChange", transform.position);
            Destroy(gameObject);
        }
        else if (eg.isInvincible == false)
        {
            eg.EnemyLife -= x;
            eg.BlendShape();
        }
    }




    private void ShotForce(Vector3 target)
    {
        float speedVec = ComputeVectorFromAngle(target, 45);
        if (speedVec <= 0.0f)
        {
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, 60, target);
        shotvel(vec);
    }

    private float ComputeVectorFromAngle(Vector3 target, float angle)
    {
        Vector2 startPos = new Vector2(shotpos.transform.position.x, shotpos.transform.position.z);
        Vector2 targetPos = new Vector2(target.x, target.z);
        float distance = Vector2.Distance(targetPos, startPos);

        float x = distance;
        float g = Physics.gravity.y;
        float y0 = shotpos.transform.position.y;
        float y = target.y;
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);

        float v0Square = g * x * x / (2 * cos * cos * (y - y0 - x * tan));
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }
        float v0 = Mathf.Sqrt(v0Square);
        return v0;
    }

    private Vector3 ConvertVectorToVector3(float i_v0, float i_angle, Vector3 i_targetPosition)
    {
        Vector3 startPos = shotpos.transform.position;
        Vector3 targetPos = i_targetPosition;
        startPos.y = 0.0f;
        targetPos.y = 0.0f;
        Vector3 dir = (targetPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = i_v0 * Vector3.right;
        vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;
        return vec;
    }

    private void shotvel(Vector3 i_shootVector)
    {

        Vector3 force = i_shootVector * bullet.GetComponent<Rigidbody>().mass;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    IEnumerator shotMark()
    {
        marker.SetActive(true);
        eg.isActive = false;
        yield return new WaitForSeconds(3f);
        marker.SetActive(false);
        eg.isActive = true;
    }

}
