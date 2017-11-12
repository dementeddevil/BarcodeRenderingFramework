//-----------------------------------------------------------------------
// <copyright file="BarcodeImageDesigner.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.SSRS.Design
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.Drawing;
	using System.Linq;
	using System.Threading;
	using System.Windows.Forms;
	using Microsoft.ReportDesigner;
	using Microsoft.ReportDesigner.Design;
	using Microsoft.ReportingServices.Interfaces;
	using Microsoft.ReportingServices.RdlObjectModel;

	[LocalizedName("Barcode")]
	[Editor(typeof(BarcodeImageEditor), typeof(ComponentEditor))]
	[CustomReportItem("Barcode")]
	public class BarcodeImageDesigner : CustomReportItemDesigner
	{
		#region Private Fields
		private int _inSuspendValidate;
		private bool _pendingInvalidate;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeImageDesigner"/> class.
		/// </summary>
		public BarcodeImageDesigner()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the default size.
		/// </summary>
		/// <value>The default size.</value>
		/// <remarks>
		/// By default the control is 5cm x 1.5cm
		/// </remarks>
		public override ItemSize DefaultSize
		{
			get
			{
				return new ItemSize(
					new ReportSize(5, SizeTypes.Cm),
					new ReportSize(1.5, SizeTypes.Cm));
			}
		}

		/// <summary>
		/// Gets or sets the name of the data set.
		/// </summary>
		/// <value>The name of the data set.</value>
		[Browsable(true)]
		[Category("Data")]
		public string DataSetName
		{
			get
			{
				return CustomData.DataSetName;
			}
			set
			{
				CustomData.DataSetName = value;
			}
		}

		/// <summary>
		/// Gets or sets the symbology.
		/// </summary>
		/// <value>The symbology.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Barcode symbology to use for image.")]
		public BarcodeSymbology Symbology
		{
			get
			{
				string symbology = GetCustomPropertyString("barcode:Symbology");
				try
				{
					return (BarcodeSymbology)Enum.Parse(typeof(BarcodeSymbology), symbology, true);
				}
				catch
				{
					return BarcodeSymbology.Unknown;
				}
			}
			set
			{
				if (Symbology != value && value != BarcodeSymbology.Unknown)
				{
					SetCustomProperty("barcode:Symbology", value.ToString());

					// Refresh the draw metrics for this symbology
					BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(value);
					BarcodeMetrics drawMetrics = drawObject.GetDefaultMetrics(MaximumBarHeight);

					Scale = drawMetrics.Scale;

					using (IDisposable disp = new BarcodeDesignerEditScope(this))
					{
						BarcodeMetrics1d metrics1d = drawMetrics as BarcodeMetrics1d;
						if (metrics1d != null)
						{
							MinimumBarHeight = metrics1d.MinHeight;
							MaximumBarHeight = metrics1d.MaxHeight;
							MinimumBarWidth = metrics1d.MinWidth;
							MaximumBarWidth = metrics1d.MaxWidth;
							InterGlyphSpacing = metrics1d.InterGlyphSpacing;
							RenderVertically = metrics1d.RenderVertically;
						}
						else if (value == BarcodeSymbology.CodeQr)
						{
							BarcodeMetricsQr metricsQr = (BarcodeMetricsQr)drawMetrics;
							QrEncodingMode = metricsQr.EncodeMode;
							QrErrorCorrectionMode = metricsQr.ErrorCorrection;
							QrVersion = metricsQr.Version;
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the text to be rendered as a barcode.
		/// </summary>
		/// <value>The text.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the text to be rendered as a barcode.")]
		public string Text
		{
			get
			{
				return GetCustomPropertyString("barcode:Text");
			}
			set
			{
				if (Text != value)
				{
					SetCustomProperty("barcode:Text", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the scale factor used to render the barcode.
		/// </summary>
		/// <value>The scale factor.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the scale factor used to render the barcode.")]
		public int Scale
		{
			get
			{
				return GetCustomPropertyInt32("barcode:Scale", 1);
			}
			set
			{
				if (Scale != value)
				{
					SetCustomProperty("barcode:Scale", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the height of the bar.
		/// </summary>
		/// <value>The height of the bar.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the bar height.")]
		public int? BarHeight
		{
			get
			{
				if (MinimumBarHeight == MaximumBarHeight)
				{
					return MaximumBarHeight;
				}
				return null;
			}
			set
			{
				if (BarHeight != value && value != null)
				{
					using (IDisposable disp = new BarcodeDesignerEditScope(this))
					{
						MinimumBarHeight = MaximumBarHeight = value.Value;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum height of the bar.
		/// </summary>
		/// <value>The minimum height of the bar.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the minimum bar height.")]
		[DefaultValue(30)]
		public int MinimumBarHeight
		{
			get
			{
				return GetCustomPropertyInt32("barcode:MinimumBarHeight", 30);
			}
			set
			{
				if (MinimumBarHeight != value)
				{
					SetCustomProperty("barcode:MinimumBarHeight", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum height of the bar.
		/// </summary>
		/// <value>The maximum height of the bar.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the maximum bar height.")]
		[DefaultValue(30)]
		public int MaximumBarHeight
		{
			get
			{
				return GetCustomPropertyInt32("barcode:MaximumBarHeight", 30);
			}
			set
			{
				if (MaximumBarHeight != value)
				{
					SetCustomProperty("barcode:MaximumBarHeight", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the bar.
		/// </summary>
		/// <value>The width of the bar.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the bar width.")]
		public int? BarWidth
		{
			get
			{
				if (MinimumBarWidth == MaximumBarWidth)
				{
					return MaximumBarWidth;
				}
				return null;
			}
			set
			{
				if (BarWidth != value && value != null)
				{
					using (IDisposable disp = new BarcodeDesignerEditScope(this))
					{
						MinimumBarWidth = MaximumBarWidth = value.Value;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum width of the bar.
		/// </summary>
		/// <value>The minimum width of the bar.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the minimum bar width.")]
		[DefaultValue(1)]
		public int MinimumBarWidth
		{
			get
			{
				return GetCustomPropertyInt32("barcode:MinimumBarWidth", 1);
			}
			set
			{
				if (MinimumBarWidth != value)
				{
					SetCustomProperty("barcode:MinimumBarWidth", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum width of the bar.
		/// </summary>
		/// <value>The maximum width of the bar.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the maximum bar width.")]
		[DefaultValue(1)]
		public int MaximumBarWidth
		{
			get
			{
				return GetCustomPropertyInt32("barcode:MaximumBarWidth", 1);
			}
			set
			{
				if (MaximumBarWidth != value)
				{
					SetCustomProperty("barcode:MaximumBarWidth", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the inter glyph spacing.
		/// </summary>
		/// <value>The inter glyph spacing.</value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets the inter-glyph spacing.")]
		public int? InterGlyphSpacing
		{
			get
			{
				int spacing = GetCustomPropertyInt32("barcode:InterGlyphSpacing", -1);
				if (spacing == -1)
				{
					return null;
				}
				return spacing;
			}
			set
			{
				if (InterGlyphSpacing != value)
				{
					if (value == null)
					{
						SetCustomProperty("barcode:InterGlyphSpacing", -1);
					}
					else
					{
						SetCustomProperty("barcode:InterGlyphSpacing", value.Value);
					}
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to render barcode vertically.
		/// </summary>
		/// <value>
		/// <c>true</c> to render barcode vertically; otherwise, <c>false</c>.
		/// </value>
		[Browsable(true)]
		[Category("Barcode")]
		[Description("Gets or sets whether to draw barcode vertically.")]
		public bool RenderVertically
		{
			get
			{
				return GetCustomPropertyBool("barcode:RenderVertically", false);
			}
			set
			{
				if (RenderVertically != value)
				{
					SetCustomProperty("barcode:RenderVertically", value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the version used to render QR 2D barcode.
		/// </summary>
		/// <value>The qr version.</value>
		/// <remarks>
		/// Using 0 here will allow the code to use the best version for the
		/// content to render (which could lead to variable sized QR codes if
		/// your content varies wildly - best to set to a value that can
		/// accomodate all your data values...
		/// </remarks>
		[Browsable(true)]
		[Category("QR Barcode")]
		[Description("Gets or sets the version used to render QR 2D barcode.")]
		public int? QrVersion
		{
			get
			{
				if (Symbology != BarcodeSymbology.CodeQr)
				{
					return null;
				}
				return GetCustomPropertyInt32("barcode:QrVersion", 3);
			}
			set
			{
				if (Symbology == BarcodeSymbology.CodeQr && value != null && QrVersion != value)
				{
					if (value < 0 || value > 40)
					{
						throw new ArgumentOutOfRangeException(
							"value", "Version out of range (0=auto, 1-40=version)");
					}
					SetCustomProperty("barcode:QrVersion", value.Value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the encoding mode used to render a QR 2D barcode.
		/// </summary>
		/// <value>
		/// A value from the <see cref="QrEncodeMode"/> enumeration.
		/// </value>
		[Browsable(true)]
		[Category("QR Barcode")]
		[Description("Gets or sets the encoding mode used to render QR 2D barcode.")]
		public QrEncodeMode? QrEncodingMode
		{
			get
			{
				if (Symbology != BarcodeSymbology.CodeQr)
				{
					return null;
				}
				return (QrEncodeMode)GetCustomPropertyInt32("barcode:QrEncodeMode", (int)QrEncodeMode.Byte);
			}
			set
			{
				if (Symbology == BarcodeSymbology.CodeQr && value != null && QrEncodingMode != value)
				{
					SetCustomProperty("barcode:QrEncodeMode", (int)value.Value);
					InvalidateIfPossible();
				}
			}
		}

		/// <summary>
		/// Gets or sets the error correction used when rendering a QR 2D barcode.
		/// </summary>
		/// <value>
		/// A value from the <see cref="QrErrorCorrection"/> enumeration.
		/// </value>
		[Browsable(true)]
		[Category("QR Barcode")]
		[Description("Gets or sets the error correction mode used to render QR 2D barcode.")]
		public QrErrorCorrection? QrErrorCorrectionMode
		{
			get
			{
				if (Symbology != BarcodeSymbology.CodeQr)
				{
					return null;
				}
				return (QrErrorCorrection)GetCustomPropertyInt32("barcode:QrErrorCorrection", (int)QrErrorCorrection.M);
			}
			set
			{
				if (Symbology == BarcodeSymbology.CodeQr && value != null && QrErrorCorrectionMode != value)
				{
					SetCustomProperty("barcode:QrErrorCorrection", (int)value.Value);
					InvalidateIfPossible();
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Initializes the new component.
		/// </summary>
		public override void InitializeNewComponent()
		{
			base.InitializeNewComponent();

			CustomData = new CustomData();

			// Static row
			CustomData.DataRowHierarchy = new DataHierarchy();
			CustomData.DataRowHierarchy.DataMembers.Add(new DataMember());

			// Static column
			CustomData.DataColumnHierarchy = new DataHierarchy();
			CustomData.DataColumnHierarchy.DataMembers.Add(new DataMember());

			// Store text in rows
			CustomData.DataRows.Add(new DataRow());
			CustomData.DataRows[0].Add(new DataCell());
			CustomData.DataRows[0][0].Add(NewDataValue("Text", ""));
		}

		/// <summary>
		/// Draws the design-time representation of the barcode.
		/// </summary>
		/// <param name="g">
		/// A <see cref="Graphics"/> object representing the designer surface.
		/// </param>
		/// <param name="dp">
		/// A <see cref="ReportItemDrawParams"/> containing draw parameters.
		/// </param>
		public override void Draw(Graphics g, ReportItemDrawParams dp)
		{
			// Our background is always white
			if (dp.DrawBackground)
			{
				g.Clear(Color.White);
			}

			// Delegate drawing of outlines
			if (dp.DrawOutlines)
			{
				base.Draw(g, dp.AsOutlinesOnly());
			}

			// Draw content if we can...
			if (dp.DrawContent &&
				Symbology != BarcodeSymbology.Unknown &&
				!string.IsNullOrEmpty(Text))
			{
				BarcodeDraw drawObject =
					BarcodeDrawFactory.GetSymbology(Symbology);

				BarcodeMetrics metrics = drawObject.GetDefaultMetrics(30);
				metrics.Scale = Scale;

				BarcodeMetrics1d metrics1d = metrics as BarcodeMetrics1d;
				if (metrics1d != null)
				{
					metrics1d.InterGlyphSpacing = InterGlyphSpacing;
					metrics1d.MaxHeight = MaximumBarHeight;
					metrics1d.MinHeight = MinimumBarHeight;
					metrics1d.MaxWidth = MaximumBarWidth;
					metrics1d.MinWidth = MinimumBarWidth;
					metrics1d.RenderVertically = RenderVertically;
				}
				else if (Symbology == BarcodeSymbology.CodeQr)
				{
					BarcodeMetricsQr qrMetrics = (BarcodeMetricsQr)metrics;
					if (QrVersion != null)
					{
						qrMetrics.Version = QrVersion.Value;
					}
					if (QrEncodingMode != null)
					{
						qrMetrics.EncodeMode = QrEncodingMode.Value;
					}
					if (QrErrorCorrectionMode != null)
					{
						qrMetrics.ErrorCorrection = QrErrorCorrectionMode.Value;
					}
				}
				using (System.Drawing.Image image = drawObject.Draw(Text, metrics))
				{
					g.DrawImage(image, new Point(0, 0));
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="E:DragEnter"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
		public override void OnDragEnter(DragEventArgs e)
		{
			IFieldsDataObject fieldsDataObject = e.Data.GetData(
				typeof(IReportItemDataObject)) as IFieldsDataObject;
			if (fieldsDataObject != null &&
				fieldsDataObject.Fields != null &&
				fieldsDataObject.Fields.Length > 0)
			{
				BeginEdit();
			}
		}

		/// <summary>
		/// Raises the <see cref="E:DragOver"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
		public override void OnDragOver(DragEventArgs e)
		{
			IFieldsDataObject fieldsDataObject = e.Data.GetData(
				typeof(IReportItemDataObject)) as IFieldsDataObject;
			if (fieldsDataObject != null &&
				fieldsDataObject.Fields != null &&
				fieldsDataObject.Fields.Length > 1)
			{
				e.Effect = DragDropEffects.All;
			}
		}

		/// <summary>
		/// Raises the <see cref="E:DragDrop"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
		public override void OnDragDrop(DragEventArgs e)
		{
			IFieldsDataObject fieldsDataObject = e.Data.GetData(
				typeof(IReportItemDataObject)) as IFieldsDataObject;
			if (fieldsDataObject != null &&
				fieldsDataObject.Fields != null &&
				fieldsDataObject.Fields.Length > 0)
			{
				Text = string.Format(
					"=Fields!{0}.Value",
					fieldsDataObject.Fields[0].Name);
			}
		}

		public void SuspendInvalidate()
		{
			if (Interlocked.Increment(ref _inSuspendValidate) == 1)
			{
				_pendingInvalidate = false;
			}
		}

		public void ResumeInvalidate()
		{
			if (Interlocked.Decrement(ref _inSuspendValidate) == 0)
			{
				if (_pendingInvalidate)
				{
					RaiseComponentChanged();
					Invalidate();
				}
			}
		}

		public int GetCustomPropertyInt32(string propertyName)
		{
			return GetCustomPropertyInt32(propertyName, 0);
		}

		public int GetCustomPropertyInt32(string propertyName, int defaultValue)
		{
			int value;
			string valueText = GetCustomPropertyString(propertyName);
			if (!Int32.TryParse(valueText, out value))
			{
				value = defaultValue;
			}
			return value;
		}

		public bool GetCustomPropertyBool(string propertyName)
		{
			return GetCustomPropertyBool(propertyName, false);
		}

		public bool GetCustomPropertyBool(string propertyName, bool defaultValue)
		{
			bool value;
			string valueText = GetCustomPropertyString(propertyName);
			if (!Boolean.TryParse(valueText, out value))
			{
				value = defaultValue;
			}
			return value;
		}

		public void SetCustomProperty(string propertyName, int value)
		{
			SetCustomProperty(propertyName, value.ToString());
		}

		public void SetCustomProperty(string propertyName, bool value)
		{
			SetCustomProperty(propertyName, value.ToString());
		}

		public string GetCustomPropertyString(string propertyName)
		{
			string value;
			if (!CustomProperties.TryGetValue(propertyName, out value))
			{
				value = null;
			}
			return value;
		}

		public void SetCustomProperty(string propertyName, string value)
		{
			if (!CustomProperties.ContainsKey(propertyName))
			{
				CustomProperties.Add(propertyName, value);
			}
			else
			{
				CustomProperties[propertyName] = value;
			}
		}
		#endregion

		#region Private Methods
		private void InvalidateIfPossible()
		{
			if (_inSuspendValidate > 0)
			{
				_pendingInvalidate = true;
			}
			else
			{
				RaiseComponentChanged();
				Invalidate();
			}
		}

		private void RaiseComponentChanged()
		{
			IComponentChangeService changeSvc = (IComponentChangeService)
				Site.GetService(typeof(IComponentChangeService));
			changeSvc.OnComponentChanged(this, null, null, null);
		}

		private static string GetCustomPropertyString(IList<CustomProperty> properties, string propertyName)
		{
			if (properties == null)
			{
				return null;
			}

			CustomProperty prop = properties
				.FirstOrDefault((item) => item.Name == propertyName);
			if (prop != null)
			{
				return prop.Value.Value;
			}
			return null;
		}

		private static void SetCustomProperty(IList<CustomProperty> properties, string propertyName, string value)
		{
			CustomProperty prop = properties
				.FirstOrDefault((item) => item.Name == propertyName);
			if (prop == null)
			{
				prop = new CustomProperty();
				prop.Name = propertyName;
				properties.Add(prop);
			}

			prop.Value = value;
		}

		private static string GetDataValue(IList<DataValue> cell, string name)
		{
			DataValue value = cell.FirstOrDefault((item) => item.Name == name);
			if (value != null)
			{
				return value.Value.Value;
			}
			return null;
		}

		private static void SetDataValue(IList<DataValue> cell, string name, string expression)
		{
			DataValue value = cell.FirstOrDefault((item) => item.Name == name);
			if (value != null)
			{
				value.Value = expression;
			}
			else
			{
				DataValue datavalue = NewDataValue(name, expression);
				cell.Add(datavalue);
			}
		}

		private static DataValue NewDataValue(string name, string value)
		{
			return new DataValue
			{
				Name = name,
				Value = value
			};
		}
		#endregion
	}

	public sealed class BarcodeDesignerEditScope : IDisposable
	{
		public BarcodeDesignerEditScope(BarcodeImageDesigner designer)
		{
			Designer = designer;
			Designer.SuspendInvalidate();
		}

		~BarcodeDesignerEditScope()
		{
			Dispose(false);
		}

		private BarcodeImageDesigner Designer
		{
			get;
			set;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && Designer != null)
			{
				Designer.ResumeInvalidate();
			}
			Designer = null;
		}
	}
}
