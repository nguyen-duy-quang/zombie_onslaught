using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public string weaponName;

    public int ammoCount;
    public int clipSize;

    public Transform raycastOrigin;
    public Transform raycastDestination;
    public GameObject magazine;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifeTime = 3.0f;

    [Header("Rifle Effect")]
    public GameObject goreEffect; // Hiệu ứng máu
    public float giveDamage = 20f;

    public ScriptableObjectWeaponUpgrades weaponUpgrades;
    public SaveWeaponLevel saveWeaponLevel;

    private void Start()
    {
        saveWeaponLevel = GetComponentInParent<SaveWeaponLevel>();
        saveWeaponLevel.LoadData();
        giveDamage = weaponUpgrades.damage;
    }

    Vector3 GetPosition(Bullet bullet)
    {
        // p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }    

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }    
    
    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 2.0f; // ban đầu là 0.0f
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 20.0f) // ban đầu là 0.0f
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }    

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSement(p0, p1, bullet);
        });
    }    

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time > maxLifeTime);
    }

    void RaycastSement(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            if (bullet.tracer != null) // Kiểm tra xem bullet.tracer có tồn tại không
            {
                bullet.tracer.transform.position = hitInfo.point;
                bullet.time = maxLifeTime;
            }

            // Raycast tới Enemy
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();
            Zombie3 zombie3 = hitInfo.transform.GetComponent<Zombie3>();
            Monster1 monster1 = hitInfo.transform.GetComponent<Monster1>();
            Monster2 monster2 = hitInfo.transform.GetComponent<Monster2>();
            Monster3 monster3 = hitInfo.transform.GetComponent<Monster3>();

            if(zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamage);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1.0f);    
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamage);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1.0f);
            }
            else if (zombie3 != null)
            {
                zombie3.zombieHitDamage(giveDamage);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1.0f);
            }
            else if (monster1 != null)
            {
                monster1.monsterHitDamage(giveDamage);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1.0f);
            }
            else if (monster2 != null)
            {
                monster2.monsterHitDamage(giveDamage);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1.0f);
            }
            else if (monster3 != null)
            {
                monster3.monsterHitDamage(giveDamage);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1.0f);
            }
        }
        else
        {
            if (bullet.tracer != null) // Kiểm tra xem bullet.tracer có tồn tại không
            {
                bullet.tracer.transform.position = end;
            }
        }
    }


    private void FireBullet()
    {
        // Nạp đạn khi số đạn <= 0
        if (ammoCount <= 0)
        {
            return;
        }
        ammoCount--;

        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
