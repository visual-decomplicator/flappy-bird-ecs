using Unity.Entities;
using UnityEngine;

public class CanPlayJumpAnimationComponentAuthoring : MonoBehaviour {
    class Baker : Baker<CanPlayJumpAnimationComponentAuthoring> {
        public override void Bake(CanPlayJumpAnimationComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CanPlayJumpAnimationComponent { });
            AddComponent(entity, new NeedPlayJumpAnimationComponent { });
            SetComponentEnabled<NeedPlayJumpAnimationComponent>(entity, false);
        }
    }
}

public struct CanPlayJumpAnimationComponent : IComponentData {}
public struct NeedPlayJumpAnimationComponent : IComponentData, IEnableableComponent {}