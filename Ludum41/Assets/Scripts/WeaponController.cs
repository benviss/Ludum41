using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{

    public Transform weaponHold;
    public Weapon[] allWeapons;
    public Weapon equippedWeapon;

    void Start()
    {
        //Equipweapon(2);
    }

    public void Equipweapon(Weapon weaponToEquip)
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon.gameObject);
        }
        equippedWeapon = Instantiate(weaponToEquip, weaponHold.position, weaponHold.rotation) as Weapon;
        equippedWeapon.transform.parent = weaponHold;
    }

    public void Equipweapon(int weaponIndex)
    {
        Equipweapon(allWeapons[weaponIndex]);
    }

    public float weaponHeight
    {
        get
        {
            return weaponHold.position.y;
        }
    }

    public void Attack()
    {
        equippedWeapon.Attack();
    }


    public void Aim(Vector3 aimPoint)
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Aim(aimPoint);
        }
    }

    public void Reload()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Reload();
        }
    }

    public float GetWeaponRange()
    {
        return equippedWeapon.range;
    } 

}