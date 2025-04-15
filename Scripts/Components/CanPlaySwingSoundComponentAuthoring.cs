using Unity.Entities;
using UnityEngine;

public class CanPlaySwingSoundComponentAuthoring : MonoBehaviour {
    class Baker : Baker<CanPlaySwingSoundComponentAuthoring> {
        public override void Bake(CanPlaySwingSoundComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CanPlaySwingSoundComponent { });
            AddComponent(entity, new NeedPlaySwingSoundComponent { });
            SetComponentEnabled<NeedPlaySwingSoundComponent>(entity, false);
        }
    }
}

public struct CanPlaySwingSoundComponent : IComponentData {}
public struct NeedPlaySwingSoundComponent : IComponentData, IEnableableComponent {}