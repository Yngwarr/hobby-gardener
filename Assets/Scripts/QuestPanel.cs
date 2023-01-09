using System;
using TMPro;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    
    public void Set(Quest quest) {
        label.text = quest.ToString();
    }
}