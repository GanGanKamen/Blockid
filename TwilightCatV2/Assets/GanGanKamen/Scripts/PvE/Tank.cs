using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject UI, Turret,bullet,shotpos;
    bool CanRide,Riding;
    GameObject pl,look;
    PlayerPvECtrl pveCtrl;
    public int TankLife = 5;
    bool Reloading;

    [SerializeField] private GameObject soundObj;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("GameManager").GetComponent<DefenceManager>().sikaku;
        look = GameObject.Find("Lookat");
        pl = GameObject.FindGameObjectWithTag("Character");
        pveCtrl = pl.GetComponent<PlayerPvECtrl>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire0")&&CanRide == true)
        {
            if (pveCtrl.RideOnTank == true&&Riding == true)
            {
                SoundManager.StopS(soundObj);
                pveCtrl.Tank = null;
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                pveCtrl.TankLife.SetActive(false);
                rb.mass = 60;
                look.transform.localPosition = new Vector3(0, 0.9f, 0);
                Riding = false;
                pveCtrl.RideOnTank = false;
                transform.parent = null;
                gameObject.layer = LayerMask.NameToLayer("Default");
                pveCtrl.playerCharacter.body.SetActive(true);
                
                pl.transform.eulerAngles = new Vector3 (0, 0, 0);
                pl.transform.Translate(transform.right);
            }
            else if(pveCtrl.RideOnTank == false)
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
                pveCtrl.TankLife.SetActive(true);
                pveCtrl.Tank = gameObject;
                pveCtrl.tankLife();
                look.transform.localPosition = new Vector3(0, 1.5f, 0);
                pveCtrl.RideOnTank = true;
                pl.transform.position = gameObject.transform.position;
                pveCtrl.playerCharacter.body.transform.rotation = transform.rotation;
                Turret.transform.rotation = transform.rotation ;
                gameObject.layer = LayerMask.NameToLayer("Player");
                pveCtrl.isCameraReset = true;
                transform.parent = pl.transform;
                transform.localPosition = Vector3.zero;
                pveCtrl.playerCharacter.body.SetActive(false);
                Riding = true;
                SoundManager.PlayS(soundObj, "SE_Tank_Engine", true);
                //pl.transform.rotation = gameObject.transform.rotation;

            }
            
        }
        if(Riding == true)
        {
            if (Input.GetAxis("Horizontal1") != 0)
            {
                //Turret.transform.Rotate(0, Input.GetAxis("Horizontal1") * Time.deltaTime * 200, 0);
                
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                pl.transform.Translate(Input.GetAxis("Vertical") *7*Time.deltaTime*transform.forward);
            }
            if(Input.GetAxis("Horizontal") != 0)
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime*90, 0);
                pveCtrl.yaw += Input.GetAxis("Horizontal") * Time.deltaTime*90;
            }
            if (Input.GetButtonDown("Fire2")&& Reloading == false)
            {
                GameObject b = Instantiate(bullet, shotpos.transform);
                b.GetComponent<Rigidbody>().AddForce(Turret.transform.forward*(14-(pveCtrl.pitch/10)), ForceMode.Impulse);
                b.transform.parent = null;
                b.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                Reloading = true;
                SoundManager.PlayS(gameObject, "SE_Tank_Shoot");
                StartCoroutine(Reload());
            }

        }
        if (TankLife <= 0)
        {
            if(Riding == true)
            {
                pveCtrl.Tank = null;
                pveCtrl.TankLife.SetActive(false);
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.mass = 60;
                look.transform.localPosition = new Vector3(0, 0.9f, 0);
                Riding = false;
                pveCtrl.RideOnTank = false;
                transform.parent = null;
                gameObject.layer = LayerMask.NameToLayer("Default");
                pveCtrl.playerCharacter.body.SetActive(true);

                pl.transform.eulerAngles = new Vector3(0, 0, 0);
                pl.transform.Translate(transform.right);
            }
            UI.SetActive(false);
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(Riding == false&&collision.gameObject.tag =="Enemy")
        {
            TankLife -= 1;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            UI.SetActive(true);
            CanRide = true;
            SoundManager.PlayS(gameObject, "SE_Tank_Use");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Character"&&pveCtrl.RideOnTank == false)
        {
            UI.SetActive(false);
            CanRide = false;
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        Reloading = false;
        yield break;
    }
}
