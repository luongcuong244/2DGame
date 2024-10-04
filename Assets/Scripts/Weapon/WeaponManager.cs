using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(BaseWeapon weapon)
    {
        // check if is instance of ProjectileWeapon
        if (weapon is ProjectileWeapon)
        {
            StartCoroutine(SpawnProjectileWeapon((ProjectileWeapon) weapon));
        }
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
