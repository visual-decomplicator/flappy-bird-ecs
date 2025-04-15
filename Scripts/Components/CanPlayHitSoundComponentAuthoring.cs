using Unity.Entities;
using UnityEngine;

public class CanPlayHitSoundComponentAuthoring : MonoBehaviour {
    class Baker : Baker<CanPlayHitSoundComponentAuthoring> {
        public override void Bake(CanPlayHitSoundComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CanPlayHitSoundComponent { });
            AddComponent(entity, new NeedPlayHitSoundComponent { });
            SetComponentEnabled<NeedPlayHitSoundComponent>(entity, false);
        }
    }
}

public struct CanPlayHitSoundComponent : IComponentData {}
public struct NeedPlayHitSoundComponent : IComponentData, IEnableableComponent {}