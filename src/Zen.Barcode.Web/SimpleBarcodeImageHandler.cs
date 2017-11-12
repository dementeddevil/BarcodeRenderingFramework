//-----------------------------------------------------------------------
// <copyright file="BarcodeImageHandler.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2011-2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Linq;
	using System.Web;
	using Zen.Barcode;

	/// <summary>
	/// <c>SimpleBarcodeImageHandler</c> is an http handler designed to return 
	/// barcode images based on query information passed to the handler.
	/// </summary>
	/// <remarks>
	/// The following query parameters are understood by the handler
	/// sym = symbology to use for encoding (see symbology enumeration)
	/// text = text to encode (must be URL encoded)
	/// itype = image type for rendering to the stream 0=JPEG, 1=PNG
	/// 
	/// For non-QR symbology:
	/// bh = bar height
	/// bw = bar width
	/// mbw = minimum bar width
	/// xbw = maximum bar width
	/// mbh = minimum bar height
	/// xbh = maximum bar height
	/// igs = inter-glyph spacing
	/// 
	/// For QR symbology:
	/// em = QR encoding mode (use numeric representation of QrEncodeMode enumeration)
	/// ec = QR error correction scheme (use numeric representation of QrErrorCorrection enumeration)
	/// sca = QR scale factor
	/// ver = QR version (0=auto-detect,1-40=specific version)
	/// </remarks>
	public sealed class SimpleBarcodeImageHandler : IHttpHandler
	{
		/// <summary>
		/// Supported rendering formats
		/// </summary>
		public enum RenderImageFormat
		{
			/// <summary>
			/// Render as JPEG
			/// </summary>
			Jpeg = 0,

			/// <summary>
			/// Render as PNG
			/// </summary>
			Png = 1,
		}

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
		/// </summary>
		/// <value></value>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler
		/// that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
		/// </summary>
		/// <param name="context">
		/// An <see cref="T:System.Web.HttpContext"/> object that provides 
		/// references to the intrinsic server objects (for example, Request,
		/// Response, Session, and Server) used to service HTTP requests.
		/// </param>
		public void ProcessRequest(HttpContext context)
		{
			try
			{
				// We want aggressive caching since the barcode will not change
				//	for a given set of request parameters however we disable
				//	caching on proxy servers...
				context.Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);

				// Determine the symbology desired
				string symbologyText = context.Request.QueryString["sym"];
				BarcodeSymbology symbology;
				if (!TryParseEnum<BarcodeSymbology>(symbologyText, true, out symbology) ||
					symbology == BarcodeSymbology.Unknown)
				{
					throw new ArgumentException("Unable to determine symbology.");
				}

				// Get the text to render from the context
				// NOTE: We URL decode it first...
				string textToRender =
					context.Server.UrlDecode(
					context.Request.QueryString["text"]);
				if (string.IsNullOrEmpty(textToRender))
				{
					throw new ArgumentException("Must have text to render as barcode.");
				}

				// Get the rendering metrics from the context
				BarcodeMetrics metrics = GetBarcodeMetricsFromContext(context, symbology);

				// Determine the image format (default is jpeg)
				RenderImageFormat format;
				string imageFormatText = context.Request.QueryString["itype"];
				if (!TryParseEnum<RenderImageFormat>(imageFormatText, true, out format))
				{
					format = RenderImageFormat.Jpeg;
				}

				// Setup content-type and image format for saving
				ImageFormat imageFormat;
				switch (format)
				{
					case RenderImageFormat.Jpeg:
						imageFormat = ImageFormat.Jpeg;
						context.Response.ContentType = "image/jpeg";
						break;
					case RenderImageFormat.Png:
						imageFormat = ImageFormat.Png;
						context.Response.ContentType = "image/png";
						break;
					default:
						throw new ArgumentException(
							"Unexpected rendering image format encountered.");
				}

				// If we can find an encoder for the image type then attempt
				//	to set top quality and monochrome colour depth.
				ImageCodecInfo codecInfo = ImageCodecInfo.GetImageEncoders()
					.FirstOrDefault((item) => item.FormatID == imageFormat.Guid);
				EncoderParameters codecParameters = null;
				if (codecInfo != null)
				{
					// Two parameters; maximum quality and monochrome
					codecParameters = new EncoderParameters(2);
					codecParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
					codecParameters.Param[1] = new EncoderParameter(Encoder.ColorDepth, 2);
				}

				// Create instance of barcode rendering engine
				BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(symbology);
				using (Image image = drawObject.Draw(textToRender, metrics))
				{
					// Save image to the response stream directly
					// TODO: Should the response have buffer enabled?
					if (codecInfo == null)
					{
						image.Save(context.Response.OutputStream, imageFormat);
					}
					else
					{
						image.Save(context.Response.OutputStream, codecInfo, codecParameters);
					}
				}

				// Set status and finalise request handling.
				context.Response.StatusCode = 200;
				context.Response.End();
			}
			catch (Exception)
			{
				// TODO: Log the error and return a 500...
				context.Response.StatusCode = 500;
				context.Response.End();
			}
		}

		private static bool TryParseEnum<T>(
			string text, bool ignoreCase, out T value)
		{
			try
			{
				value = (T)Enum.Parse(typeof(T), text, ignoreCase);
				return true;
			}
			catch
			{
				value = default(T);
				return false;
			}
		}

		private static BarcodeMetrics GetBarcodeMetricsFromContext(
			HttpContext context, BarcodeSymbology symbology)
		{
			BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(symbology);
			BarcodeMetrics metrics = drawObject.GetDefaultMetrics(30);

			BarcodeMetrics1d metrics1d = metrics as BarcodeMetrics1d;
			if (metrics1d != null)
			{
				// Get query parameter strings
				string barHeightText = context.Request.QueryString["bh"];
				string barWidthText = context.Request.QueryString["bw"];
				string minimumBarHeightText = context.Request.QueryString["mbh"];
				string maximumBarHeightText = context.Request.QueryString["xbh"];
				string minimumBarWidthText = context.Request.QueryString["mbw"];
				string maximumBarWidthText = context.Request.QueryString["xbw"];
				string interGlyphSpaceText = context.Request.QueryString["igs"];

				int value;
				if (int.TryParse(barWidthText, out value))
				{
					metrics1d.MinWidth = metrics1d.MaxWidth = value;
				}
				if (int.TryParse(minimumBarWidthText, out value))
				{
					metrics1d.MinWidth = value;
				}
				if (int.TryParse(maximumBarWidthText, out value))
				{
					metrics1d.MaxWidth = value;
				}
				if (int.TryParse(barHeightText, out value))
				{
					metrics1d.MinHeight = metrics1d.MaxHeight = value;
				}
				if (int.TryParse(minimumBarHeightText, out value))
				{
					metrics1d.MinHeight = value;
				}
				if (int.TryParse(maximumBarHeightText, out value))
				{
					metrics1d.MaxHeight = value;
				}
				if (int.TryParse(interGlyphSpaceText, out value))
				{
					metrics1d.InterGlyphSpacing = value;
				}
			}
			else if (symbology == BarcodeSymbology.CodeQr)
			{
				BarcodeMetricsQr qrMetrics = (BarcodeMetricsQr)metrics;

				string encodeMode = context.Request.QueryString["em"];
				string errorCorrect = context.Request.QueryString["ec"];
				string scale = context.Request.QueryString["sca"];
				string version = context.Request.QueryString["ver"];

				int value;
				if (int.TryParse(encodeMode, out value))
				{
					qrMetrics.EncodeMode = (QrEncodeMode)value;
				}
				if (int.TryParse(errorCorrect, out value))
				{
					qrMetrics.ErrorCorrection = (QrErrorCorrection)value;
				}
				if (int.TryParse(scale, out value))
				{
					qrMetrics.Scale = value;
				}
				if (int.TryParse(version, out value))
				{
					qrMetrics.Version = value;
				}
			}

			return metrics;
		}
	}
}
