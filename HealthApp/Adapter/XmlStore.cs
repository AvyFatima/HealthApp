using System;
using System.IO;
using System.Xml.Serialization;
using HealthApp.Interfaces;

namespace HealthApp.Adapter
{

	/// <summary>
	/// Encapsulates XML serialization and file access for persisted application data.
	/// </summary>
	public sealed class XmlStore<T> : IXmlStore<T>
	{
		private readonly string _filePath;

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlStore"/> class.
		/// </summary>
		/// <param name="filePath">The path to the XML file.</param>
		public XmlStore(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				throw new ArgumentException("A file path is required.", nameof(filePath));

			_filePath = filePath;
		}

		/// <summary>
		/// Saves the specified items to the XML file.
		/// </summary>
		/// <param name="items">The items to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the  operation.</returns>
		public Task SaveAsync(
            IReadOnlyList<T> items,
            CancellationToken cancellationToken = default)
        {
			var directory = Path.GetDirectoryName(Path.GetFullPath(_filePath));
			if (!string.IsNullOrEmpty(directory))
				Directory.CreateDirectory(directory);

			cancellationToken.ThrowIfCancellationRequested();
			using var stream = File.Create(_filePath);
			var serializer = new XmlSerializer(typeof(List<T>));
			serializer.Serialize(stream, items.ToList());
			return Task.CompletedTask;
		}

		/// <summary>
		/// Loads the items from the XML file.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public Task<IReadOnlyList<T>> LoadAsync(
            CancellationToken cancellationToken = default)
        {
			cancellationToken.ThrowIfCancellationRequested();
			if (!File.Exists(_filePath))
				return Task.FromResult<IReadOnlyList<T>>(Array.Empty<T>());

			var serializer = new XmlSerializer(typeof(List<T>));
			using var stream = File.OpenRead(_filePath);
			var value = serializer.Deserialize(stream);
			return Task.FromResult(value as IReadOnlyList<T> ?? Array.Empty<T>());
		}
	}

}
