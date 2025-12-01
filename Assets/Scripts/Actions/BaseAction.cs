using Units;
using UnityEngine;

namespace Actions
{
    public abstract class BaseActionParams { }
    
    public class BaseAction : MonoBehaviour
    {
        [SerializeField] private int actionPointsCost;
        
        protected UnitActionSystem.GameAction Action;
        protected Unit Unit;
    
        protected void Start()
        {
            Unit = GetComponent<Unit>();
        }

        protected virtual void ExecuteAction(BaseActionParams parameters)
        {
            Unit.SetActionPoints(Unit.GetActionPoints() - actionPointsCost);
            UnitActionSystem.Instance.ChangeCurrentAction(UnitActionSystem.GameAction.Busy);
        }

        protected virtual void OnActionFinished()
        {
            UnitActionSystem.Instance.ChangeCurrentAction(UnitActionSystem.GameAction.None);
        }

        public UnitActionSystem.GameAction GetAction()
        {
            return Action;
        }

        public bool CanExecuteAction()
        {
            return Unit.GetActionPoints() >= actionPointsCost;
        }
    }
}
