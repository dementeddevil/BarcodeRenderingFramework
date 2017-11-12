//-----------------------------------------------------------------------
// <copyright file="BarcodeHtmlHelper.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web.Mvc
{
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// <c>BarcodeHtmlHelper</c> contains MVC extensions to enable easy
	/// embedding of barcode images and URIs into MVC views.
	/// </summary>
	public static class BarcodeHtmlHelper
	{
		/// <summary>
		/// An HTML image tag
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="imageUrl">Url of the image</param>
		/// <param name="alternateText">The alternate text of the image</param>
		/// <param name="imageHtmlAttributes">Attributes for the image</param>
		/// <returns></returns>
		public static string Image(
			this HtmlHelper helper,
			string imageUrl,
			string alternateText,
			object imageHtmlAttributes)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			// Create image
			var imageTagBuilder = new TagBuilder("img");
			imageTagBuilder.MergeAttribute("src", urlHelper.Content(imageUrl));
			imageTagBuilder.MergeAttribute("alt", urlHelper.Content(alternateText));
			imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));
			return imageTagBuilder.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Generates an HTML image tag that contains a barcode representation
		/// of the specified text.
		/// </summary>
		/// <param name="helper">The HTML helper.</param>
		/// <param name="text">The text to render.</param>
		/// <param name="symbology">The barcode symbology to use.</param>
		/// <param name="height">The height (1D barcodes only).</param>
		/// <param name="scale">
		/// The scale factor to use (null = use default for symbology).
		/// </param>
		/// <param name="useExtensionlessUri">
		/// <c>true</c> to use extensionless URI; otherwise, <c>false</c>.
		/// </param>
		/// <param name="imageHtmlAttributes">Attributes for the image</param>
		/// <returns></returns>
		public static string BarcodeImage(
			this HtmlHelper helper,
			string text,
			BarcodeSymbology symbology,
			int height = 30,
			int? scale = null,
			bool useExtensionlessUri = true,
			object imageHtmlAttributes = null)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			// Create image
			var imageTagBuilder = new TagBuilder("img");
			imageTagBuilder.MergeAttribute("src", urlHelper.Barcode(text, symbology, height, scale, useExtensionlessUri));
			imageTagBuilder.MergeAttribute("alt", urlHelper.Content(text));
			imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));
			return imageTagBuilder.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Generates a barcode URI.
		/// </summary>
		/// <param name="helper">The HTML helper.</param>
		/// <param name="text">The text to be encoded.</param>
		/// <param name="symbology">The barcode symbology to use.</param>
		/// <param name="height">The height.</param>
		/// <param name="scale">
		/// The scale factor to use (null = use default for symbology).
		/// </param>
		/// <param name="useExtensionlessUri">
		/// <c>true</c> to use extensionless URI; otherwise, <c>false</c>.
		/// </param>
		/// <returns></returns>
		public static string Barcode(
			this UrlHelper helper,
			string text,
			BarcodeSymbology symbology,
			int height = 30,
			int? scale = null,
			bool useExtensionlessUri = true)
		{
			BarcodeImageUriBuilder builder = null;

			// We cheat and get the default metrics
			var temp = BarcodeDrawFactory.GetSymbology(symbology);
			var metrics = temp.GetDefaultMetrics(height);
			if (scale != null)
			{
				metrics.Scale = scale.Value;
			}
			BarcodeMetrics1d metrics1d = metrics as BarcodeMetrics1d;
			if (metrics1d != null)
			{
				builder =
					new BarcodeImageUriBuilder
					{
						EncodingScheme = symbology,
						Text = text,
						BarMaxHeight = metrics1d.MaxHeight,
						BarMinHeight = metrics1d.MinHeight,
						BarMaxWidth = metrics1d.MaxWidth,
						BarMinWidth = metrics1d.MinWidth,
						Scale = metrics.Scale,
						UseExtensionlessUri = useExtensionlessUri
					};
			}
			else
			{
				BarcodeMetricsQr metricsQr = metrics as BarcodeMetricsQr;
				if (metricsQr != null)
				{
					builder = 
						new BarcodeImageUriBuilder
						{
							EncodingScheme = BarcodeSymbology.CodeQr,
							Text = text,
							QrEncodingMode = metricsQr.EncodeMode,
							QrErrorCorrect = metricsQr.ErrorCorrection,
							QrVersion = metricsQr.Version,
							Scale = metrics.Scale,
							UseExtensionlessUri = useExtensionlessUri
						};
				}
			}
			return helper.Content(builder.ToString());
		}
	}
}
