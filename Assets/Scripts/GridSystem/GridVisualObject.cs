using UnityEngine;

namespace GridSystem
{
    public class GridVisualObject : MonoBehaviour
    {
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material greenMaterial;

        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void SetDefaultMaterial()
        {
            _meshRenderer.material = defaultMaterial;
        }

        public void SetGreenMaterial()
        {
            _meshRenderer.material = greenMaterial;
        }
    }
}
