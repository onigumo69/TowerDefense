using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler OnMouseEntter;
    public event EventHandler OnMouseExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEntter?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke(this, EventArgs.Empty);
    }
}
