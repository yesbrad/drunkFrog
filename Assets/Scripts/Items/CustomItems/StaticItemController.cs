﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticItemController : ItemController
{
    public int placePoints;
    public float waitTime = 1;

    float currentTime;

    public override void Init(Item newItem, CharacterManager manager)
    {
        base.Init(newItem, manager);
    }

    public override void Interact(Pawn pawn, Action taskFinishCallback)
    {
        base.Interact(pawn, taskFinishCallback);
        owner.HouseManager.AddHP(placePoints);
        currentTime = waitTime;
    }

    private void Update()
    {
        if (occupied)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                Debug.Log("YEEET");
                EndTask();
            }
        }
    }

    public override void EndTask()
    {
        base.EndTask();
    }
}
