using UnityEngine;

namespace Board
{
    public class BoardPiece : MonoBehaviour
    {
        [SerializeField] private Vector2Int position;
        public Vector2Int Position
        {
            get => position;
            set
            {
                position = value;
                transform.position = new Vector3(value.x, value.y, 0f);
            }
        }

        public virtual void CollideWith(GameObject other)
        {
        }
    }
}