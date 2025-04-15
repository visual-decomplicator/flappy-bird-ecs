using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems {
    public partial struct PipeSpawnerSystem : ISystem {
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
            
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (pipeSpawnerComponent, transform, random) in SystemAPI
                         .Query<RefRW<PipeSpawnerComponent>, RefRO<LocalTransform>, RefRW<RandomComponent>>()) {
                pipeSpawnerComponent.ValueRW.Timer += SystemAPI.Time.DeltaTime;
                if (pipeSpawnerComponent.ValueRO.Timer < pipeSpawnerComponent.ValueRO.TimeInterval) {
                    continue;
                }
                
                pipeSpawnerComponent.ValueRW.Timer = 0f;
                Entity spawnedEntity = ecb.Instantiate(pipeSpawnerComponent.ValueRO.Prefab);
                var newTransform = transform.ValueRO;
                newTransform.Position.y = random.ValueRW.Value.NextFloat(pipeSpawnerComponent.ValueRO.MaxBottomPoint,
                    pipeSpawnerComponent.ValueRO.MaxTopPoint);
                ecb.SetComponent(spawnedEntity, newTransform);
            }
        }
    }
}