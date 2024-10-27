using Assets;
using CustomizationInspector.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    [CustomizationInspector.Runtime.ReadOnly]
    public List<DirectionEnum> Directions = new List<DirectionEnum>();
    [CustomizationInspector.Runtime.ReadOnly]
    public int Value;
    // Start is called before the first frame update
    public NodeDictionary models = new NodeDictionary();
    public void Init(List<DirectionEnum> directions)
    {
        Directions.Clear();
        Directions.AddRange(directions);
        int tmp = 0;
        foreach (var item in Directions)
        {
            tmp |= (int)item;
        }
        Value = tmp;
        Applay();
    }
    public void Rotate()
    {
        transform.Rotate(0, 90, 0, Space.World);
        Directions.Clear();
        Value = DirectionExtension.LeftExtension(Value);
        Directions = DirectionExtension.ConvertDirection(Value);
        //Applay();
    }
    void Applay()
    {
        foreach (var item in models)
        {
            item.Value.gameObject.SetActive(false);
        }
        var enumArray = Enum.GetValues(typeof(DirectionEnum));
        foreach (DirectionEnum item in Directions)
        {
            models[item].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
