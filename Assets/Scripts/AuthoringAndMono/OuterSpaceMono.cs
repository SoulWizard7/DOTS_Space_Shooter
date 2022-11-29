
using ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace AuthoringAndMono
{
    public class OuterSpaceMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberPlanetsToSpawn;
        public GameObject PlanetPrefab;
        
        public int NumberAsteroidsToSpawnStart;
        public GameObject AsteroidPrefab;
        public float AsteroidSpawnRate;
        public int NumberAsteroidsSpawn;
        
        public uint RandomSeed;
    }

    public class OuterSpaceBaker : Baker<OuterSpaceMono>
    {
        public override void Bake(OuterSpaceMono authoring)
        {
            AddComponent(new OuterSpaceProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberPlanetsToSpawn = authoring.NumberPlanetsToSpawn,
                PlanetPrefab = GetEntity(authoring.PlanetPrefab),
                
                NumberAsteroidsToSpawnStart = authoring.NumberAsteroidsToSpawnStart,
                AsteroidPrefab = GetEntity(authoring.AsteroidPrefab),
                AsteroidSpawnRate = authoring.AsteroidSpawnRate,
                NumberAsteroidsToSpawn = authoring.NumberAsteroidsSpawn
            });
            AddComponent(new OuterSpaceRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<PlanetSpawnPoints>();
            AddComponent<AsteroidSpawnTimer>();
            AddComponent<PlanetList>();
        }
    }
}