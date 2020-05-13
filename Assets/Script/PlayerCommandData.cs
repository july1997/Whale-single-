using System;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PlayerCommandData : IComponentData
{
    public float3 value;
}

