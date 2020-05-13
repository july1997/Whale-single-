using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;

//debug.log用
using UnityEngine;

public class PlayerCommponentSystem : ComponentSystem
{
    private float3 posVector;
    private float angleH = 0.0f;
    private float angleV = 0.0f;
    private float speed = 2.0f;

    protected override void OnCreate()
    {
        // 向いてる方向
        posVector = new float3 (1, 0, 0);
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation pos, ref Rotation rot, ref PlayerCommandData playercommanddata) =>
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            angleH += horizontal;
            angleV += vertical;

            // 回転行列を求める
            var rotation = Quaternion.AngleAxis(angleH, new float3(0, 1, 0)) * Quaternion.AngleAxis(angleV, new float3(0, 0, -1));
            var dir = rotation * posVector;

            pos = new Translation { Value = pos.Value + new float3(-dir) * speed * Time.DeltaTime}; 
            rot = new Rotation { Value = rotation };
        });
    }
}