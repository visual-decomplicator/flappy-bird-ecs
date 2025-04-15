using Unity.Entities;
using UnityEngine;

public class GameStateComponentAuthoring : MonoBehaviour {
    public GameState State;
    
    class Baker : Baker<GameStateComponentAuthoring> {
        public override void Bake(GameStateComponentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new GameStateComponent {
                State = authoring.State
            });
        }
    }
}

public struct GameStateComponent : IComponentData {
    public GameState State;
}

public enum GameState {
    Prepare,
    Play,
    GameOver,
    WaitForResponse
}