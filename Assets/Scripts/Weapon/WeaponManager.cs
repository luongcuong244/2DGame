using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon defaultWeapon;
    private List<Coroutine> weaponCoroutines = new List<Coroutine>();

    private void Start()
    {
        if (defaultWeapon != null)
        {
            AddWeapon(defaultWeapon);
        }
    }

    public void AddWeapon(BaseWeapon weapon)
    {
        // check if is instance of ProjectileWeapon
        if (weapon is ProjectileWeapon)
        {
            weaponCoroutines.Add(StartCoroutine(SpawnProjectileWeapon((ProjectileWeapon)weapon)));
        }
    }

    public void ClearWeapon()
    {
        foreach (Coroutine coroutine in weaponCoroutines)
        {
            StopCoroutine(coroutine);
        }
        weaponCoroutines.Clear();
    }

    private IEnumerator SpawnProjectileWeapon(ProjectileWeapon weapon)
    {
        while(true)
        {
            // Instantiate a new projectile weapon
            ProjectileWeapon newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(weapon.spawnInterval);
        }
    }
}
