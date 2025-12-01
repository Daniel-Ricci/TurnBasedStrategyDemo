using System;
using GridSystem;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using Logger = Utils.Logger;

namespace Actions
{
    public class UnitActionSystem : MonoBehaviour
    {
        public enum GameAction
        {
            None,
            Busy,
            Move
        }

        public static UnitActionSystem Instance { get; private set; }

        [SerializeField] private LayerMask unitsLayerMask;
        [SerializeField] private LayerMask mousePlaneLayerMask;
        [SerializeField] GridObject gridObjectPrefab;

        private GridSystem.GridSystem _gridSystem;
        private Unit _selectedUnit;
        private GameAction _currentAction = GameAction.None;

        public static event Action<Unit> OnUnitSelectedChanged;
        public static event Action<GameAction> OnActionChanged;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _gridSystem = new GridSystem.GridSystem(10, 10, 2f, gridObjectPrefab);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HandleMouseClick();
            }
        }

        private void HandleMouseClick()
        {
            if (_currentAction == GameAction.Busy) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            switch (_currentAction)
            {
                case GameAction.None:
                    if (Physics.Raycast(ray, out RaycastHit raycastHitUnit, float.MaxValue, unitsLayerMask | mousePlaneLayerMask))
                    {
                        if (raycastHitUnit.transform.TryGetComponent(out Unit unit))
                        {
                            SetSelectedUnit(unit);
                        }
                        else
                        {
                            SetSelectedUnit(null);
                        }
                    }
                    break;
                case GameAction.Move:
                    if (Physics.Raycast(ray, out RaycastHit raycastHitPlane, float.MaxValue, mousePlaneLayerMask))
                    {
                        GridPosition hitGridPosition = _gridSystem.GetGridPosition(raycastHitPlane.point);
                        if (_gridSystem.IsGridPositionValid(hitGridPosition) &&
                            _gridSystem.GetGridObject(hitGridPosition).GetCurrentObject() == null &&
                            _gridSystem.GetGridObject(hitGridPosition).IsClickable())
                        {
                            _gridSystem.GetGridObject(_gridSystem.GetGridPosition(_selectedUnit.GetComponent<MoveAction>().GetTargetPosition())).ClearCurrentObject();
                            _gridSystem.GetGridObject(_gridSystem.GetGridPosition(raycastHitPlane.point)).SetCurrentObject(_selectedUnit.gameObject);
                            _selectedUnit.GetComponent<MoveAction>().ExecuteAction(new MoveActionParams
                            {
                                TargetPosition = _gridSystem.GetWorldPosition(_gridSystem.GetGridPosition(raycastHitPlane.point))
                            });
                        }
                    }
                    break;
            }
        }

        private void SetSelectedUnit(Unit unit)
        {
            _selectedUnit = unit;
            OnUnitSelectedChanged?.Invoke(_selectedUnit);
            Logger.Log($"Unit selected: {_selectedUnit}");
        }
    
        public Unit GetSelectedUnit()
        {
            return _selectedUnit;
        }

        public void ChangeCurrentAction(GameAction action) 
        {
            _currentAction = action;
            OnActionChanged?.Invoke(_currentAction);
            Logger.Log($"Current action changed to {_currentAction}");
        }

        public GridSystem.GridSystem GetGridSystem()
        {
            return _gridSystem;
        }

        public GameAction GetCurrentAction()
        {
            return _currentAction;
        }
    }
}
