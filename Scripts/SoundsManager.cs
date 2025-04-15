using UnityEngine;

public class SoundsManager : MonoBehaviour {
    [SerializeField] private SoundsSO _soundsSo;
    
    public static SoundsManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void PlayClip(AudioClip clip, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, new Vector3(), volume);
    }

    public void PlaySwingSound() {
        PlayClip(_soundsSo.swing);
    }
    public void PlayHitSound() {
        PlayClip(_soundsSo.hit);
    }
    public void PlayCoinSound() {
        PlayClip(_soundsSo.coin);
    }
}
