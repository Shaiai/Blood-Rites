using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{

    public Character character;

    public bool decision;
    public bool narration;
    public int endOfChoice;

    public string decision1;
    public string decision2;

    [TextArea(2,5)]
    public string text;
}


[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]

public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;

}    
