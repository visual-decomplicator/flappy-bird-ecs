using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public class SetPhysicsMassOverrideComponentAuthoring : MonoBehaviour {
    public byte IsKinematic;
    public byte SetVelocityToZero;
    
    class Baker : Baker<SetPhysicsMassOverrideComponentAuthoring> {
        public override void Bake(SetPhysicsMassOverrideComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PhysicsMassOverride() {
                IsKinematic = authoring.IsKinematic,
                SetVelocityToZero = authoring.SetVelocityToZero
            });
        }
    }
}
