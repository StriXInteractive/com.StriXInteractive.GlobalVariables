using UnityEngine;

namespace StriXInteractive.Tools.GlobalVariables {
    [CreateAssetMenu(fileName = "FloatVariableSO", menuName = "StriX Interactive/Variables/FloatVariableSO")]
    public class FloatVariableSO : GenericGlobalVariableSO<float> {

        [Header("Extra Settings")]
        [SerializeField] private bool useClampValue;

        public float minValue;
        public float MinValue {
            get { return minValue; }

            set {
                minValue = value;

                if (useClampValue) {
                    if (Value < minValue) {
                        Value = minValue;
                    }
                }
            }
        }

        public float maxValue;
        public float MaxValue {
            get { return maxValue; }

            set {
                maxValue = value;

                if (useClampValue) {
                    if (Value > maxValue) {
                        Value = maxValue;
                    }
                }
            }
        }

        public override float Value {
            get { return RuntimeValue; }

            set {

                if (useClampValue) {
                    if (value < minValue) {
                        value = minValue;
                    } else if (value > maxValue) {
                        value = maxValue;
                    }
                }

                base.Value = value;
            }
        }
    }
}
