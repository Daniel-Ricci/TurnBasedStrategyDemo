using UnityEngine;

namespace Actions
{
    public class MoveActionParams : BaseActionParams
    {
        public Vector3 TargetPosition;
    }
    
    public class MoveAction : BaseAction
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private int maxMovementRange = 5;
        [SerializeField] private Animator unitAnimator;

        private Vector3 _targetPosition;
        private const float StoppingDistance = 0.01f;
        
        private new void Start()
        {
            base.Start();
            Action = UnitActionSystem.GameAction.Move;
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (UnitActionSystem.Instance.GetSelectedUnit() != Unit) return;
        
            if (Vector3.Distance(_targetPosition, transform.position) > StoppingDistance)
            {
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
                unitAnimator.SetBool(IsWalking, true);
            }
            else
            {
                if (UnitActionSystem.Instance.GetCurrentAction() == UnitActionSystem.GameAction.Busy)
                {
                    OnActionFinished();
                }
            }

        }

        public new void ExecuteAction(BaseActionParams parameters)
        {
            base.ExecuteAction(parameters);
            var moveParams = (MoveActionParams)parameters;
            _targetPosition = moveParams.TargetPosition;
        }

        private new void OnActionFinished()
        {
            base.OnActionFinished();
            unitAnimator.SetBool(IsWalking, false);
        }
        
        public int GetMaxMovement()
        {
            return maxMovementRange;
        }

        public Vector3 GetTargetPosition()
        {
            return _targetPosition;
        }
    }
}
