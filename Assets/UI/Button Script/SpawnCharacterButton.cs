using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacterButton : MonoBehaviour
{
    RaycastHit hit;
    float offset = 0.5f;
    [SerializeField] Characters Characters;
    string characterName = String.Empty;



    // Update is called once per frame
    void Update()
    {
        UpdateCharacterSpawning();

    }

    public void SpawnCharacter(string characterName)
    {
        this.characterName = characterName; // this.CharacterName = the string character name in the class rather than the one in the function.
    }

   private void UpdateCharacterSpawning()
    {
        GameObject characterPrefab = Characters.GetCharacterPrefab(characterName);
        if (characterPrefab == null) { return; }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 characterPos = hit.transform.position + (hit.transform.up * offset);
                
                Instantiate(characterPrefab, characterPos, Quaternion.identity);

            }
        }
    }
}
