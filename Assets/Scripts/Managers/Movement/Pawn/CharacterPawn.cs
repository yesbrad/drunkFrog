﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterPawn : Pawn
{
    private Vector3 movePosition;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void MoveDirection(Vector3 _direction)
    {
        movePosition = _direction * speed * Time.deltaTime;
        controller.SimpleMove(movePosition);
    }
}