using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;
    private ItemObject item;

    private void OnMouseEnter()
    {
    //    TooltipManager._instance.SetAndShowTooltip(message);
       // message = item.description;
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideTooltip();
    }
}
