using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class JumpComponentAuthoring : MonoBehaviour {
    public float JumpForce;
    public float RotationForce;
    public float JumpRotationAngle;
    
    class Baker : Baker<JumpComponentAuthoring> {
        public override void Bake(JumpComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new JumpComponent {
                JumpForce = authoring.JumpForce,
                RotationForce = authoring.RotationForce,
                JumpRotationAngle = quaternion.EulerXYZ(authoring.JumpRotationAngle, 0f, 0f)
            });
        }
    }
}

public struct JumpComponent : IComponentData {
    public float JumpForce;
    public float RotationForce;
    public quaternion JumpRotationAngle;
}