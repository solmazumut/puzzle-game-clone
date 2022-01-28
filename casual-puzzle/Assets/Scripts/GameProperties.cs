using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameProperties")]
public class GameProperties : ScriptableObject
{
    public float ColumnLength;
    public float RowLength;
    public float ZValue;
    public float BlockSpeed;
    public float NewBlockEmergingPointYCoordinate;

}
