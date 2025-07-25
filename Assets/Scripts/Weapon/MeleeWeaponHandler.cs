using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxsize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        collideBoxsize = collideBoxsize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxsize.x, collideBoxsize, 0, Vector2.zero, 0, target);

        if (hit.collider != null)
        {

            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                if (IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }

    public override void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
