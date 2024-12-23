using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName="Data/Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite icon;
    public bool stackable;
    public string tag;
}
