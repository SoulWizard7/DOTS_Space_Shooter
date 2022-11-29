using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

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
            new AsteroidCollisionJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
    [BurstCompile]
    public partial struct AsteroidCollisionJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(AsteroidMoveAspect asteroid)
        {
            if (asteroid.IsHittingSun())
            {
                
            }
        }
    }
}