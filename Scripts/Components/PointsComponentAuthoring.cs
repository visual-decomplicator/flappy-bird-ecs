using Unity.Entities;
using UnityEngine;

public class PointsComponentAuthoring : MonoBehaviour {
    class Baker : Baker<PointsComponentAuthoring> {
        public override void Bake(PointsComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new PointsComponent {
                Value = 0
            });
        }
    }
}

public struct PointsComponent : IComponentData {
    public int Value;
}