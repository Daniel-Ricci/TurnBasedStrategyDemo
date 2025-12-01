using System.Collections.Generic;
using Actions;
using Units;
using UnityEngine;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private ActionButton actionButtonPrefab;
        [SerializeField] private Transform actionButtonsContainer;

        private List<ActionButton> _actionButtons = new();

        private void Start()
        {
            UnitActionSystem.OnUnitSelectedChanged += UpdateActionButtons;
        }

        private void UpdateActionButtons(Unit selectedUnit)
        {
            if (_actionButtons.Count > 0) ClearActionButtons();
            if (selectedUnit == null) return;
            foreach (UnitActionSystem.GameAction action in selectedUnit.GetUnitActions())
            {
                ActionButton actionButton = Instantiate(actionButtonPrefab, actionButtonsContainer);
                actionButton.SetGameAction(action);
                actionButton.SetInteractable(selectedUnit.GetActionPoints() > 0);
                _actionButtons.Add(actionButton);
            }
        }

        private void ClearActionButtons()
        {
            foreach (var button in _actionButtons)
            {
                Destroy(button.gameObject);
            }
            _actionButtons.Clear();
        }
    }
}
