using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct OuterSpaceRandom : IComponentData
    {
        public Random Value;
    }
}