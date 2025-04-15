using Unity.Entities;


public partial class SyncWithUISystem : SystemBase {
    protected override void OnCreate() {
        RequireForUpdate<GameStateComponent>();
    }

    protected override void OnUpdate() {
        foreach (var (pointsComponent, entity) in SystemAPI.Query<PointsComponent>()
                     .WithAll<NeedSyncWithUIComponent>()
                     .WithEntityAccess()
                 ) {
            GameStatusBarUI.Instance.SetPoints(pointsComponent.Value);
            SystemAPI.SetComponentEnabled<NeedSyncWithUIComponent>(entity, false);
        }

        GameStateComponent gameState = SystemAPI.GetSingleton<GameStateComponent>();
        if (gameState.State == GameState.GameOver) {
            GameOverUI.Instance.Show();
            gameState.State = GameState.WaitForResponse;
            SystemAPI.SetSingleton(gameState);
        }
    }
}
