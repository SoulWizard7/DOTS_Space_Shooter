using Unity.Collections;
using Unity.Entities;

namespace ComponentsAndTags
{
    public struct PlanetList : IComponentData
    {
        public NativeArray<Entity> Value;
    }
}