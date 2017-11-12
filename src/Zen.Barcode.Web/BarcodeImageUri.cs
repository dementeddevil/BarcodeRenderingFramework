//-----------------------------------------------------------------------
// <copyright file="BarcodeImageUri.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// <para>
    /// This class is used to generate URIs which when requested by the client
    /// will result in a barcode image being streamed from the database as a
    /// standard JPEG file.
    /// </para>
    /// <para>
    /// For all of this to work the following line must be added to web.config
    /// in the httpHandler section
    /// <![CDATA[
    /// <add verb="GET" path="*.Barcode" type="Zen.Barcode.Web.BarcodeImageHandler, Zen.Barcode.Web, Culture=Neutral, Version=2.0.3.0, PublicKeyToken=b5ae55aa76d2d9de" />
    /// ]]>
    /// The .Barcode file extension will need to be associated with ASP.NET
    /// from within IIS and the "Check if the file exists" checkbox must be cleared.
    /// </para>
    /// </summary>
    public class BarcodeImageUri
    {
        #region Private Fields
        private string _fileName;

        private string _text;

        private BarcodeSymbology _encodingScheme;
        private int _barMinHeight = 30;
        private int _barMaxHeight = 30;
        private int _barMinWidth = 1;
        private int _barMaxWidth = 1;

        private static object syncParser = new object();
        private static Regex _filenameParser;
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeImageUri"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public BarcodeImageUri(Uri uri)
        {
            // Extract filename from URI
            string path = uri.AbsolutePath;
            string fileName = path;
            int index = path.LastIndexOf('/');
            if (index != -1)
            {
                fileName = path.Substring(index + 1);
            }
            _fileName = fileName;

            // Decode the filename
            DecodeFileName();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeImageUri"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public BarcodeImageUri(string fileName)
        {
            // Cache the filename
            _fileName = fileName;

            // Decode filename
            DecodeFileName();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return _text;
            }
        }

        /// <summary>
        /// Gets the encoding scheme.
        /// </summary>
        /// <value>The encoding scheme.</value>
        public BarcodeSymbology EncodingScheme
        {
            get
            {
                return _encodingScheme;
            }
        }

        /// <summary>
        /// Gets the height of the bar min.
        /// </summary>
        /// <value>The height of the bar min.</value>
        public int BarMinHeight
        {
            get
            {
                return _barMinHeight;
            }
        }

        /// <summary>
        /// Gets the height of the bar max.
        /// </summary>
        /// <value>The height of the bar max.</value>
        public int BarMaxHeight
        {
            get
            {
                return _barMaxHeight;
            }
        }

        /// <summary>
        /// Gets the width of the bar min.
        /// </summary>
        /// <value>The width of the bar min.</value>
        public int BarMinWidth
        {
            get
            {
                return _barMinWidth;
            }
        }

        /// <summary>
        /// Gets the width of the bar max.
        /// </summary>
        /// <value>The width of the bar max.</value>
        public int BarMaxWidth
        {
            get
            {
                return _barMaxWidth;
            }
        }
        #endregion

        #region Private Properties
        private static Regex FileNameParser
        {
            get
            {
                if (_filenameParser == null)
                {
                    lock (syncParser)
                    {
                        if (_filenameParser == null)
                        {
                            // Create filename parser
                            _filenameParser = new Regex(
                                @"^(?<OriginalFilename>(?:
                                (?:Barcode)(?:\x5b\s*
                                (?<EncodingSystem>[0-9]*)\s*,\s*
                                (?<BarMinHeight>[0-9]*)\s*,\s*
                                (?<BarMaxHeight>[0-9]*)\s*,\s*
                                (?<BarMinWidth>[0-9]*)\s*,\s*
                                (?<BarMaxWidth>[0-9]*)\s*\x5d)?:
                                (?<BarCodePayload>[0-9A-Z-.$/+%]*))):
                                (?<HashCode>(?:[-])?[0-9]+)$",
                                RegexOptions.Singleline | RegexOptions.Compiled |
                                RegexOptions.IgnorePatternWhitespace);
                        }
                    }
                }
                return _filenameParser;
            }
        }
        #endregion

        #region Private Methods
        private void DecodeFileName()
        {
            string fileName = Path.GetFileNameWithoutExtension(_fileName);

            // Remove file extension and convert from base64
            byte[] encryptedFileName = HttpServerUtility.UrlTokenDecode(
                fileName);

            // Create memory stream backed against encrypted form
            MemoryStream memStm = new MemoryStream(encryptedFileName);

            // Read from crypto-stream via stream reader in UTF8
            StreamReader reader = new StreamReader(memStm, Encoding.UTF8);
            fileName = reader.ReadToEnd();

            // Lets see if we can parse the string
            Match m = FileNameParser.Match(fileName);
            if (!m.Success)
            {
                throw new InvalidOperationException("Filename is not valid.");
            }

            // Validate the hash-code.
            string originalFilename = m.Result("${OriginalFilename}");
            string hashCode = m.Result("${HashCode}");
            if (originalFilename.GetHashCode() != Int32.Parse(hashCode))
            {
                throw new InvalidOperationException("Filename is not valid.");
            }

            // Determine encoding system
            string encoding = m.Result("${EncodingSystem}");
            if (!string.IsNullOrEmpty(encoding))
            {
                _encodingScheme = (BarcodeSymbology) Int32.Parse(encoding);
            }

            // Determine barcode height
            string barMinHeight = m.Result("${BarMinHeight}");
            if (!string.IsNullOrEmpty(barMinHeight))
            {
                _barMinHeight = Int32.Parse(barMinHeight);
            }
            string barMaxHeight = m.Result("${BarMaxHeight}");
            if (!string.IsNullOrEmpty(barMaxHeight))
            {
                _barMaxHeight = Int32.Parse(barMaxHeight);
            }

            // Determine barcode width
            string barMinWidth = m.Result("${BarMinWidth}");
            if (!string.IsNullOrEmpty(barMinWidth))
            {
                _barMinWidth = Int32.Parse(barMinWidth);
            }
            string barMaxWidth = m.Result("${BarMaxWidth}");
            if (!string.IsNullOrEmpty(barMaxWidth))
            {
                _barMaxWidth = Int32.Parse(barMaxWidth);
            }

            // Parse the barcode off the end of this string
            _text = m.Result("${BarCodePayload}");
        }
        #endregion
    }
}
