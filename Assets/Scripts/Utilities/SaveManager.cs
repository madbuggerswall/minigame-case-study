using System.IO;
using UnityEngine;

namespace Utilities {
	// TODO Rename this class (JSON)
	public static class SaveManager {
		public static void Save<T>(T serializable, string filePath) {
			string json = JsonUtility.ToJson(serializable, false);
			Debug.Log(json);

			using FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
			using BinaryWriter binaryWriter = new BinaryWriter(file);
			binaryWriter.Write(json);
		}

		public static T Load<T>(string filePath) {
			string objectAsJSON;

			using (FileStream file = File.Open(filePath, FileMode.Open)) {
				using (BinaryReader binaryReader = new BinaryReader(file)) {
					objectAsJSON = binaryReader.ReadString();
				}
			}

			return JsonUtility.FromJson<T>(objectAsJSON);
		}

		public static void Overwrite<T>(T objectToOverwrite, string filePath) {
			string objectAsJSON;

			using (FileStream file = File.Open(filePath, FileMode.Open)) {
				using (BinaryReader binaryReader = new BinaryReader(file)) {
					objectAsJSON = binaryReader.ReadString();
				}
			}

			JsonUtility.FromJsonOverwrite(objectAsJSON, objectToOverwrite);
		}

		public static bool Exists(string filePath) {
			return File.Exists(filePath);
		}
	}
}