using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StriXInteractive.Tools.GlobalVariables {
    [Serializable]
    public class GenericGlobalVariableSO<T> : ScriptableObject, ISerializationCallbackReceiver {

        [SerializeField] private T initialValue;
        [NonSerialized] public T RuntimeValue;

        [Header("Editor Only")]
        [SerializeField] private bool saveDuringPlayMode;

        [NonSerialized] public UnityAction OnValueChanged;

        public virtual T Value {
            get { return RuntimeValue; }
            set {

                T oldValue = RuntimeValue;

                RuntimeValue = value;

                if (!EqualityComparer<T>.Default.Equals(oldValue, RuntimeValue)) {
                    if (OnValueChanged != null) {
                        OnValueChanged?.Invoke();
                    }
                }
            }
        }

        public void OnAfterDeserialize() {
            Value = initialValue;
        }

        public void OnBeforeSerialize() {
            if (saveDuringPlayMode) {
                initialValue = Value;
            }
        }
    }
}