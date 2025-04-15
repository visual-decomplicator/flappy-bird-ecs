using Unity.Entities;
using UnityEngine;

public class RemoveOnPositionComponentAuthoring : MonoBehaviour {
    public float Position;
    
    private class RemoveOnPositionComponentBaker : Baker<RemoveOnPositionComponentAuthoring> {
        public override void Bake(RemoveOnPositionComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RemoveOnPositionComponent() {
                Position = authoring.Position
            });
        }
    }
}

public struct RemoveOnPositionComponent : IComponentData {
    public float Position;
}
