using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockid : MonoBehaviour
{
    [Header("ビームの弾速")] public float speed;
    [Header("ビーム発射何秒後消える")] public float destoryTime;
    [Header("近距離ダメージ")] public int meleeDamage;
    [Header("遠距離ダメージ")] public int beamDamage;
    public enum Category
    {
        Beam,
        Melee
    }
    public Category category;
    //ブロック化のビームにアタッチする
    public int level;
    public Vector3 direction;
    [SerializeField] private bool ishit;
    public CharacterrCtrl master;

    // Start is called before the first frame update
    void Start()
    {
        if (category == Category.Beam)
        {
            Destroy(gameObject, destoryTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (category)
        {
            case Category.Beam:
                transform.position += transform.forward * speed * Time.deltaTime;
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag ==("Enemy")) //Enemyのコンポーネントによって呼び出す関数を指定する
        {
            SoundManager.PlayS(gameObject, "SE_Hit");
            if (other.gameObject.GetComponent<Enemy_Normal>() != null)
            {
                switch (category)
                {
                    case Category.Melee:
                        other.gameObject.GetComponent<Enemy_Normal>().DamageFromKick(meleeDamage);
                        SoundManager.PlayS(other.gameObject, "SE_Hit");
                        break;
                    case Category.Beam:
                        other.gameObject.GetComponent<Enemy_Normal>().DamageFromShot(5);
                        break;
                }
            }
            if (other.gameObject.GetComponent<Enemy_NewNormal>() != null)
            {
                switch (category)
                {
                    case Category.Melee:
                        other.gameObject.GetComponent<Enemy_NewNormal>().DamageFromKick(meleeDamage);
                        SoundManager.PlayS(other.gameObject, "SE_Hit");
                        break;
                    case Category.Beam:
                        other.gameObject.GetComponent<Enemy_NewNormal>().DamageFromShot(beamDamage);
                        break;
                }
            }
            if (other.gameObject.GetComponent<Enemy_Gunner>() != null)
            {
                switch (category)
                {
                    case Category.Melee:
                        other.gameObject.GetComponent<Enemy_Gunner>().DamageFromKick(meleeDamage);
                        SoundManager.PlayS(other.gameObject, "SE_Hit");
                        break;
                    case Category.Beam:
                        other.gameObject.GetComponent<Enemy_Gunner>().DamageFromShot(beamDamage);
                        break;
                }
            }
            if (other.gameObject.GetComponent<GunnerWeapon>() != null)
            {
                switch (category)
                {
                    case Category.Melee:
                        other.gameObject.GetComponent<GunnerWeapon>().DamageFromKick(meleeDamage);
                        SoundManager.PlayS(other.gameObject, "SE_Hit");
                        break;
                    case Category.Beam:
                        other.gameObject.GetComponent<GunnerWeapon>().DamageFromShot(beamDamage);
                        break;
                }

            }
            if (other.gameObject.GetComponent<Enemy_Bomb>() != null)
            {
                switch (category)
                {
                    case Category.Melee:
                        other.gameObject.GetComponent<Enemy_Bomb>().DamageFromKick(meleeDamage);
                        SoundManager.PlayS(other.gameObject, "SE_Hit");
                        break;
                    case Category.Beam:
                        other.gameObject.GetComponent<Enemy_Bomb>().DamageFromShot(beamDamage);
                        break;
                }
            }
            if(category == Category.Beam)
            {
                Destroy(gameObject);
            }
            
        }
        else if (other.gameObject.GetComponent<BlockCancel>() != null)
        {
            if (other.gameObject.GetComponent<BlockCancel>().categoly == BlockCancel.Categoly.Nomal)
            {
                Instantiate(SystemCtrl.nomalBlock, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy_Normal>() != null)
        {
            switch (category)
            {
                case Category.Melee:
                    break;
                case Category.Beam:
                    other.gameObject.GetComponent<Enemy_Normal>().ExitShot();
                    break;
            }
        }
    }
}
