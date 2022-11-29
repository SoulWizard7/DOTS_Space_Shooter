using Unity.Entities;

namespace ComponentsAndTags
{
    public struct PlanetProperties : IComponentData
    {
        public float SunOrbitSpeed;
        public float RadiusSQ;
        public float Health;
    }

    public struct PlanetDebug : IComponentData
    {
        public float CurDistanceFromSun;
    }
    
    public struct NewPlanetTag : IComponentData {}
}