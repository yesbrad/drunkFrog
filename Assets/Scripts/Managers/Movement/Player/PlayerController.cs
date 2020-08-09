using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterPawn
{
    public NewControls controls;

    private void Awake()
    {
        controls = new NewControls();

        controls.Player.Move.performed += ctx =>
        {
            float ye = ctx.ReadValue<float>();
            Debug.Log(ye);
            MoveDirection(new Vector3(ye,0,0));
      
        };
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}
