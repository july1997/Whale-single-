using Unity.Entities;

[InternalBufferCapacity(8)]
public unsafe struct NeighborsEntityBuffer : IBufferElementData
{
    public Entity Value;
}
