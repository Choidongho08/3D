using System;
using UnityEngine;
using _02_Code.Core.Entities.FSM;
using _02_Code.Core.Entities.State;
using Code.SO;

namespace _02_Code.Player
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [field: SerializeField] public float MouseSensitivity { get; private set; }
        
        [SerializeField] private StateDataSO[] stateDataList;
        
        private EntityStateMachine _stateMachine;
        
        
        protected override void Awake()
        {
            base.Awake();

            _stateMachine = new EntityStateMachine(this, stateDataList);
        }

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
    }
}