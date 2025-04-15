
using Unity.Entities;
using UnityEngine;

public class PipePairTagComponentAuthoring : MonoBehaviour {
    private class PipePairTagComponentBaker : Baker<PipePairTagComponentAuthoring> {
        public override void Bake(PipePairTagComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PipePairTagComponent>(entity);
        }
    }
}

public struct PipePairTagComponent : IComponentData {}