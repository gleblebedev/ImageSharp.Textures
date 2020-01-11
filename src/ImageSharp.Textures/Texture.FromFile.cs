// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using SixLabors.ImageSharp.Textures.Formats;

namespace SixLabors.ImageSharp.Textures
{
    /// <content>
    /// Adds static methods allowing the creation of new image from a given file.
    /// </content>
    public partial class Texture
    {
        /// <summary>
        /// By reading the header on the provided file this calculates the images mime type.
        /// </summary>
        /// <param name="filePath">The image file to open and to read the header from.</param>
        /// <returns>The mime type or null if none found.</returns>
        public static ITextureFormat DetectFormat(string filePath)
        {
            return DetectFormat(Configuration.Default, filePath);
        }

        /// <summary>
        /// By reading the header on the provided file this calculates the images mime type.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="filePath">The image file to open and to read the header from.</param>
        /// <returns>The mime type or null if none found.</returns>
        public static ITextureFormat DetectFormat(Configuration config, string filePath)
        {
            config = config ?? Configuration.Default;
            using (Stream file = config.FileSystem.OpenRead(filePath))
            {
                return DetectFormat(config, file);
            }
        }

        /// <summary>
        /// Create a new instance of the <see cref="Texture"/> class from the given file.
        /// </summary>
        /// <param name="path">The file path to the image.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        /// <returns>The <see cref="Texture"/>.</returns>
        public static Texture Load(string path) => Load(Configuration.Default, path);

        /// <summary>
        /// Create a new instance of the <see cref="Texture"/> class from the given file.
        /// </summary>
        /// <param name="path">The file path to the image.</param>
        /// <param name="format">The mime type of the decoded image.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        /// <returns>A new <see cref="Texture"/>.</returns>
        public static Texture Load(string path, out ITextureFormat format) => Load(Configuration.Default, path, out format);

        /// <summary>
        /// Create a new instance of the <see cref="Texture"/> class from the given file.
        /// </summary>
        /// <param name="config">The config for the decoder.</param>
        /// <param name="path">The file path to the image.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        /// <returns>The <see cref="Texture"/>.</returns>
        public static Texture Load(Configuration config, string path) => Load(config, path, out _);

        /// <summary>
        /// Create a new instance of the <see cref="Texture"/> class from the given file.
        /// </summary>
        /// <param name="config">The Configuration.</param>
        /// <param name="path">The file path to the image.</param>
        /// <param name="decoder">The decoder.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        /// <returns>The <see cref="Texture"/>.</returns>
        public static Texture Load(Configuration config, string path, ITextureDecoder decoder)
        {
            using (Stream stream = config.FileSystem.OpenRead(path))
            {
                return Load(config, stream, decoder);
            }
        }

        /// <summary>
        /// Create a new instance of the <see cref="Texture"/> class from the given file.
        /// </summary>
        /// <param name="path">The file path to the image.</param>
        /// <param name="decoder">The decoder.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        /// <returns>The <see cref="Texture"/>.</returns>
        public static Texture Load(string path, ITextureDecoder decoder) => Load(Configuration.Default, path, decoder);

        /// <summary>
        /// Create a new instance of the <see cref="Texture"/> class from the given file.
        /// The pixel type is selected by the decoder.
        /// </summary>
        /// <param name="config">The configuration options.</param>
        /// <param name="path">The file path to the image.</param>
        /// <param name="format">The mime type of the decoded image.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        public static Texture Load(Configuration config, string path, out ITextureFormat format)
        {
            using (Stream stream = config.FileSystem.OpenRead(path))
            {
                return Load(config, stream, out format);
            }
        }
    }
}