using UnityEngine;

namespace Project.Character
{
    [CreateAssetMenu(menuName = "Project/Customization/" + nameof(CharacterCustomizationData), fileName = nameof(CharacterCustomizationData))]
    public class CharacterCustomizationData : ScriptableObject
    {
        [SerializeField]
        private Texture2D[] _textures;

        public Texture2D[] Textures => _textures;
    }
}