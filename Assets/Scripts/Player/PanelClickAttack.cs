using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClickAttack : MonoBehaviour, IPointerClickHandler
{
    public bool attack = false;

    public SwordController swordController;

    public void OnPointerClick(PointerEventData eventData)
    {
        swordController.CLICK = true;
    }
}
