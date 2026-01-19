using UnityEngine;

namespace ChukStuph.Input
{
    [CreateAssetMenu(fileName = "Float Input", menuName = "Chuk Essentials/Input/Float Input")]
    public class FloatInputReactionSO : InputReactionValueSO<float>
    {
        protected override bool CheckDeadzone(float val)
        {
            return Mathf.Abs(val) < deadzone;
        }
    }
}