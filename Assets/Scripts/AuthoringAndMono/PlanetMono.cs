using ComponentsAndTags;
using UnityEngine;
using Unity.Entities;

namespace AuthoringAndMono
{
    public class PlanetMono : MonoBehaviour
    {
        public float sunOrbitSpeed;
    }
    
    public class PlanetBaker : Baker<PlanetMono>
    {
        public override void Bake(PlanetMono authoring)
        {
            AddComponent(new PlanetProperties() {SunOrbitSpeed = authoring.sunOrbitSpeed});
            AddComponent(new NewPlanetTag());
            
            AddComponent<PlanetDebug>();
        }
    }
}