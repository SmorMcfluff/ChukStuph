using UnityEngine;

namespace ChukStuph.Input
{
    /// <summary>
    /// Calls Tick() on all bound InputReactionSO assets each frame.
    /// Handles subscribing and unsubscribing UnityEvents for buttons and value inputs.
    /// </summary>
    [DisallowMultipleComponent]
    public class InputBinder : MonoBehaviour
    {
        [Tooltip("Assign InputReactionSO assets and link UnityEvents to scene actions.")]
        [SerializeField] private BoundInput[] bindings;

        private void OnEnable()
        {
            foreach (var bind in bindings)
            {
                if (bind.input == null || bind.input.action?.action == null) continue;

                bind.input.Enable();

                if (bind.input is InputReactionButtonSO btn)
                {
                    if (bind.onPressed != null) btn.OnPressed += bind.onPressed.Invoke;
                    if (bind.onHeld != null) btn.OnHeld += bind.onHeld.Invoke;
                    if (bind.onReleased != null) btn.OnReleased += bind.onReleased.Invoke;
                }

                else if (bind.input is InputReactionValueSO<float> floatInput)
                {
                    if (bind.onValueChangedFloat != null)
                        floatInput.OnValueChanged += bind.onValueChangedFloat.Invoke;

                    if (bind.getValueFloat != null)
                        floatInput.GetValue += bind.getValueFloat.Invoke;
                }

                else if (bind.input is InputReactionValueSO<Vector2> vecInput)
                {
                    if (bind.onValueChangedVector2 != null)
                        vecInput.OnValueChanged += bind.onValueChangedVector2.Invoke;

                    if (bind.getValueVector2 != null)
                        vecInput.GetValue += bind.getValueVector2.Invoke;
                }
            }
        }

        private void OnDisable()
        {
            foreach (var bind in bindings)
            {
                if (bind.input == null || bind.input.action?.action == null) continue;

                bind.input.Disable();

                if (bind.input is InputReactionButtonSO btn)
                {
                    if (bind.onPressed != null) btn.OnPressed -= bind.onPressed.Invoke;
                    if (bind.onHeld != null) btn.OnHeld -= bind.onHeld.Invoke;
                    if (bind.onReleased != null) btn.OnReleased -= bind.onReleased.Invoke;
                }

                else if (bind.input is InputReactionValueSO<float> floatInput)
                {
                    if (bind.onValueChangedFloat != null)
                        floatInput.OnValueChanged -= bind.onValueChangedFloat.Invoke;

                    if (bind.getValueFloat != null)
                        floatInput.GetValue -= bind.getValueFloat.Invoke;
                }

                else if (bind.input is InputReactionValueSO<Vector2> vecInput)
                {
                    if (bind.onValueChangedVector2 != null)
                        vecInput.OnValueChanged -= bind.onValueChangedVector2.Invoke;

                    if (bind.getValueVector2 != null)
                        vecInput.GetValue -= bind.getValueVector2.Invoke;
                }
            }
        }

        private void Update()
        {
            foreach (var bind in bindings)
            {
                bind.input?.Tick(Time.deltaTime);
            }
        }
    }
}