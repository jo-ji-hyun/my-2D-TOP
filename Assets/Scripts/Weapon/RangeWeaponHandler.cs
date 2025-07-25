using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor
    {
        get { return projectileColor; }
        private set { projectileColor = value; } // 이 줄을 추가하여 내부에서 설정 가능하게 함
                                                 // 만약 외부에서도 설정해야 한다면 'public set'으로 변경
    }

    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;

        //if (projectileManager == null)
        //{
        //    Debug.Log("projectileManager 할당이 안됨");
        //}
        //else
        //{
        //    Debug.Log("매니저 들어옴");
        //}
    }

    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngel; // 각도
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; // 갯수

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace; //최소 각도

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread); //화살 퍼짐
            angle += randomSpread;

            //Debug.Log("화살 생성 전");

            CreateProjectile(Controller.LookDirection, angle);
        }
       // Debug.Log("공격");
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
    this,
    projectileSpawnPosition.position,
    RotateVector2(_lookDirection, angle));
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        Debug.Log($"{v}");
        return Quaternion.Euler(0, 0, degree) * v;
    }

}
