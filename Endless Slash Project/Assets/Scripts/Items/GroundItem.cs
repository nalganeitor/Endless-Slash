using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

   // public List<item, uiDisplay items = new List<item, uiDisplay>();

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponent<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponent<SpriteRenderer>());
#endif
    }
}
