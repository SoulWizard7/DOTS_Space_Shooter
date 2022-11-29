using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;

namespace DefaultNamespace
{
    public class PlanetSpawnerMono : MonoBehaviour
    {
        public int planetsToSpawn;
        public GameObject planetPrefab;
        public int asteroidsToSpawn;
        public GameObject asteroidPrefab;
        public float fieldDimensions;
        public bool useDots = false;
        
        List<GameObject> planets = new List<GameObject>();
        
        
        private const float PLANET_SAFETY_RADIUS = 40;

        private const float SUN_SAFETY_MIN_RADIUS = 300;
        private const float SUN_SAFETY_MAX_RADIUS = 3000;
        
        private const float ASTEROID_SPAWNER_MIN_RADIUS = 2900;
        private const float ASTEROID_SPAWNER_MAX_RADIUS = 3000;

        private void Start()
        {
            if (!useDots)
            {
                Debug.Log("MonoSpawn");
                SpawnPlanets();
            }
        }

        private void SpawnPlanets()
        {
            float3 zero = new float3(0, 0, 0);
            for (int i = 0; i < planetsToSpawn; i++)
            {
                GameObject planet = Instantiate(planetPrefab, GetRandomPlanetPosition(), Quaternion.identity);
                float random = UnityEngine.Random.Range(20, 50);
                planet.transform.localScale = new Vector3(random, random, random);
                planets.Add(planet);
            }
            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                float3 newPos = GetRandomPlanetPosition();
                quaternion rotation = quaternion.RotateY(MathHelpers.GetHeading(newPos, zero));
                Instantiate(asteroidPrefab, newPos, rotation);
            }
        }
        
        private Vector3 GetRandomPlanetPosition()
        {
            Vector3 randomPosition;
            float3 zero = new float3(0, 0, 0);
            do
            {
                randomPosition = new  Vector3(
                    UnityEngine.Random.Range(-fieldDimensions, fieldDimensions), 
                    0, 
                    UnityEngine.Random.Range(-fieldDimensions, fieldDimensions));
            } while (math.distance(zero, randomPosition) <= SUN_SAFETY_MIN_RADIUS || math.distance(zero, randomPosition) >= SUN_SAFETY_MAX_RADIUS);
            
            return randomPosition;
        }
        private Vector3 GetRandomAsteroidPosition()
        {
            Vector3 randomPosition;
            float3 zero = new float3(0, 0, 0);
            do
            {
                randomPosition = new  Vector3(
                    UnityEngine.Random.Range(-fieldDimensions, fieldDimensions), 
                    0, 
                    UnityEngine.Random.Range(-fieldDimensions, fieldDimensions));
            } while (math.distance(zero, randomPosition) <= SUN_SAFETY_MIN_RADIUS || math.distance(zero, randomPosition) >= SUN_SAFETY_MAX_RADIUS || !IsNotCloseToPlanet(randomPosition));
            
            return randomPosition;
        }
        private bool IsNotCloseToPlanet(Vector3 randomPosition)
        {
            for (int i = 0; i < planets.Count; i++)
            {
                if (math.distance(planets[i].transform.position, randomPosition) <= PLANET_SAFETY_RADIUS)
                {
                    return true;
                }
            }
            return false;
        }
    }
}