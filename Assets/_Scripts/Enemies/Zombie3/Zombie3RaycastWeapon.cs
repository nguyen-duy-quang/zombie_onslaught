using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie3RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false; // đơn giản chỉ để check xem đã được bắn hay chưa
    public int fireRate = 25;
    public ParticleSystem[] muzzleFlash;
    public TrailRenderer tracerEffect;

    public Transform raycastOrigin;
    public Transform raycastDestination;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;

    public float damage = 10f;

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    private void FireBullet()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
