using Units;
using UnityEngine;

namespace GridSystem
{
    public class GridObject : MonoBehaviour
    {
        [SerializeField] private GridDebugObject debugObject;
        [SerializeField] private GridVisualObject visualObject;

        private GridPosition _gridPosition;
        private GameObject _currentObject;
        private bool _isClickable = false;

        public void SetGridPosition(GridPosition gridPosition)
        {
            _gridPosition = gridPosition;
            debugObject.UpdateText(ToString());
        }

        public GridPosition GetGridPosition()
        {
            return _gridPosition;
        }

        public void SetCurrentObject(GameObject currentObject)
        {
            _currentObject = currentObject;
            debugObject.UpdateText(ToString());
        }

        public GameObject GetCurrentObject()
        {
            return _currentObject;
        }

        public void ClearCurrentObject()
        {
            _currentObject = null;
            debugObject.UpdateText(ToString());
        }

        public void SetClickable(bool isClickable)
        {
            _isClickable = isClickable;
            if (_isClickable) visualObject.SetGreenMaterial(); else visualObject.SetDefaultMaterial();
        }

        public bool IsClickable()
        {
            return _isClickable;
        }

        public override string ToString()
        {
            return _gridPosition.ToString() + '\n' + _currentObject?.GetComponent<Unit>().ToString();
        }
    }
}
