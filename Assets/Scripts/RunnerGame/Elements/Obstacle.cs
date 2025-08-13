using UnityEngine;

namespace RunnerGame.Elements {
    public class Obstacle : MonoBehaviour
    {
        private Vector2Int gridPosition;

        public void Initialize(Vector2Int gridPosition) {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }

        public Vector2Int GetGridPosition() => gridPosition;
    }
}
