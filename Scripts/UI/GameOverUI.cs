using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private Button _restartButton;
    
    public static GameOverUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
        Hide();
        
        _restartButton.onClick.AddListener(() => {
            Hide();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
