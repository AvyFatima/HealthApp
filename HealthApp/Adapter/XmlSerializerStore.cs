using System;
using System.IO;
using System.Xml.Serialization;

namespace HealthApp.Adapter;

/// <summary>
/// Persists objects as XML using <see cref="XmlSerializer"/>.
/// </summary>
public sealed class XmlSerializerStore
{
	private readonly string _filePath;

	/// Initializes a new instance of the <see cref="XmlSerializerStore"/> class with the specified file path for storing XML data.
	/// <param name="filePath">The file path where XML data will be stored.</param>
	/// <exception cref="ArgumentException">Thrown when the provided file path is null, empty, or consists only of white-space characters.</exception>
	public XmlSerializerStore(string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath))
		{
			throw new ArgumentException("A file path is required.", nameof(filePath));
		}

		_filePath = filePath;
	}

	public string FilePath => _filePath;

	/// Saves the specified object to the XML file.
	/// <typeparam name="T">The type of the object to save.</typeparam>
	/// <param name="value">The object to save.</param>
	/// <exception cref="ArgumentNullException">Thrown when the provided value is null.</exception
	public void Save<T>(T value)
	{
        var directory = Path.GetDirectoryName(Path.GetFullPath(_filePath));
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);
		
        if (File.Exists(_filePath))
			File.Delete(_filePath);
			
        var serializer = new XmlSerializer(typeof(T));
		using var stream = File.Create(_filePath);
		serializer.Serialize(stream, value);
	}

	/// Loads an object of the specified type from the XML file.
	/// <typeparam name="T">The type of the object to load.</typeparam>	
	/// <returns>The deserialized object of type <typeparamref name="T"/> if the file exists; otherwise, returns the default value of <typeparamref name="T"/>.</returns>
	public T? Load<T>()
	{
		if (!File.Exists(_filePath))
		{
			return default;
		}

		var serializer = new XmlSerializer(typeof(T));
		using var stream = File.OpenRead(_filePath);
		return (T?)serializer.Deserialize(stream);
	}
}
