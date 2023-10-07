using System;
using System.Collections.Generic;
using Devens;
using UnityEngine;

namespace Game.Characters
{
    public class PlayerCharacterSwapping : MonoBehaviour
    {
        [SerializeField] private List<CharacterSet> characters;
        [SerializeField] private int characterIndex = 0;
        [SerializeField] private int materialIndex = 0;

        private SkinnedMeshRenderer currentCharacterRenderer;
        
        [Serializable]
        public struct CharacterSet
        {
            public GameObject character;
            public AssetPackMaterialsSO materialSet;
        }
        
        private void Awake()
        {
            for (var i = 0; i < characters.Count; i++)
            {
                if (characters[i].character.activeInHierarchy)
                {
                    characterIndex = i;
                    currentCharacterRenderer = characters[characterIndex].character.GetComponent<SkinnedMeshRenderer>();
                    return;
                }
            }
        }

        public void Update()
        {
            if (Input.GetButtonDown("NextCharacter"))
            {
                characterIndex++;
                ChangeCharacter();
            }
            if (Input.GetButtonDown("LastCharacter"))
            {
                characterIndex--;
                ChangeCharacter();
            }
            
            if (Input.GetButtonDown("ChangeSkin"))
            {
                ChangeSkin();
            }
            
        }

        private void ChangeCharacter()
        {
            foreach (var character in characters)
            {
                character.character.SetActive(false);
            }

            if (characterIndex < 0)
            {
                characterIndex = characters.Count - 1;
            }

            if (characterIndex >= characters.Count)
            {
                characterIndex = 0;
            }
            characters[characterIndex].character.SetActive(true);
            currentCharacterRenderer = characters[characterIndex].character.GetComponent<SkinnedMeshRenderer>();
            materialIndex = 0;
        }

        private void ChangeSkin()
        {
            materialIndex++;

            if (materialIndex >= characters[characterIndex].materialSet.Materials.Count)
            {
                materialIndex = 0;
            }

            currentCharacterRenderer.material = characters[characterIndex].materialSet.Materials[materialIndex];
        }
    }
}
