using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;


public partial struct DetectCollisionSystem : ISystem {
    [BurstCompile]
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<SimulationSingleton>();
        state.RequireForUpdate<GameStateComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        GameStateComponent gameState = SystemAPI.GetSingleton<GameStateComponent>();
        if (gameState.State != GameState.Play) {
            return;
        }
        
        state.Dependency = new DetectCollisionJob() {
            PlayerTagLookup = SystemAPI.GetComponentLookup<PlayerTagComponent>(),
            PlayerVelocity = SystemAPI.GetComponentLookup<PhysicsVelocity>(),
            GameStateEntity = SystemAPI.GetSingletonEntity<GameStateComponent>(),
            Ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged)
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) {

    }
}

public struct DetectCollisionJob : ICollisionEventsJob {
    [ReadOnly] public ComponentLookup<PlayerTagComponent> PlayerTagLookup;
    public ComponentLookup<PhysicsVelocity> PlayerVelocity;
    public Entity GameStateEntity;
    public EntityCommandBuffer Ecb;
    
    public void Execute(CollisionEvent collisionEvent) {
        Entity birdEntity = Entity.Null;
        if (PlayerTagLookup.HasComponent(collisionEvent.EntityA)) {
            birdEntity = collisionEvent.EntityA;
        } else if (PlayerTagLookup.HasComponent(collisionEvent.EntityB)) {
            birdEntity = collisionEvent.EntityB;
        }

        if (birdEntity == Entity.Null) {
            return;
        }

        PhysicsVelocity currentVelocity = PlayerVelocity[birdEntity];
        currentVelocity.Linear = new float3(currentVelocity.Linear.x, currentVelocity.Linear.y, 1f);
        PlayerVelocity[birdEntity] = currentVelocity;
        Ecb.SetComponent(GameStateEntity, new GameStateComponent() {
            State = GameState.GameOver
        });
        Ecb.SetComponentEnabled<NeedPlayHitSoundComponent>(birdEntity, true);
    }
}