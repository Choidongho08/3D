using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace _02_Code.Core.StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/Stat", order = 0)]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float current, float previous);
        public event ValueChangeHandler OnValueChange;

        public string statName;
        [TextArea]
        public string description;
        
        [SerializeField] private Sprite icon;
        [SerializeField] private string displayName;
        [SerializeField] private float baseValue, minValue, maxValue;

        private Dictionary<object, float> _modifyDictionary = new Dictionary<object, float>();
        
        [field: SerializeField] public bool isPercent { get; private set; }
        
        private float _modifiedValue;

        public Sprite Icon => icon;
        
        public float MinValue
        {
            get => minValue;
            set => minValue = value;
        }

        public float MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }

        public float BaseValue
        {
            get => baseValue;
            set
            {
                float previousValue = value;
                baseValue = Mathf.Clamp(value, minValue, maxValue);
                TryInvokeValueChange(Value, previousValue);
            }
        }
        
        public float Value => Mathf.Clamp(baseValue + _modifiedValue, minValue, maxValue);
        public bool IsMax => Mathf.Approximately(Value, maxValue); 
        public bool IsMin => Mathf.Approximately(Value, minValue);

        public void AddModifier(object key, float value)
        {
            if (_modifyDictionary.ContainsKey(key)) return;
            float previouseValue = value;
            
            _modifiedValue += value;
            _modifyDictionary.Add(key, value);
            
            TryInvokeValueChange(Value, previouseValue);
        }

        public void RemoveModifier(object key)
        {
            if (_modifyDictionary.TryGetValue(key, out float value))
            {
                float previousValue = value;
                
                _modifiedValue -= value;
                _modifyDictionary.Remove(key);
                
                TryInvokeValueChange(Value, previousValue);
            }
        }

        public void ClearAllModifiers()
        {
            float previousValue = Value;
            _modifiedValue = 0;
            _modifyDictionary.Clear();
            TryInvokeValueChange(Value, previousValue);
        }
        
        private void TryInvokeValueChange(float value, float previousValue)
        {
            
        }
        
        public object Clone() => Instantiate(this);
    }

}