using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct PlanetSpawnPoints : IComponentData
    {
        public NativeArray<float3> Value;
    }
}