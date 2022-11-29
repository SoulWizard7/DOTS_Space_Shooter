using Unity.Entities;

namespace ComponentsAndTags
{
    public struct AsteroidMoveSpeed : IComponentData
    {
        public float Value;
    }

    public struct AsteroidHeading : IComponentData
    {
        public float Value;
    }
}