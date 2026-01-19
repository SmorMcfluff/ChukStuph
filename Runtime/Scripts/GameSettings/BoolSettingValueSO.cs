using UnityEngine;

namespace ChukStuph.GameSettings
{
    [CreateAssetMenu(fileName = "Bool Setting", menuName = "Chuk Essentials/Settings/Bool Setting")]
    public class BoolSettingValueSO : SettingValueSO<bool>
    {
        public override bool Value
        {
            get => base.Value;
            set => base.Value = value;
        }

        public override void ResetToDefault()
        {
            Value = Default;
        }

        protected override string ConvertToString(bool value)
        {
            return value ? "1" : "0";
        }

        protected override bool ConvertFromString(string str)
        {
            if (str == "1") return true;
            if (str == "0") return false;

            return Default;
        }

    }
}