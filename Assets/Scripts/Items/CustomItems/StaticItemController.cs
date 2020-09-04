using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticItemController : ItemController
{
    public int placePoints;
    public float waitTime = 1;
    public MeshRenderer renderer;

    float currentTime;

    public override void Init(Item newItem, CharacterManager manager, bool boxed)
    {
        base.Init(newItem, manager, boxed);

        owner?.HouseManager.AddHP(placePoints);
    }

    public override void Interact(Pawn pawn, Action taskFinishCallback)
    {
        base.Interact(pawn, taskFinishCallback);
        owner.HouseManager.AddHP(placePoints);
        currentTime = waitTime;

        if (renderer)
        {
            renderer.material.SetColor("_Color", UnityEngine.Random.ColorHSV());
        }
    }

    private void Update()
    {
        if (occupied)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                EndTask();
            }
        }
    }

    public override void EndTask()
    {
        base.EndTask();
    }
}
