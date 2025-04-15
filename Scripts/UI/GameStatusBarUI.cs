using System;
using TMPro;
using UnityEngine;

public class GameStatusBarUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _pointsText;
    
    public static GameStatusBarUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void SetPoints(int points) {
        _pointsText.SetText(points.ToString());
    }
}
