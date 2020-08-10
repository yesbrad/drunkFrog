using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterPawn
{
    public NewControls controls;

    private Vector3 inputDirection;

    private void Awake()
    {
        controls = new NewControls();
    }

	private void Update () 
	{
        UpdateInput();
	}

    private void UpdateInput ()
    {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        inputDirection.x = input.x;
        inputDirection.z = input.y;
        MoveDirection(inputDirection);
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}
