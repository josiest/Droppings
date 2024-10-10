using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board
{
    public class GameBoard : Tickable
    {
        /** The dimensions of the board */
        [SerializeField] private RectInt dimensions = new(-5, -5, 10, 10);
        public RectInt Dimensions => dimensions;

        private readonly List<BoardPiece> _pieces = new();
        private readonly HashSet<BoardPiece> _dirtyCache = new();

        public static GameBoard Instance { get; private set; }
        public void Awake()
        {
            Instance = this;
            _pieces.AddRange(FindObjectsOfType<GameObject>(gameObject)
                                .Select(obj => obj.GetComponent<BoardPiece>())
                                .Where(obj => obj != null));
        }
        public void OnDestroy()
        {
            Instance = null;
        }

        public override void Tick()
        {
            foreach (var playerPiece in _pieces.Where(piece => piece.CompareTag("Player")))
            {
                foreach (var other in _pieces.Where(other => !other.CompareTag("Player"))
                                             .Where(other => playerPiece.Position == other.Position))
                {
                    other.CollideWith(playerPiece);
                }
            }
            _pieces.RemoveAll(piece => _dirtyCache.Contains(piece));
            foreach (var piece in _dirtyCache) { Destroy(piece.gameObject); }
            _dirtyCache.Clear();
        }
        
        public GameObject CreatePiece(GameObject prefab, Vector2Int pos)
        {
            var obj = Instantiate(prefab, transform);
            var piece = obj.GetComponent<BoardPiece>();
            piece.Position = pos;
            _pieces.Add(piece);
            return obj;
        }

        public GameObject CreatePiece(GameObject prefab, Vector2Int pos, Transform parent)
        {
            var obj = Instantiate(prefab, parent);
            var piece = obj.GetComponent<BoardPiece>();
            piece.Position = pos;
            _pieces.Add(piece);
            return obj;
        }

        public void AddPiece(BoardPiece piece)
        {
            _pieces.Add(piece);
        }

        public BoardPiece FindPieceByTag(string searchTag)
        {
            return _pieces.First(piece => piece.CompareTag(searchTag));
        }

        public void RemovePiece(BoardPiece piece)
        {
            _pieces.Remove(piece);
            Destroy(piece.gameObject);
        }
        public bool HasCollision(Vector2Int pos)
        {
            return _pieces.Any(piece => piece.Position == pos);
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
            _dirtyCache.UnionWith(_pieces.Where(piece => piece.CompareTag(clearTag)));
        }
    }
}