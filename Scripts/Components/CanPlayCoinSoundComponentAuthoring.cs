using Unity.Entities;
using UnityEngine;

public class CanPlayCoinSoundComponentAuthoring : MonoBehaviour {
    class Baker : Baker<CanPlayCoinSoundComponentAuthoring> {
        public override void Bake(CanPlayCoinSoundComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CanPlayCoinSoundComponent { });
            AddComponent(entity, new NeedPlayCoinSoundComponent { });
            SetComponentEnabled<NeedPlayCoinSoundComponent>(entity, false);
        }
    }
}

public struct CanPlayCoinSoundComponent : IComponentData {}
public struct NeedPlayCoinSoundComponent : IComponentData, IEnableableComponent {}