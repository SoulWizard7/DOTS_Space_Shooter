using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ComponentsAndTags
{
    public readonly partial struct AsteroidMoveAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<AsteroidMoveSpeed> _asteroidMoveSpeed;
        private readonly RefRO<AsteroidHeading> _asteroidHeading;

        private float MoveSpeed => _asteroidMoveSpeed.ValueRO.Value;
        private float Heading => _asteroidHeading.ValueRO.Value;

        private readonly RefRW<PlanetList> _planetList;
        
        public NativeArray<Entity> PlanetList
        {
            get => _planetList.ValueRO.Value;
            set => _planetList.ValueRW.Value = value;
        }
        

        public void MoveForward(float deltaTime)
        {
            _transformAspect.Position += _transformAspect.Forward * _asteroidMoveSpeed.ValueRO.Value * deltaTime;
        }

        public bool IsHittingSun()
        {
            return math.distancesq(float3.zero, _transformAspect.Position) <= SUN_RADIUS;
        }
        
        private const float SUN_RADIUS = 18000;

        public bool IsHittingPlanet(float3 planetPosition, float planetRadius)
        {
            
            return math.distancesq(planetPosition, _transformAspect.Position) <= planetRadius;
        }

        public int debugPlanetList()
        {
            return PlanetList.Length;
            //return _planetList.ValueRO.Value.Length; no work
        }

        public void DamageSun(EntityCommandBuffer.ParallelWriter ecb, int sortKey, Entity sun)
        {
            var damage = new PlanetDamageBufferElement { Value = 1 };
            ecb.AppendToBuffer(sortKey, sun, damage);
        }
    }
}