using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponTypes weaponType;
    public Camera playerCam;

    [SerializeField]
    private LayerMask layerMask;

    [Header("Bullet")]
    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public int curBulletAmount;
    public int maxBullets;
    public float maxBulletSpread = 0.02f;
    public float bulletSpeed = 40;
    public float weaponDamage = 1;
    public float extraDamage = 0, extraSpeed = 0;

    [Header("Timers")]
    public float reloadTime;
    public float fireInterval;

    private bool canFire = true;
    private bool isReloading;
    private float timeToFire;
    private float reloadEndTime;
    private Vector3 aimPoint;

    public GameObject currentTarget;

    [HideInInspector]
    public ElementMain elementMain;

    public enum WeaponTypes
    {
        Rifle,
        Shotgun,
        RPG
    }

    void Start()
    {
        elementMain = GetComponent<ElementMain>();
        curBulletAmount = maxBullets;
    }

    void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 50, Color.green);

        if (Input.GetButton("Fire1") && Time.time >= timeToFire && canFire)
        {
            timeToFire = Time.time + fireInterval;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            Reload();
        }

        if (isReloading && Time.time >= reloadEndTime)
        {
            canFire = true;
            isReloading = false;
            curBulletAmount = maxBullets;
        }
    }

    /// <summary>
    /// Set the element of a weapon
    /// </summary>
    /// <param name="elementType">The weapon you want to change the element on</param>
    public void SetWeaponElement(ElementMain.ElementType elementType)
    {
        switch (elementType)
        {
            case ElementMain.ElementType.None:
                elementMain.currentType = ElementMain.ElementType.None;
                break;

            case ElementMain.ElementType.Water:
                elementMain.currentType = ElementMain.ElementType.Water;
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "ChangeElementWater");
                break;

            case ElementMain.ElementType.Fire:
                elementMain.currentType = ElementMain.ElementType.Fire;
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "ChangeElementFire");
                break;

            case ElementMain.ElementType.Air:
                elementMain.currentType = ElementMain.ElementType.Air;
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "ChangeElementAir");
                break;
        }
    }

    public void Shoot()
    {
        if (curBulletAmount <= 0)
        {
            canFire = false;
            Reload();
            return;
        }
        else
        {
            curBulletAmount -= 1;
        }

        ShootSound();

        Transform bullet;
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, float.MaxValue, layerMask))
        {
            aimPoint = hit.point;
        }
        Vector3 aimDir = (aimPoint - spawnBulletPos.position).normalized;

        if (weaponType == WeaponTypes.Shotgun)
        {
            int oneUnitRange = 0;
            int twoUnitRange = 0;
            int outsideUnitRange = 0;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= 1)
                {
                    oneUnitRange++;
                }
                else if (distance <= 2)
                {
                    twoUnitRange++;
                }
                else
                {
                    outsideUnitRange++;
                }
            }

            Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
            {
                name = "ShotShotgun",
                data = new Dictionary<string, object>()
                {
                    { "EnemiesWithinOneUnit", oneUnitRange },
                    { "EnemiesWithinTwoUnits", twoUnitRange },
                    { "EnemiesOutsideTwoUnits", outsideUnitRange }
                }
            });

            for (int i = 0; i < 6; i++)
            {
                bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                BulletProjectile bulletProjectile = bullet.GetComponent<BulletProjectile>();
                bulletProjectile.SetVelocity((bullet.forward + new Vector3(Random.Range(-maxBulletSpread, maxBulletSpread), Random.Range(-maxBulletSpread, maxBulletSpread), Random.Range(-maxBulletSpread, maxBulletSpread))) * bulletSpeed);
                bulletProjectile.SetElementType(elementMain.currentType);
                bulletProjectile.SetDamage(weaponDamage + extraDamage);
            }
        }
        else
        {
            bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            BulletProjectile bulletProjectile = bullet.GetComponent<BulletProjectile>();
            bulletProjectile.SetVelocity(bullet.forward * bulletSpeed);
            bulletProjectile.SetElementType(elementMain.currentType);
            bulletProjectile.SetDamage(weaponDamage + extraDamage);
        }

        if (weaponType == WeaponTypes.Rifle)
        {
            List<float> distances = new List<float>();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                distances.Add(Vector3.Distance(transform.position, enemy.transform.position));
            }

            Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
            {
                name = "ShotRifle",
                data = new Dictionary<string, object>()
                    {
                        { "AimedEnemyDistance", hit.transform.CompareTag("Enemy") ? Vector3.Distance(transform.position, aimPoint) : -1 },
                        { "AllEnemyDistances", distances }
                    }
            });
        }

        if (weaponType == WeaponTypes.RPG)
        {
            Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
            {
                name = "RPGShotPlayerPos",
                data = new Dictionary<string, object>()
                    {
                        {"Player position", transform.position}
                    }
            });
        }
    }

    private void ShootSound()
    {
        switch (weaponType)
        {
            case WeaponTypes.Rifle:
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "RifleShoot");
                break;
            case WeaponTypes.Shotgun:
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "ShotgunReload");
                break;
            case WeaponTypes.RPG:
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "RPGReload");
                break;
        }
    }

    private void ReloadSound()
    {
        switch (weaponType)
        {
            case WeaponTypes.Rifle:
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "RifleReload");
                break;
            case WeaponTypes.Shotgun:
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "ShotgunReload");
                break;
            case WeaponTypes.RPG:
                AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "RPGReload");
                break;
            default:
                break;
        }
    }

    public virtual void Reload()
    {
        ReloadSound();
        {
            isReloading = true;
            canFire = false;
            reloadEndTime = Time.time + reloadTime;
        }
    }
}
