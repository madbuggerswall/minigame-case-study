using System.IO;
using UnityEngine;

namespace Utilities {
	// TODO Rename this class (BinaryJson+Writer/Utils)
	public static class BinaryJsonUtility {
		public static void Save<T>(T serializable, string filePath) {
			string json = JsonUtility.ToJson(serializable, false);
			Debug.Log(json);

			using FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
			using BinaryWriter binaryWriter = new BinaryWriter(file);
			binaryWriter.Write(json);
		}

		public static T Load<T>(string filePath) {
			string objectAsJson = ReadBinaryJsonFile<T>(filePath);
			return JsonUtility.FromJson<T>(objectAsJson);
		}

		public static void Overwrite<T>(T overwrittenObject, string filePath) {
			string objectAsJson = ReadBinaryJsonFile<T>(filePath);
			JsonUtility.FromJsonOverwrite(objectAsJson, overwrittenObject);
		}

		private static string ReadBinaryJsonFile<T>(string filePath) {
			using FileStream file = File.Open(filePath, FileMode.Open);
			using BinaryReader binaryReader = new(file);

			return binaryReader.ReadString();
		}

		public static bool Exists(string filePath) {
			return File.Exists(filePath);
		}
	}
}
