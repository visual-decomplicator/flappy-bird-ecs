using System;
using UnityEngine;

public class PrepareUI : MonoBehaviour
{
    public static PrepareUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
