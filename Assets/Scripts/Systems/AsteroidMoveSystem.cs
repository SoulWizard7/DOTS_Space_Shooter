using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnAsteroidsSystem))]
    public partial struct AsteroidMoveSystem : ISystem
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
            
            new AsteroidMoveJob
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct AsteroidMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(AsteroidMoveAspect asteroid)
        {
            asteroid.MoveForward(DeltaTime);
        }
    }
}