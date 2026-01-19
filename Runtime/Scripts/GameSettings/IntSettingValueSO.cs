using UnityEngine;

namespace ChukStuph.GameSettings
{
    [CreateAssetMenu(fileName = "Integer Setting", menuName = "Chuk Essentials/Settings/Integer Setting")]
    public class IntSettingValueSO : SettingValueSO<int>
    {
        [Header("Optional Range")]
        [SerializeField] private bool useRange = false;
        [SerializeField] private int minValue = 0;
        [SerializeField] private int maxValue = 1;

        public bool HasRange => useRange;
        public int Min => minValue;
        public int Max => maxValue;

        public override int Value
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

        protected override string ConvertToString(int value) => value.ToString("R");
        protected override int ConvertFromString(string str) => int.TryParse(str, out int result)
            ? result
            : Default;
    }
}