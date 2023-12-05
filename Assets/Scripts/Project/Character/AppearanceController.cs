using UnityEngine;

namespace Project.Character
{
    public class AppearanceController : MonoBehaviour
    {
        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

        [SerializeField]
        private Renderer _renderer;

        private Material _material;

        private void Awake()
        {
            _material = _renderer.material;
        }

        public void SetTexture(Texture2D texture)
        {
            _material.SetTexture(BaseMap, texture);
        }
    }
}