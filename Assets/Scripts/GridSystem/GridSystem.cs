using Actions;
using UnityEngine;

namespace GridSystem
{
    public class GridSystem
    {
        private int _width;
        private int _height;
        private readonly float _cellSize;
        private readonly GridObject[,] _gridObjectsArray;

        public GridSystem(int width, int height, float cellSize, GridObject gridObjectPrefab)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _gridObjectsArray = new GridObject[width, height];

            for (var x = 0; x < width; x++)
            {
                for(var z = 0; z < height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    _gridObjectsArray[x,z] = Object.Instantiate(gridObjectPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                    _gridObjectsArray[x,z].SetGridPosition(gridPosition);
                }
            }
        
            UnitActionSystem.OnActionChanged += OnActionChanged;
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize),
                Mathf.RoundToInt(worldPosition.z / _cellSize)
            );
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return _gridObjectsArray[gridPosition.x, gridPosition.z];
        }

        public bool IsGridPositionValid(GridPosition gridPosition)
        {
            return gridPosition.x >= 0 &&
                   gridPosition.z >= 0 &&
                   gridPosition.x < _width &&
                   gridPosition.z < _height;
        }

        private void OnActionChanged(UnitActionSystem.GameAction action)
        {
            switch (action)
            {
                case UnitActionSystem.GameAction.None:
                case UnitActionSystem.GameAction.Busy:
                    foreach (var gridObject in _gridObjectsArray)
                    {
                        gridObject.SetClickable(false);
                    }
                    break;
                case UnitActionSystem.GameAction.Move:
                    var selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
                    var unitGridPosition = GetGridPosition(selectedUnit.transform.position);
                    var movementRange = selectedUnit.GetComponent<MoveAction>().GetMaxMovement();
                    foreach (var gridObject in _gridObjectsArray)
                    {
                        if (Mathf.Abs(gridObject.GetGridPosition() - unitGridPosition) <= movementRange && gridObject.GetCurrentObject() == null)
                        {
                            gridObject.SetClickable(true);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
