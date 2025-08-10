using UnityEngine;

namespace Utilities {
	public static class VectorExtensionMethods {
		public static Vector2 GetXY(this Vector3 vector) {
			return new Vector2(vector.x, vector.y);
		}

		public static Vector3 WithX(this Vector3 vector, float x) {
			return new Vector3(x, vector.y, vector.z);
		}

		public static Vector3 WithY(this Vector3 vector, float y) {
			return new Vector3(vector.x, y, vector.z);
		}

		public static Vector3 WithZ(this Vector3 vector, float z) {
			return new Vector3(vector.x, vector.y, z);
		}

		public static Vector2 WithX(this Vector2 vector, float x) {
			return new Vector2(x, vector.y);
		}

		public static Vector2 WithY(this Vector2 vector, float y) {
			return new Vector2(vector.x, y);
		}

		public static Vector3 WithZ(this Vector2 vector, float z) {
			return new Vector3(vector.x, vector.y, z);
		}


		public static Vector2Int GetXY(this Vector3Int vector) {
			return new Vector2Int(vector.x, vector.y);
		}

		public static Vector3Int WithX(this Vector3Int vector, int x) {
			return new Vector3Int(x, vector.y, vector.z);
		}

		public static Vector3Int WithY(this Vector3Int vector, int y) {
			return new Vector3Int(vector.x, y, vector.z);
		}

		public static Vector3Int WithZ(this Vector3Int vector, int z) {
			return new Vector3Int(vector.x, vector.y, z);
		}

		public static Vector2Int WithX(this Vector2Int vector, int x) {
			return new Vector2Int(x, vector.y);
		}

		public static Vector2Int WithY(this Vector2Int vector, int y) {
			return new Vector2Int(vector.x, y);
		}

		public static Vector3Int WithZ(this Vector2Int vector, int z) {
			return new Vector3Int(vector.x, vector.y, z);
		}
	}
}
