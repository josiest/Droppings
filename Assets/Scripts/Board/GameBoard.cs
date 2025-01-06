using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Board
{
    public class GameBoard : MonoBehaviour, ITickable
    {
        public const string PlayerTag = "Player";

        /** The dimensions of the board */
        [SerializeField] private RectInt dimensions = new(-5, -5, 10, 10);
        public RectInt Dimensions => dimensions;

        public IEnumerable<BoardPiece> Pieces => pieces;
        private readonly List<BoardPiece> pieces = new();
        
        private readonly HashSet<BoardPiece> dirtyCache = new();

        private void Awake()
        {
            GameBoardSystem.RegisterBoard(this);
            pieces.AddRange(FindObjectsOfType<BoardPiece>());
            // TODO: Create ScalableBoardBackground component to manage this scaling behavior
            // backgroundSprite.transform.localScale = new Vector3(Dimensions.width, Dimensions.height, 1f);
        }

        private void Start()
        {
            GameBoardSystem.Find<TickSystem>()?.AddTickable(this);
        }

        public void Tick()
        {
            ClearDirtyCache();
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
            return AllPiecesWithTag(searchTag).First();
        }
        public IEnumerable<BoardPiece> AllPiecesWithTag(string searchTag)
        {
            return pieces.Where(piece => piece.CompareTag(searchTag) && !dirtyCache.Contains(piece));
        }

        public void RemoveImmediateByTag(string searchTag)
        {
            RemoveByTag(searchTag);
            pieces.RemoveAll(piece => dirtyCache.Contains(piece));
            foreach (var piece in dirtyCache) { DestroyImmediate(piece.gameObject); }
            dirtyCache.Clear();
        }
        public void RemoveByTag(string searchTag)
        {
            dirtyCache.UnionWith(pieces.Where(piece => piece.CompareTag(searchTag)));
        }
        public void RemoveRange(IEnumerable<BoardPiece> piecesToRemove)
        {
            dirtyCache.UnionWith(piecesToRemove);
        }
        public void RemovePiece(BoardPiece pieceToRemove)
        {
            dirtyCache.Add(pieceToRemove);
        }
        public IEnumerable<BoardPiece> RandomPiecesWithTag(string searchTag, int amount)
        {
            var candidates = AllPiecesWithTag(searchTag).ToList();
            var boardPieces = new List<BoardPiece>();
            for (int i = 0; i < amount; ++i)
            {
                if (candidates.Count == 0) { break; }

                int index = Random.Range(0, candidates.Count);
                boardPieces.Add(candidates[index]);
                candidates.RemoveAt(index);
            }
            return boardPieces;
        }
        public bool HasCollision(Vector2Int pos)
        {
            return pieces.Any(piece => piece.Position == pos);
        }

        public Vector2Int RandomOpenSpace()
        {
            var pos = RandomSpace();
            for (int i = 0; i < Dimensions.width * Dimensions.height; i++)
            {
                if (!HasCollision(pos)) { return pos; }
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

        private void ClearDirtyCache()
        {
            pieces.RemoveAll(piece => dirtyCache.Contains(piece));
            foreach (var piece in dirtyCache) { Destroy(piece.gameObject); }
            dirtyCache.Clear();
        }

        private BoardPiece FindRandomPiece()
        {
            return pieces[Random.Range(0, pieces.Count)];
        }
    }
}