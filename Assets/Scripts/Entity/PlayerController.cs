using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera _camera;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        _camera = Camera.main;
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    protected override void HandleAction()
    {

    }

    void OnMove(InputValue inputValue)
    {
        movemomentDirection = inputValue.Get<Vector2>();
        movemomentDirection = movemomentDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    void OnFire(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        isAttacking = inputValue.isPressed;
    }
}