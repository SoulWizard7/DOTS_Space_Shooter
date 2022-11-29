using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnPlanetsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<OuterSpaceProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var outerSpaceEntity = SystemAPI.GetSingletonEntity<OuterSpaceProperties>();
            var outerSpaceAspect = SystemAPI.GetAspectRW<OuterSpaceAspect>(outerSpaceEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            var spawnPoints = new NativeList<float3>(Allocator.Temp);
            var planets = new NativeList<Entity>(Allocator.Temp);

            for (int i = 0; i < outerSpaceAspect.NumberPlanetsToSpawn; i++)
            {
                var newPlanet = ecb.Instantiate(outerSpaceAspect.PlanetPrefab);
                var newPlanetTransform = outerSpaceAspect.GetRandomPlanetTransform();
                ecb.SetComponent(newPlanet, new LocalToWorldTransform{ Value = newPlanetTransform });
                spawnPoints.Add(newPlanetTransform.Position);
                planets.Add(newPlanet);
            }
            
            outerSpaceAspect.PlanetSpawnPoints = spawnPoints.ToArray(Allocator.Persistent);
            outerSpaceAspect.PlanetList = planets.ToArray(Allocator.Persistent);

            for (int i = 0; i < outerSpaceAspect.NumberAsteroidsToSpawnStart; i++)
            {
                var newAsteroid = ecb.Instantiate(outerSpaceAspect.AsteroidPrefab);
                var newAsteroidTransform = outerSpaceAspect.GetRandomAsteroidTransform();
                ecb.SetComponent(newAsteroid, new LocalToWorldTransform{ Value = newAsteroidTransform });
                ecb.SetComponent(newAsteroid, new PlanetList{Value = outerSpaceAspect.PlanetList});
            }
            
            
            
            ecb.Playback(state.EntityManager);
        }
    }
}