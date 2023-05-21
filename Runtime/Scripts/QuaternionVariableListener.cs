using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace StriXInteractive.Tools.GlobalVariables {
    public class QuaternionVariableListener : MonoBehaviour {

        [SerializeField] private QuaternionVariableSO globalVariable;
        [SerializeField] private UnityEvent<Quaternion> response;
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
