using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace StriXInteractive.Tools.GlobalVariables {
    public class Vector4VariableListener : MonoBehaviour {

        [SerializeField] private Vector4VariableSO globalVariable;
        [SerializeField] private UnityEvent<Vector4> response;
        [SerializeField] private float delay;

        private void OnEnable() {
            if (globalVariable != null) {
                globalVariable.OnValueChanged += OnValueChanged;
            }
        }

        private void OnDisable() {
            if (globalVariable != null) {
                globalVariable.OnValueChanged -= OnValueChanged;
            }
        }

        private void OnValueChanged() {
            StartCoroutine(RaiseEventDelayed(delay));
        }

        private IEnumerator RaiseEventDelayed(float delay) {
            yield return new WaitForSeconds(delay);
            response.Invoke(globalVariable.Value);
        }
    }
}
