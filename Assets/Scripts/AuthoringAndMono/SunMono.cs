using ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class SunMono : MonoBehaviour
    {
        //public int health;
        public int maxHealth;
    }

    public class SunBaker : Baker<SunMono>
    {
        public override void Bake(SunMono authoring)
        {
            AddComponent(new SunHealth
            {
                Health = authoring.maxHealth,
                MaxHealth = authoring.maxHealth
            });
            AddComponent<SunTag>();
            AddBuffer<PlanetDamageBufferElement>();
        }
    }
}