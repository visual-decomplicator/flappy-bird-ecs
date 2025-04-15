using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems {
    public partial struct PipeMovingSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<GameStateComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            GameStateComponent gameState = SystemAPI.GetSingleton<GameStateComponent>();
            if (gameState.State != GameState.Play) {
                return;
            }
            foreach (var (transform, moveSpeedComponent) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeedComponent>>()) {
                transform.ValueRW.Position.z -= moveSpeedComponent.ValueRO.Value * SystemAPI.Time.DeltaTime;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {

        }
    }
}