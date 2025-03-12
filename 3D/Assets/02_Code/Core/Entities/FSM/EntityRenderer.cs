using UnityEngine;

namespace _02_Code.Core.Entities.FSM
{
    public class EntityRenderer :  MonoBehaviour, IEntityComponent
    {
        //[SerializeField] private AnimParamSO yVelocityParam;

        private Entity _entity;
        private Animator _animator;

        public void Initialize(Entity entity)
        { 
            _entity = entity;
            _animator = GetComponent<Animator>();      

        }

       //public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
       //public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);
       //public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue, value);
       //public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);

       public void HandleVelocityChange(Vector3 movement)
       {
           //if(yVelocityParam != null)
           //    SetParam(yVelocityParam, movement.y);
       }
       
       // 회전

    }
}