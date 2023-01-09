using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ExpWidget : MonoBehaviour
{
    TMP_Text _label;
    
    void Awake() {
        _label = GetComponent<TMP_Text>();
        GameEvents.ExpChanged.AddListener(UpdateText);
    }
    
    void UpdateText(int exp) {
        _label.text = exp.ToString();
    }
}