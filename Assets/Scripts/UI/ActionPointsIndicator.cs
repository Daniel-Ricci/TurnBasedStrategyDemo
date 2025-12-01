using Actions;
using TMPro;
using Units;
using UnityEngine;
using Utils;

namespace UI
{
    public class ActionPointsIndicator : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        [SerializeField] private Canvas canvas;
    
        private TextMeshProUGUI _indicatorText;
        private Camera _mainCamera;

        private const string GreenHex = "#44FF20";
        private const string RedHex = "#FF0F00";

        private void Awake()
        {
            _indicatorText = GetComponent<TextMeshProUGUI>();
            _mainCamera = Camera.main;
            UnitActionSystem.OnUnitSelectedChanged += OnUnitSelected;
        }
    
        private void LateUpdate()
        {
            // canvas.transform.LookAt(_mainCamera.transform);
            canvas.transform.LookAt(canvas.transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }

        private void OnUnitSelected(Unit selectedUnit)
        {
            _indicatorText.enabled = unit == selectedUnit;
        }

        public void UpdateActionPoints(int actionPointsLeft)
        {
            _indicatorText.text = $"{actionPointsLeft}";
            _indicatorText.color = actionPointsLeft > 0 ? ColorUtils.HexToColor(GreenHex) : ColorUtils.HexToColor(RedHex);
        }
    }
}
