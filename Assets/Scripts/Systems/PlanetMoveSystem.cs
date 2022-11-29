using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(AsteroidMoveSystem))]
    public partial struct PlanetMoveSystem : ISystem
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
            new PlanetMoveJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct PlanetMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(PlanetMoveAspect planet)
        {
            planet.Orbit(DeltaTime);
        }
    }
}