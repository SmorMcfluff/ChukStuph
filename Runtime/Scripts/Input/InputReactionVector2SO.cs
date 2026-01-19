using UnityEngine;

namespace ChukStuph.Input
{
    [CreateAssetMenu(fileName = "Vector2 Input", menuName = "Chuk Essentials/Input/Vector2 Input")]
    public class Vector2InputReactionSO : InputReactionValueSO<Vector2>
    {
        protected override bool CheckDeadzone(Vector2 val)
        {
            return val.sqrMagnitude < deadzone * deadzone;
        }
    }
}