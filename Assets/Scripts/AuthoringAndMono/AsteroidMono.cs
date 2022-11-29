using ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class AsteroidMono : MonoBehaviour
    {
        public float AsteroidMoveSpeed;
    }

    public class AsteroidBaker : Baker<AsteroidMono>
    {
        public override void Bake(AsteroidMono authoring)
        {
            AddComponent(new AsteroidMoveSpeed {Value = authoring.AsteroidMoveSpeed});
            AddComponent(new AsteroidHeading());
            AddComponent<PlanetList>();
        }
    }
}