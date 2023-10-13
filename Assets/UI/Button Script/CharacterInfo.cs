using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyGame", menuName = "characterinfo")]
public class CharacterInfo : ScriptableObject
{
    public  GameObject characterObject;
    public  string characterName;
}
