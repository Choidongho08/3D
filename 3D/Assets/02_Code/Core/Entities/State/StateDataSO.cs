using System;
using UnityEngine;
using UnityEngine.UI;

namespace _02_Code.Core.Entities.State
{
    [CreateAssetMenu(menuName = "StateDataSO", fileName = "SO/FSM/StateData")]
    public class StateDataSO : ScriptableObject
    {
        public string stateName;
        public string className;
        public string animParamName;
        
        public int animationHash;

        private void OnValidate()
        {
            animationHash = Animator.StringToHash(animParamName);
        }
    }
}