using Unity.Entities;
using UnityEngine;

public class ParticleEntityComponentAuthoring : MonoBehaviour {
    public GameObject Prefab;
    private class ParticleEntityComponentBaker : Baker<ParticleEntityComponentAuthoring> {
        public override void Bake(ParticleEntityComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ParticleEntityComponent() {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct ParticleEntityComponent : IComponentData {
    public Entity Prefab;
}