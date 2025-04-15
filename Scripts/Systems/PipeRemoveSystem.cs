using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    public partial struct PipeRemoveSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (transform, removeOnPositionComponent, entity) in SystemAPI
                         .Query<LocalTransform, RemoveOnPositionComponent>()
                         .WithEntityAccess()
                     ) {
                if (transform.Position.z < removeOnPositionComponent.Position) {
                    ecb.DestroyEntity(entity);
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {

        }
    }
}