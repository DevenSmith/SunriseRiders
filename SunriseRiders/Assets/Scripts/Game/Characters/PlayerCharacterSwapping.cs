using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters
{
    public class PlayerCharacterSwapping : MonoBehaviour
    {
        [SerializeField] private List<GameObject> characters;
        [SerializeField] private int characterIndex = 0;

        [SerializeField] private List<Material> materials;
        [SerializeField] private int materialIndex = 0;

        private SkinnedMeshRenderer currentCharacterRenderer;
        
        private void Awake()
        {
            for (var i = 0; i < characters.Count; i++)
            {
                if (characters[i].activeInHierarchy)
                {
                    characterIndex = i;
                    return;
                }
            }

            currentCharacterRenderer = characters[characterIndex].GetComponent<SkinnedMeshRenderer>();
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
                character.SetActive(false);
            }

            if (characterIndex < 0)
            {
                characterIndex = characters.Count - 1;
            }

            if (characterIndex >= characters.Count)
            {
                characterIndex = 0;
            }
            characters[characterIndex].SetActive(true);
            currentCharacterRenderer = characters[characterIndex].GetComponent<SkinnedMeshRenderer>();
        }

        private void ChangeSkin()
        {
            materialIndex++;

            if (materialIndex >= materials.Count)
            {
                materialIndex = 0;
            }

            currentCharacterRenderer.material = materials[materialIndex];
        }
    }
}
