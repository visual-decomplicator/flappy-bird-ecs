using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Systems {
    public partial struct PlayerJumpSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<GameStateComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            GameStateComponent gameState = SystemAPI.GetSingleton<GameStateComponent>();
            if (gameState.State != GameState.Play) {
                return;
            }
            
            foreach (var (physicsVelocity, transform, jumpComponent, particleEntity, entity) in SystemAPI
                         .Query<RefRW<PhysicsVelocity>, RefRW<LocalTransform>, JumpComponent, ParticleEntityComponent>()
                         .WithAll<NeedJumpTagComponent>()
                         .WithEntityAccess()
                     ) {
                transform.ValueRW.Rotation = jumpComponent.JumpRotationAngle;
                physicsVelocity.ValueRW.Linear = new float3(0f, jumpComponent.JumpForce, 0f);
                physicsVelocity.ValueRW.Angular = new float3(jumpComponent.RotationForce, 0f, 0f);

                Entity particle = ecb.Instantiate(particleEntity.Prefab);
                ecb.SetComponent(particle, transform.ValueRO);
                
                SystemAPI.SetComponentEnabled<NeedJumpTagComponent>(entity, false);
                SystemAPI.SetComponentEnabled<NeedPlaySwingSoundComponent>(entity, true);
                SystemAPI.SetComponentEnabled<NeedPlayJumpAnimationComponent>(entity, true);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {

        }
    }
}