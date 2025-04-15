using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    public partial struct PointsSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            bool gotPoint = false;
            foreach (var (transform, entity) in SystemAPI
                         .Query<LocalTransform>()
                         .WithAll<CanGivePointComponent>()
                         .WithEntityAccess()
                     ) {
                if (transform.Position.z > 0) {
                    continue;
                }

                gotPoint = true;
                ecb.RemoveComponent<CanGivePointComponent>(entity);
            }

            if (!gotPoint) {
                return;
            }

            foreach (var pointsComponent in SystemAPI.Query<RefRW<PointsComponent>>()) {
                pointsComponent.ValueRW.Value++;
            }

            foreach (var (canPlay, entity) in SystemAPI.Query<CanPlayCoinSoundComponent>()
                         .WithNone<NeedPlayCoinSoundComponent>()
                         .WithEntityAccess()
                     ) {
                SystemAPI.SetComponentEnabled<NeedPlayCoinSoundComponent>(entity, true);
                SystemAPI.SetComponentEnabled<NeedSyncWithUIComponent>(entity, true);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {

        }
    }
}