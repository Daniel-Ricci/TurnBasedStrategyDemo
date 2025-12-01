using TMPro;
using UnityEngine;

namespace GridSystem
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMeshPro;

        public void UpdateText(string text)
        {
            textMeshPro.SetText(text);
        }
    }
}