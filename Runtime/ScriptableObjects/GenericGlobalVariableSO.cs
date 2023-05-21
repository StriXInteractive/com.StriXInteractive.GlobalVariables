using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StriXInteractive.Tools.GlobalVariables {
    [Serializable]
    public class GenericGlobalVariableSO<T> : ScriptableObject, ISerializationCallbackReceiver {

        [SerializeField] private T initialValue;

        [Header("Editor Only")]
        [SerializeField] private bool saveDuringPlayMode;

        [NonSerialized] public UnityAction OnValueChanged;

        private T runtimeValue;
        public T RuntimeValue {
            get { return runtimeValue; }
        }

        public virtual T Value {
            get { return runtimeValue; }
            set {

                T oldValue = runtimeValue;

                runtimeValue = value;

                if (!EqualityComparer<T>.Default.Equals(oldValue, runtimeValue)) {
                    RaiseEvent();
                }
            }
        }

        public void RaiseEvent() {

            if (OnValueChanged == null) {
                return;
            }

            OnValueChanged?.Invoke();
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