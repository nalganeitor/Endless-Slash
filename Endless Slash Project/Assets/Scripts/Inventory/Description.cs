using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public TextMeshProUGUI names, description, buffs;
    public Image background;
    public Image item;


    public static Description Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Error: There are multiple descriptions present, deleting old one");
            Destroy(Instance);
            Instance = this;
        }
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public void AssignValues(string _name, string _description, string _buffs, Sprite _itemSprite)
    {
        names.text = _name;
        description.text = _description;
        buffs.text = _buffs;
        item.sprite = _itemSprite;
    }
}