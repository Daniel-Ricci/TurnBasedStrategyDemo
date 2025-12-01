using Actions;
using GridSystem;
using UI;
using UnityEngine;
using Logger = Utils.Logger;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private MeshRenderer selectedVisual;
        [SerializeField] private ActionPointsIndicator actionPointsIndicator;
        [SerializeField] private GridPosition startingPosition;
        [SerializeField] private int maxActionPoints;

        private int _unitIndex;
        private int _actionPoints;
        private static int _lastUnitIndex = 0;

        private void Awake()
        {
            _unitIndex = ++_lastUnitIndex;
            _actionPoints = maxActionPoints;

            // Doing this on awake since UnitActionSystem executes first.
            // Still logging an error in case something goes wrong.
            if (UnitActionSystem.Instance)
            {
                UnitActionSystem.OnUnitSelectedChanged += OnUnitSelected;

                GridSystem.GridSystem gridSystem = UnitActionSystem.Instance.GetGridSystem();
                transform.position = gridSystem.GetWorldPosition(startingPosition);
                gridSystem.GetGridObject(gridSystem.GetGridPosition(this.transform.position))?.SetCurrentObject(this.gameObject);
            }
            else
            {
                Logger.Log("No instance of UnitActionSystem found.", LogType.Error);
            }
        }

        private void OnUnitSelected(Unit unit)
        {
            selectedVisual.enabled = unit == this;
        }

        public void SetActionPoints(int points)
        {
            _actionPoints = points;
            actionPointsIndicator.UpdateActionPoints(_actionPoints);
        }

        public int GetActionPoints()
        {
            return _actionPoints;
        }

        public UnitActionSystem.GameAction[] GetUnitActions()
        {
            return new UnitActionSystem.GameAction[]
            {
                UnitActionSystem.GameAction.Move
            };
        }

        public override string ToString()
        {
            return $"Unit {_unitIndex}";
        }
    }
}
