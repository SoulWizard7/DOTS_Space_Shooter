using DefaultNamespace;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ComponentsAndTags
{
    public readonly partial struct PlanetMoveAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<PlanetProperties> _planetProperties;
        private readonly RefRW<PlanetDebug> _planetDebug;

        private float OrbitSpeed => _planetProperties.ValueRO.SunOrbitSpeed;

        public void Orbit(float deltaTime)
        {
            _transformAspect.Rotation = quaternion.RotateY(MathHelpers.GetHeading(_transformAspect.Position, float3.zero));
            _transformAspect.Position += _transformAspect.Right * OrbitSpeed * deltaTime;

            _planetDebug.ValueRW.CurDistanceFromSun = math.distance(_transformAspect.Position, float3.zero);
        }
    }
}