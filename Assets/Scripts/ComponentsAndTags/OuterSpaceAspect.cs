using DefaultNamespace;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace ComponentsAndTags
{
    public readonly partial struct OuterSpaceAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<OuterSpaceProperties> _outerSpaceProperties;
        private readonly RefRW<OuterSpaceRandom> _outerSpaceRandom;
        private readonly RefRW<PlanetSpawnPoints> _planetSpawnPoints;
        private readonly RefRW<AsteroidSpawnTimer> _asteroidSpawnTimer;
        
        public int NumberPlanetsToSpawn => _outerSpaceProperties.ValueRO.NumberPlanetsToSpawn;
        public Entity PlanetPrefab => _outerSpaceProperties.ValueRO.PlanetPrefab;

        public NativeArray<float3> PlanetSpawnPoints
        {
            get => _planetSpawnPoints.ValueRO.Value;
            set => _planetSpawnPoints.ValueRW.Value = value;
        }

        private readonly RefRW<PlanetList> _planetList;

        public NativeArray<Entity> PlanetList
        {
            get => _planetList.ValueRO.Value;
            set => _planetList.ValueRW.Value = value;
        }

        public int NumberAsteroidsToSpawnStart => _outerSpaceProperties.ValueRO.NumberAsteroidsToSpawnStart;
        public int NumberAsteroidsToSpawn => _outerSpaceProperties.ValueRO.NumberAsteroidsToSpawn;
        public Entity AsteroidPrefab => _outerSpaceProperties.ValueRO.AsteroidPrefab;

        public UniformScaleTransform GetRandomPlanetTransform()
        {
            return new UniformScaleTransform
            {
                Position = GetRandomPlanetPosition(),
                Rotation = quaternion.identity,
                Scale = GetRandomScale(20, 50f)
            };
        }
        

        private float3 GetRandomPlanetPosition()
        {
            float3 randomPosition;
            
            do
            {
                randomPosition = _outerSpaceRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distance(_transformAspect.Position, randomPosition) <= SUN_SAFETY_MIN_RADIUS || math.distance(_transformAspect.Position, randomPosition) >= SUN_SAFETY_MAX_RADIUS);
            
            return randomPosition;
        }
        
        public UniformScaleTransform GetRandomAsteroidTransform()
        {
            var position = GetRandomAsteroidPosition();
            return new UniformScaleTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(MathHelpers.GetHeading(position, _transformAspect.Position)),
                //Scale = GetRandomScale(1f, 2f)
                Scale = 1f
            };
        }
        
        private float3 GetRandomAsteroidPosition()
        {
            float3 randomPosition;

            do
            {
                randomPosition = _outerSpaceRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distance(_transformAspect.Position, randomPosition) <= SUN_SAFETY_MIN_RADIUS || math.distance(_transformAspect.Position, randomPosition) >= SUN_SAFETY_MAX_RADIUS ||
                     IsNotCloseToPlanet(randomPosition)
                     );
            
            return randomPosition;
        }
        
        public UniformScaleTransform GetRandomAsteroidTransformSpawner()
        {
            var position = GetRandomAsteroidPositionSpawner();
            return new UniformScaleTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(MathHelpers.GetHeading(position, _transformAspect.Position)),
                //Scale = GetRandomScale(1f, 2f)
                Scale = 1f
            };
        }
        
        private float3 GetRandomAsteroidPositionSpawner()
        {
            float3 randomPosition;

            do
            {
                // TODO Figure out better way than to do random position in square and check if its on the edges
                randomPosition = _outerSpaceRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distance(_transformAspect.Position, randomPosition) <= ASTEROID_SPAWNER_MIN_RADIUS || math.distance(_transformAspect.Position, randomPosition) >= ASTEROID_SPAWNER_MAX_RADIUS
                    );
            
            return randomPosition;
        }

        private bool IsNotCloseToPlanet(float3 randomPosition)
        {
            for (int i = 0; i < _planetSpawnPoints.ValueRO.Value.Length; i++)
            {
                if (math.distance(_planetSpawnPoints.ValueRO.Value[i], randomPosition) <= PLANET_SAFETY_RADIUS)
                {
                    return true;
                }
            }
            return false;
        }

        private float3 MinCorner => _transformAspect.Position - HalfDimentions;
        private float3 MaxCorner => _transformAspect.Position + HalfDimentions;

        private float3 HalfDimentions => new()
        {
            x = _outerSpaceProperties.ValueRO.FieldDimensions.x + 0.5f,
            y = 0f,
            z = _outerSpaceProperties.ValueRO.FieldDimensions.x + 0.5f
        };

        private const float PLANET_SAFETY_RADIUS = 40;

        private const float SUN_SAFETY_MIN_RADIUS = 300;
        private const float SUN_SAFETY_MAX_RADIUS = 3000;
        
        private const float ASTEROID_SPAWNER_MIN_RADIUS = 2900;
        private const float ASTEROID_SPAWNER_MAX_RADIUS = 3000;
        

        //random rotation for planets, Useless?
        private quaternion GetRandomRotation() => quaternion.RotateY(_outerSpaceRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        
        //scale planets
        private float GetRandomScale(float min, float max) => _outerSpaceRandom.ValueRW.Value.NextFloat(min, max);
        private float GetRandomScaleAsteroids(float min) => _outerSpaceRandom.ValueRW.Value.NextFloat(min, .1f);

        public float AsteroidSpawnTimer
        {
            get => _asteroidSpawnTimer.ValueRO.Value;
            set => _asteroidSpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnAsteroids => AsteroidSpawnTimer <= 0f;
        public float AsteroidSpawnRate => _outerSpaceProperties.ValueRO.AsteroidSpawnRate;

    }
}