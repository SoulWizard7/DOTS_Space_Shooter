using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnAsteroidsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnAsteroidJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }
    
    [BurstCompile]
    public partial struct SpawnAsteroidJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        private void Execute(OuterSpaceAspect outerSpace)
        {
            outerSpace.AsteroidSpawnTimer -= DeltaTime;
            if(!outerSpace.TimeToSpawnAsteroids) return;

            outerSpace.AsteroidSpawnTimer = outerSpace.AsteroidSpawnRate; //Reset timer

            for (int i = 0; i < outerSpace.NumberAsteroidsToSpawn; i++)
            {
                var newAsteroid = ECB.Instantiate(outerSpace.AsteroidPrefab);
                var newAsteroidTransform = outerSpace.GetRandomAsteroidTransformSpawner();
                ECB.SetComponent(newAsteroid, new LocalToWorldTransform{ Value = newAsteroidTransform});
            }
        }
    }
}