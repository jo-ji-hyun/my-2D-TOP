using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movemomentDirection = Vector2.zero;
    public Vector2 MovemomentDirection { get { return movemomentDirection; } }

    protected Vector2 lookDirection = Vector2.zero;

    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected AnimationHandle animationHandle; //애니메이션 참조
    protected Stat statHandle; //스텟 참조

    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandle = GetComponent<AnimationHandle>();
        statHandle = GetComponent<Stat>();

        if (WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
            Debug.Log("무기 있어용");
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
            Debug.Log("무기 없어용");
        }

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();

    }

    protected virtual void FixedUpdate()
    {
        Movement(movemomentDirection);
        if(knockbackDuration > 0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction) //이동
    {
        direction = direction * statHandle.Speed;
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandle.Move(direction); //애니메이션 작동

    }

    private void Rotate(Vector2 direction) //회전
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
            //Debug.Log($"회전값 {rotZ}");
        }

        weaponHandler?.Rotate(isLeft);

    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
        //Debug.Log("공격 딜레이");
    }


    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}
