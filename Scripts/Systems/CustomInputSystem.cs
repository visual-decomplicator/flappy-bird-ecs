using Unity.Entities;
using Unity.Physics;
using UnityEngine.InputSystem;


public partial class CustomInputSystem : SystemBase {
    private InputActions inputActions;
    
    protected override void OnCreate() {
        RequireForUpdate<GameStateComponent>();
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Tap.performed += TapOnperformed;
    }

    private void TapOnperformed(InputAction.CallbackContext obj) {
        GameStateComponent gameState = SystemAPI.GetSingleton<GameStateComponent>();
        if (gameState.State == GameState.Prepare) {
            var ecb = this.World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
            PrepareUI.Instance.Hide();
            gameState.State = GameState.Play;
            SystemAPI.SetSingleton(gameState);

            foreach (var (playerTag, entity) in SystemAPI.Query<PlayerTagComponent>()
                         .WithEntityAccess()
                     ) {
                ecb.AddComponent(entity, new PhysicsMassOverride() {
                    IsKinematic = 0
                });
            }
        }

        if (gameState.State != GameState.Play) {
            return;
        }
        
        foreach (var (tag, entity) in SystemAPI
                     .Query<PlayerTagComponent>()
                     .WithNone<NeedJumpTagComponent>()
                     .WithEntityAccess()
                 ) {
            SystemAPI.SetComponentEnabled<NeedJumpTagComponent>(entity, true);
        }
    }

    protected override void OnUpdate() {
        
    }

    protected override void OnDestroy() {
        inputActions.Dispose();
    }
}
