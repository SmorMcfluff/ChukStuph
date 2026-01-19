using UnityEngine;

namespace ChukStuph.Gameplay
{
    public class GroundCheck : MonoBehaviour
    {
        public bool use2D = true;
        public Vector3 boxSize = new(1f, 0.1f, 1f);
        [SerializeField] private LayerMask groundLayer;

        /// <summary>
        /// Checks if the ground is detected within the box.
        /// Optional: provide a LayerMask to override the default.
        /// </summary>
        public bool IsGrounded(LayerMask? layerToCheck = null)
        {
            LayerMask mask = layerToCheck ?? groundLayer;

            if (use2D)
            {
                Vector2 size2D = new Vector2(boxSize.x, boxSize.y);
                return Physics2D.OverlapBox((Vector2)transform.position, size2D, 0f, layerToCheck) != null;
            }
            else
            {
                return Physics.CheckBox(transform.position, boxSize * 0.5f, Quaternion.identity, layerToCheck);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (use2D)
                Gizmos.DrawWireCube(transform.position, new Vector3(boxSize.x, boxSize.y, 0f));
            else
                Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}