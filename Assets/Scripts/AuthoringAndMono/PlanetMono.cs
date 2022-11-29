using ComponentsAndTags;
using UnityEngine;
using Unity.Entities;

namespace AuthoringAndMono
{
    public class PlanetMono : MonoBehaviour
    {
        public float sunOrbitSpeed;
        public int health;
    }
    
    public class PlanetBaker : Baker<PlanetMono>
    {
        public override void Bake(PlanetMono authoring)
        {
            AddComponent(new PlanetProperties()
            {
                SunOrbitSpeed = authoring.sunOrbitSpeed,
                Health = authoring.health
            });
            AddComponent<NewPlanetTag>();
            AddBuffer<PlanetDamageBufferElement>();
            AddComponent<PlanetDebug>();
        }
    }
}