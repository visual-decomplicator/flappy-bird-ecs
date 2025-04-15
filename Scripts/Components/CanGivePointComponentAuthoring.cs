using Unity.Entities;
using UnityEngine;

public class CanGivePointComponentAuthoring : MonoBehaviour {
    class Baker : Baker<CanGivePointComponentAuthoring> {
        public override void Bake(CanGivePointComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CanGivePointComponent {});
        }
    }
}

public struct CanGivePointComponent : IComponentData {}