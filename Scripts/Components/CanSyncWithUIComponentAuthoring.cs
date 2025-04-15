using Unity.Entities;
using UnityEngine;

public class CanSyncWithUIComponentAuthoring : MonoBehaviour {
    class Baker : Baker<CanSyncWithUIComponentAuthoring> {
        public override void Bake(CanSyncWithUIComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CanSyncWithUIComponent { });
            AddComponent(entity, new NeedSyncWithUIComponent { });
            SetComponentEnabled<NeedSyncWithUIComponent>(entity, false);
        }
    }
}

public struct CanSyncWithUIComponent : IComponentData {}
public struct NeedSyncWithUIComponent : IComponentData, IEnableableComponent {}