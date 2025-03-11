using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _02_Code.Core.Entities.FSM
{
    public abstract class Entity : MonoBehaviour
    {
        protected Dictionary<Type, IEntityComponent> _components;

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();
            AddComponentToDictionary();
            ComponentsInitialized();
            AfterInitialize();
        }

        protected virtual void AddComponentToDictionary()
        {
            GetComponentsInChildren<IEntityComponent>(true).ToList().
                ForEach(compo => _components.Add(compo.GetType(), compo));
        }

        protected virtual void ComponentsInitialized()
        {
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInit>().ToList().ForEach(compo => compo.AfterInit());
        }

        public T GetCompo<T>(bool isDervied) where T : IEntityComponent
        {
            if(_components.TryGetValue(typeof(T), out IEntityComponent component))
                return (T)component;
            if(isDervied == false)
                return default;
            
            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if(findType != null)
                return (T)_components[findType];
            
            return default;
        }
    }
}