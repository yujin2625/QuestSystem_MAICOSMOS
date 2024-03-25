using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerController PlayerController;
    private void Start()
    {
        PlayerController = PlayerManager.Instance.PlayerController;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        PlayerController.MoveComposite = context.ReadValue<Vector2>();

    }
    //public void OnLook(InputAction.CallbackContext context)
    //{
    //        PlayerController.LookComposite = context.ReadValue<Vector2>();
    //}

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        PlayerController.Jump();
    }



}
