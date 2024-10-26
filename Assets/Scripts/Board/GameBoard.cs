using System.Collections.Generic;
using System.Linq;
using Subsystems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board
{
    [RequireComponent(typeof(TickSystem))]
    public class GameBoard : SceneSubsystem, ITickable
    {

        [SerializeField] private GameObject backgroundSprite;
        /** The dimensions of the board */
        [SerializeField] private RectInt dimensions = new(-5, -5, 10, 10);
        public RectInt Dimensions => dimensions;

        private readonly List<BoardPiece> pieces = new();
        private readonly HashSet<BoardPiece> dirtyCache = new();

        private void Awake()
        {
            pieces.AddRange(FindObjectsOfType<BoardPiece>());
            backgroundSprite.transform.localScale = new Vector3(Dimensions.width, Dimensions.height, 1f);
        }

        private void Start()
        {
            GetComponent<TickSystem>().AddTickable(this);
        }

        public void Tick()
        {
            foreach (var playerPiece in pieces.Where(piece => piece.CompareTag("Player")))
            {
                foreach (var other in pieces.Where(other => !other.CompareTag("Player"))
                                             .Where(other => playerPiece.Position == other.Position))
                {
                    other.CollideWith(playerPiece);
                }
            }
            pieces.RemoveAll(piece => dirtyCache.Contains(piece));
            foreach (var piece in dirtyCache) { Destroy(piece.gameObject); }
            dirtyCache.Clear();
        }

        public T CreatePiece<T>(GameObject prefab) where T : BoardPiece
        {
            var piece = Instantiate(prefab, transform).GetComponent<T>();
            pieces.Add(piece);
            return piece;
        }

        public T CreatePiece<T>(GameObject prefab, Transform parent) where T : BoardPiece
        {
            var piece = Instantiate(prefab, parent).GetComponent<T>();
            pieces.Add(piece);
            return piece;
        }

        public T CreatePiece<T>(GameObject prefab, Vector2Int pos) where T: BoardPiece
        {
            var piece = CreatePiece<T>(prefab);
            piece.Position = pos;
            return piece;
        }

        public T CreatePiece<T>(GameObject prefab, Vector2Int pos, Transform parent) where T: BoardPiece
        {
            var piece = CreatePiece<T>(prefab, parent);
            piece.Position = pos;
            return piece;
        }

        public void AddPiece(BoardPiece piece)
        {
            pieces.Add(piece);
        }

        public BoardPiece FindPieceByTag(string searchTag)
        {
            return pieces.First(piece => piece.CompareTag(searchTag));
        }

        public void RemovePiece(BoardPiece piece)
        {
            pieces.Remove(piece);
            Destroy(piece.gameObject);
        }
        public bool HasCollision(Vector2Int pos)
        {
            return pieces.Any(piece => piece.Position == pos);
        }

        public Vector2Int RandomOpenSpace()
        {
            var pos = RandomSpace();
            while (HasCollision(pos))
            {
                pos = RandomSpace();
            }
            return pos;
        }

        public Vector2Int RandomSpace()
        {
            var x = Random.Range(Dimensions.x, Dimensions.x + Dimensions.width);
            var y = Random.Range(Dimensions.y, Dimensions.y + Dimensions.height);
            return new Vector2Int(x, y);
        }

        public void ClearByTag(string clearTag)
        {
            dirtyCache.UnionWith(pieces.Where(piece => piece.CompareTag(clearTag)));
        }
    }
}