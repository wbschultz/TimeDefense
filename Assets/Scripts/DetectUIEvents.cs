using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class BoolUnityEvent : UnityEvent<bool> { }

public class DetectUIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BoolUnityEvent onEnterUpgradeMenu;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("OnPointerEnter upgrade Menu");
        onEnterUpgradeMenu.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("OnPointerExit upgrade Menu");
        onEnterUpgradeMenu.Invoke(false);
    }
}
