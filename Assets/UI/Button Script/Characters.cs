using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyGame",menuName = "characters")]
public class Characters : ScriptableObject
{
    [SerializeField] List<CharacterInfo> charactersList;
    public GameObject GetCharacterPrefab(string characterName)
    {
        foreach (CharacterInfo characterInfo in charactersList)
        {
            if (characterInfo.characterName == characterName)
            {
                return characterInfo.characterObject;
            }
        }
        return null;
    }
}
