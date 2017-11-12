//-----------------------------------------------------------------------
// <copyright file="BarcodeImageRouteHandler.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web
{
	using System.Web;
	using System.Web.Routing;

	/// <summary>
	/// <c>BarcodeImageRouteHandler</c> implements an HTTP route handler that
	/// allows barcode rendering to be used in extension-less scenarios.
	/// </summary>
	/// <remarks>
	/// To register a route using this handler add the following code
	/// 
	/// </remarks>
	public class BarcodeImageRouteHandler : IRouteHandler
	{
		/// <summary>
		/// Provides the object that processes the request.
		/// </summary>
		/// <param name="requestContext">
		/// An object that encapsulates information about the request.
		/// </param>
		/// <returns>An object that processes the request.</returns>
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new BarcodeImageHandler();
		}
	}
}
