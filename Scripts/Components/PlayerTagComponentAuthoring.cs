
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public class PlayerTagComponentAuthoring : MonoBehaviour {
    private class PlayerTagComponentBaker : Baker<PlayerTagComponentAuthoring> {
        public override void Bake(PlayerTagComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerTagComponent());
        }
    }
}

public struct PlayerTagComponent : IComponentData {}