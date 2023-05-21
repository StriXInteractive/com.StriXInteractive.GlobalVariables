using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StriXInteractive.Tools.GlobalVariables {
    [CustomEditor(typeof(GenericGlobalVariableSO<>), true)]
    public abstract class GenericGlobalVariableSOEditor<T> : Editor {

        private GenericGlobalVariableSO<T> globalVariable;

        private void OnEnable() {
            if (globalVariable == null) {
                globalVariable = target as GenericGlobalVariableSO<T>;
            }
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Listeners:", EditorStyles.boldLabel);

            ShowListenersListView();

            EditorGUILayout.Space();

            if (GUILayout.Button("Raise Event")) {
                globalVariable.RaiseEvent();
            }
        }

        private void ShowListenersListView() {
            List<MonoBehaviour> listeners = GetListeners();

            foreach (var listener in listeners) {
                if (listener == null)
                    continue;

                string combinedName = listener.gameObject.name + " (" + listener.GetType().Name + ")";
                EditorGUILayout.LabelField(combinedName);

                if (GUILayout.Button("Ping")) {
                    EditorGUIUtility.PingObject(listener.gameObject);
                }
            }
        }

        private List<MonoBehaviour> GetListeners() {
            List<MonoBehaviour> listeners = new List<MonoBehaviour>();

            if (globalVariable == null || globalVariable.OnValueChanged == null) {
                return listeners;
            }

            var delegateSubscribers = globalVariable.OnValueChanged.GetInvocationList();
            foreach (var subscriber in delegateSubscribers) {

                var componentListener = subscriber.Target as MonoBehaviour;
                if (!listeners.Contains(componentListener)) {
                    listeners.Add(componentListener);
                }
            }

            return listeners;
        }
    }
}
