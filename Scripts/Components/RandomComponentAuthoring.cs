using Unity.Entities;
using UnityEngine;

public class RandomComponentAuthoring : MonoBehaviour {
    private class RandomComponentBaker : Baker<RandomComponentAuthoring> {
        public override void Bake(RandomComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new RandomComponent() {
                Value = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(0, 100000))
            });
        }
    }
}

public struct RandomComponent : IComponentData {
    public Unity.Mathematics.Random Value;
}
