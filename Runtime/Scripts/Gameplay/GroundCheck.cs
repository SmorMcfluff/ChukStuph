using UnityEngine;

namespace ChukStuph.Gameplay
{
    public class GroundCheck : MonoBehaviour
    {
        public bool use2D = true;
        [SerializeField] private Vector3 boxSize = new(1f, 0.1f, 1f);
        [SerializeField] private LayerMask groundLayer;

        public bool IsGrounded()
        {
            if (use2D)
            {
                Vector2 size2D = new Vector2(boxSize.x, boxSize.y);
                return Physics2D.OverlapBox((Vector2)transform.position, size2D, 0f, groundLayer) != null;
            }
            else
            {
                return Physics.CheckBox(transform.position, boxSize * 0.5f, Quaternion.identity, groundLayer);
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