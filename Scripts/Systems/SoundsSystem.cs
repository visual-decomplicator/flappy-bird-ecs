using Unity.Entities;


public partial class SoundsSystem : SystemBase {
    protected override void OnUpdate() {
        foreach (var (needPlayCoinSound, entity) in SystemAPI.Query<NeedPlayCoinSoundComponent>()
                     .WithEntityAccess()
                 ) {
            SystemAPI.SetComponentEnabled<NeedPlayCoinSoundComponent>(entity, false);
            SoundsManager.Instance.PlayCoinSound();
        }
        
        foreach (var (needPlaySwingSound, entity) in SystemAPI.Query<NeedPlaySwingSoundComponent>()
                     .WithEntityAccess()
                ) {
            SystemAPI.SetComponentEnabled<NeedPlaySwingSoundComponent>(entity, false);
            SoundsManager.Instance.PlaySwingSound();
        }
        
        foreach (var (needPlayHitSound, entity) in SystemAPI.Query<NeedPlayHitSoundComponent>()
                     .WithEntityAccess()
                ) {
            SystemAPI.SetComponentEnabled<NeedPlayHitSoundComponent>(entity, false);
            SoundsManager.Instance.PlayHitSound();
        }
    }
}
