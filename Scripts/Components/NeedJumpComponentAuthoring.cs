
using Unity.Entities;
using UnityEngine;

public class NeedJumpTagComponentAuthoring : MonoBehaviour {
    private class NeedJumpTagComponentBaker : Baker<NeedJumpTagComponentAuthoring> {
        public override void Bake(NeedJumpTagComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<NeedJumpTagComponent>(entity);
            SetComponentEnabled<NeedJumpTagComponent>(entity, false);
        }
    }
}

public struct NeedJumpTagComponent : IComponentData, IEnableableComponent {}