using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI btnText;

        private Button _button;
        private UnitActionSystem.GameAction _gameAction;

        private void Awake()
        {
            _button = GetComponent<Button>();
            UnitActionSystem.OnActionChanged += OnGameActionChanged;
        }

        private void OnDestroy()
        {
            UnitActionSystem.OnActionChanged -= OnGameActionChanged;
        }

        private void OnGameActionChanged(UnitActionSystem.GameAction action)
        {
            int actionPointsLeft = UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints();
            SetInteractable(action != _gameAction && action != UnitActionSystem.GameAction.Busy && actionPointsLeft > 0);
        }

        public void SetGameAction(UnitActionSystem.GameAction gameAction)
        {
            _gameAction = gameAction;
            btnText.SetText(gameAction.ToString());
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        public void OnActionButtonClicked()
        {
            UnitActionSystem.Instance.ChangeCurrentAction(_gameAction);
        }
    }
}
