using UnityEngine;

namespace ChukStuph.GameSettings
{
    [CreateAssetMenu(fileName = "Float Setting", menuName = "Chuk Essentials/Settings/Float Setting")]
    public class FloatSettingValueSO : SettingValueSO<float>
    {
        [Header("Optional Range")]
        [SerializeField] private bool useRange = false;
        [SerializeField] private float minValue = 0f;
        [SerializeField] private float maxValue = 1f;

        public bool HasRange => useRange;
        public float Min => minValue;
        public float Max => maxValue;

        public override float Value
        {
            get => base.Value;
            set => base.Value = useRange 
                ? Mathf.Clamp(value, minValue, maxValue) 
                : value;
        }

        public override void ResetToDefault()
        {
            Value = useRange ?
                Mathf.Clamp(Default, minValue, maxValue)
                : Default;
        }

        protected override string ConvertToString(float value) => value.ToString("R");
        protected override float ConvertFromString(string str) => float.TryParse(str, out float result) 
            ? result 
            : Default;
    }
}