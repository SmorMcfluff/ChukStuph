using UnityEngine;

namespace ChukStuph.GameSettings
{
    [CreateAssetMenu(fileName = "String Setting", menuName = "Chuk Essentials/Settings/String Setting")]
    public class StringSettingValueSO : SettingValueSO<string>
    {
        public override string Value
        {
            get => base.Value;
            set => base.Value = value;
        }

        public override void ResetToDefault()
        {
            Value = Default;
        }

        protected override string ConvertToString(string value) => value;
        protected override string ConvertFromString(string str) => str;
    }
}