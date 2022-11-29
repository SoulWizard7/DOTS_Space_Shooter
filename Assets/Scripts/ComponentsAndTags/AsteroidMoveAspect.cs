using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

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
    }
}