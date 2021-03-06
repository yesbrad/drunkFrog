﻿using System;
using UnityEngine;

[AddComponentMenu("Item Controller/Debug Item")]
public class DebugItemController : StaticItemController
{
    [Header("Debug Item")]
    [SerializeField]
    private float waitTime = 1;
    
    [SerializeField]
    private MeshRenderer meshRenderer;

    private float currentTime;

    public override void StartInteract(CharacterManager manager, Action taskFinishCallback)
    {
        base.StartInteract(manager, taskFinishCallback);
        HouseOwner?.AddPP(placePoints);
        currentTime = waitTime;

        if (meshRenderer)
        {
            meshRenderer.material.SetColor("_Color", UnityEngine.Random.ColorHSV());
        }
    }

    private void Update()
    {
        if (Characters.Count > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                EndInteract();
            }
        }
    }
}
