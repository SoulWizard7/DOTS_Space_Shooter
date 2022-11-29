using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(PlanetMoveSystem))]
    public partial struct AsteroidCollisionSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var sunEntitiy = SystemAPI.GetSingletonEntity<SunTag>();
            
            new AsteroidCollisionJob()
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                SunEntity = sunEntitiy
            }.ScheduleParallel();
        }
    }
    [BurstCompile]
    public partial struct AsteroidCollisionJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public Entity SunEntity;

        [BurstCompile]
        private void Execute(AsteroidMoveAspect asteroid, [EntityInQueryIndex] int sortKey)
        {
            if (asteroid.IsHittingSun())
            {
                asteroid.DamageSun(ECB, sortKey, SunEntity);
            }
/*
            if (asteroid.debugPlanetList() > 10)
            {
                Debug.Log("List over 10");
            }

            if (asteroid.debugPlanetList() < 10)
            {
                Debug.Log("planetlist failed");
            }*/
        }
    }
}