using Unity.Entities;
using Unity.Transforms;

namespace ComponentsAndTags
{
    public readonly partial struct SunAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRW<SunHealth> _sunHealth;
        private readonly DynamicBuffer<PlanetDamageBufferElement> _sunDamageBuffer;

        public void DamageSun()
        {
            foreach (var sunDamageBufferElement in _sunDamageBuffer)
            {
                _sunHealth.ValueRW.Health -= sunDamageBufferElement.Value;
            }
            _sunDamageBuffer.Clear();
        }
    }
}