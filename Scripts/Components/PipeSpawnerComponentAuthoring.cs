using Unity.Entities;
using UnityEngine;

public class PipeSpawnerComponentAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float TimeInterval;
    public float Timer;
    public float MaxTopPoint;
    public float MaxBottomPoint;
    
    private class PipeSpawnerComponentBaker : Baker<PipeSpawnerComponentAuthoring> {
        public override void Bake(PipeSpawnerComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PipeSpawnerComponent() {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                TimeInterval = authoring.TimeInterval,
                Timer = authoring.Timer,
                MaxTopPoint = authoring.MaxTopPoint,
                MaxBottomPoint = authoring.MaxBottomPoint
            });
        }
    }
}

public struct PipeSpawnerComponent : IComponentData {
    public Entity Prefab;
    public float TimeInterval;
    public float Timer;
    public float MaxTopPoint;
    public float MaxBottomPoint;
}


