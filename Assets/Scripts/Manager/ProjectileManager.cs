using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance; //ΩÃ±€≈Ê √≥∏Æ
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] ProjectilePrefabs;

    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this; //ΩÃ±€≈Ê º±æ
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
       // Debug.Log("»≠ªÏ ª˝º∫");
        GameObject origin = ProjectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this);
    }
    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler) //¿Ã∆Â∆Æ º“»Ø
    {
        impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
        impactParticleSystem.Play();
    }
}
