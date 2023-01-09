using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlantOptionButton : MonoBehaviour
{
    [SerializeField] PlantInfo info;
    [SerializeField] TMP_Text amountLabel;
    
    Button _button;
    
    public PlantInfo Info => info;

    void Awake() {
        _button = GetComponent<Button>();
    }

    void Start() {
        UpdateText();
        GameEvents.SeedAmountChanged.AddListener(UpdateText);
    }
    
    void UpdateText() {
        amountLabel.text = info.seedsLeft.ToString();
    }

    public void Set(bool on) {
        _button.interactable = !on;
    }
}