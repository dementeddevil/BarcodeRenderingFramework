//-----------------------------------------------------------------------
// <copyright file="BarcodeImageHandler.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Threading;
	using System.Web;
	using Zen.Barcode;
	using System.IO;

	/// <summary>
	/// <b>BarcodeImageHandler</b> is a custom ASP.NET HTTP Handler for
	/// streaming bar-code images to a web-client.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The bar-code symbology and size metrics are encoded into the image
	/// filename by <see cref="T:Zen.Barcode.Web.BarcodeImageUrlBuilder"/>.
	/// </para>
	/// <para>
	/// Currently the image format is fixed as JPEG.
	/// </para>
	/// <para>
	/// For ultimate performance this is an asynchronous HTTP handler
	/// with work performed on a thread-pooled thread.
	/// </para>
	/// </remarks>
	public sealed class BarcodeImageHandler : IHttpAsyncHandler
	{
		#region Internal Classes
		private class AsyncHandler : IAsyncResult
		{
			#region Private Fields
			private HttpContext _context;
			private HttpRequest _request;
			private HttpResponse _response;

			private AsyncCallback _callback;
			private object _state;
			private ManualResetEvent _wait;
			private bool _isCompleted;
			private Exception _error;
			#endregion

			#region Public Constructors
			public AsyncHandler(HttpContext context, AsyncCallback callback,
					object state)
			{
				_context = context;
				_callback = callback;
				_state = state;
				_wait = new ManualResetEvent(false);
			}
			#endregion

			#region Public Methods
			public void StartAsyncWork()
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
			}

			public void EndAsyncWork()
			{
				// Wait for completion if we are still working
				if (!_isCompleted)
				{
					_wait.WaitOne();
				}

				// Throw any error we have cached
				if (_error != null)
				{
					throw _error;
				}
			}
			#endregion

			#region Private Methods
			private void StartAsyncTask(object state)
			{
				try
				{
					// We want to respond to image requests of the form;
					//
					//	<encoded barcode>.Barcode

					// Cache information from context
					_request = _context.Request;
					_response = _context.Response;

					// Filename is the encoded design ID
					BarcodeImageUri uri = new BarcodeImageUri(_request.Url);

					// Lookup design and retrieve image data
					// Stream JPEG image to client
					_response.ContentType = "image/jpeg";
					_response.Clear();
					_response.BufferOutput = true;

					// Get the object capable of rendering the barcode
					BarcodeDraw drawObject =
						BarcodeDrawFactory.GetSymbology(uri.EncodingScheme);

					BarcodeMetrics metrics = drawObject.GetDefaultMetrics(30);
					metrics.Scale = uri.Scale;

					BarcodeMetrics1d metrics1d = metrics as BarcodeMetrics1d;
					if (metrics1d != null)
					{
						metrics1d.MaxHeight = uri.BarMaxHeight;
						metrics1d.MinHeight = uri.BarMinHeight;
						metrics1d.MaxWidth = uri.BarMaxWidth;
						metrics1d.MinWidth = uri.BarMinWidth;
					}
					else if (uri.EncodingScheme == BarcodeSymbology.CodeQr)
					{
						BarcodeMetricsQr qrMetrics = (BarcodeMetricsQr)metrics;
						qrMetrics.EncodeMode = uri.QrEncodingMode;
						qrMetrics.ErrorCorrection = uri.QrErrorCorrect;
						qrMetrics.Version = uri.QrVersion;
					}

					// Render barcode and save directly onto response stream
					MemoryStream imageStream = new MemoryStream();
					using (Image image = drawObject.Draw(uri.Text, metrics))
					{
						// Save to temporary stream because image tried to seek
						//	during the write operation
						image.Save(imageStream, ImageFormat.Jpeg);

						// Move to start of the stream
						imageStream.Seek(0, SeekOrigin.Begin);

						// Do synchronous copy to response output stream
						int blockSize = 1024;
						byte[] buffer = new byte[blockSize];
						while (true)
						{
							int bytesRead = imageStream.Read(buffer, 0, blockSize);
							if (bytesRead == 0)
							{
								break;
							}
							_response.OutputStream.Write(buffer, 0, bytesRead);
						}
					}
				}
				catch (Exception e)
				{
					_error = e;
				}
				finally
				{
					_response.End();
					SetComplete();
				}
			}

			private void SetComplete()
			{
				// Mark task as complete and set handle
				_isCompleted = true;
				_wait.Set();

				// If we have a callback method then call it
				if (_callback != null)
				{
					_callback(this);
				}
			}
			#endregion

			#region IAsyncResult Members
			object IAsyncResult.AsyncState
			{
				get
				{
					return _state;
				}
			}

			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return _wait;
				}
			}

			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			bool IAsyncResult.IsCompleted
			{
				get
				{
					return _isCompleted;
				}
			}
			#endregion
		}
		#endregion

		#region IHttpHandler Members
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			throw new InvalidOperationException();
		}
		#endregion

		#region IHttpAsyncHandler Members
		/// <summary>
		/// Begins processing the image request.
		/// </summary>
		/// <param name="context">Current HTTP request context</param>
		/// <param name="cb">Delegate called when processing is completed</param>
		/// <param name="extraData">Application defined state</param>
		/// <returns><see cref="T:System.IAsyncResult"/> used for tracking progress</returns>
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			AsyncHandler handler = new AsyncHandler(context, cb, extraData);
			handler.StartAsyncWork();
			return handler;
		}

		/// <summary>
		/// Ends processing of an image request.
		/// </summary>
		/// <param name="result">
		/// <see cref="T:System.IAsyncResult"/> obtained in a previous call to
		/// <b>BeginProcessRequest</b>.
		/// </param>
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			// Sanity checks
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			AsyncHandler handler = result as AsyncHandler;
			if (handler == null)
			{
				throw new ArgumentException("Not IAsyncResult from this handler.");
			}

			// Finish request processing
			handler.EndAsyncWork();
		}
		#endregion
	}
}
