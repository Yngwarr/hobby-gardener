using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Soil : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log($"Entered {name}");
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log($"Exited {name}");
    }
}
