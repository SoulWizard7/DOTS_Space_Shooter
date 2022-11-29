using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{ 
    public struct OuterSpaceProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberPlanetsToSpawn;
        public Entity PlanetPrefab;

        public int NumberAsteroidsToSpawnStart;
        public Entity AsteroidPrefab;
        public float AsteroidSpawnRate;
        public int NumberAsteroidsToSpawn;
    }

    public struct AsteroidSpawnTimer : IComponentData
    {
        public float Value;
    }
    
    public struct SunTag : IComponentData {}

    public struct SunHealth : IComponentData
    {
        public int Health;
        public int MaxHealth;
    }
}