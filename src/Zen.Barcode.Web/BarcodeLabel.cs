//-----------------------------------------------------------------------
// <copyright file="BarcodeLabel.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Text;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	/// <summary>
	/// <b>BarcodeLabel</b> renders bar-codes in a variety of formats.
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:BarcodeLabel runat=server></{0}:BarcodeLabel>")]
	public class BarcodeLabel : CompositeControl
	{
		#region Private Fields
		private TableItemStyle _barcodeRowStyle;
		private TableItemStyle _labelRowStyle;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialises an instance of <see cref="T:BarcodeLabel" />.
		/// </summary>
		public BarcodeLabel()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets/sets the text that will be rendered as a barcode.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		Description("Gets/sets the text to be rendered as a barcode."),
		DefaultValue(""),
		Localizable(true)
		]
		public string Text
		{
			get
			{
				string s = (string)ViewState["Text"];
				return ((s == null) ? string.Empty : s);
			}
			set
			{
				ViewState["Text"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the barcode encoding scheme.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the barcode encoding system."),
		DefaultValue((int)BarcodeSymbology.Unknown)
		]
		public BarcodeSymbology BarcodeEncoding
		{
			get
			{
				object objState = ViewState["BarcodeEncoding"];
				return (objState == null) ? BarcodeSymbology.Unknown : (BarcodeSymbology)objState;
			}
			set
			{
				ViewState["BarcodeEncoding"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the barcode scaling factor.
		/// </summary>
		/// <value>The scaling factor.</value>
		[
		Category("Appearance"),
		Description("Gets/sets the barcode scaling factor."),
		DefaultValue(1)
		]
		public int Scale
		{
			get
			{
				object objState = ViewState["BarcodeScale"];
				return (objState != null) ? (int)objState : ((BarcodeEncoding == BarcodeSymbology.CodeQr) ? 3 : 1);
			}
			set
			{
				ViewState["BarcodeScale"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the barcode minimum height in pixels.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the barcode minimum height."),
		DefaultValue((int)30)
		]
		public int BarMinHeight
		{
			get
			{
				object objState = ViewState["BarMinHeight"];
				return (objState == null) ? 30 : (int)objState;
			}
			set
			{
				ViewState["BarMinHeight"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the barcode maximum height in pixels.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the barcode maximum height."),
		DefaultValue((int)30)
		]
		public int BarMaxHeight
		{
			get
			{
				object objState = ViewState["BarMaxHeight"];
				return (objState == null) ? 30 : (int)objState;
			}
			set
			{
				ViewState["BarMaxHeight"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the barcode minimum bar width in pixels.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the barcode minimum bar-width."),
		DefaultValue((int)30)
		]
		public int BarMinWidth
		{
			get
			{
				object objState = ViewState["BarMinWidth"];
				return (objState == null) ? 30 : (int)objState;
			}
			set
			{
				ViewState["BarMinWidth"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the barcode maximum bar width in pixels.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the barcode maximum bar-width."),
		DefaultValue((int)1)
		]
		public int BarMaxWidth
		{
			get
			{
				object objState = ViewState["BarMaxWidth"];
				return (objState == null) ? 30 : (int)objState;
			}
			set
			{
				ViewState["BarMaxWidth"] = value;
			}
		}

		/// <summary>
		/// Gets/sets a boolean value controlling whether the label is shown
		/// together with the barcode.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets whether the label is shown together with the barcode."),
		DefaultValue(true)
		]
		public bool ShowLabel
		{
			get
			{
				object objState = ViewState["ShowLabel"];
				return (objState == null) ? true : (bool)objState;
			}
			set
			{
				ViewState["ShowLabel"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the vertical alignment of the label.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the vertical alignment of the label."),
		DefaultValue((int)VerticalAlign.Bottom)
		]
		public VerticalAlign LabelVerticalAlign
		{
			get
			{
				object objState = ViewState["LabelVerticalAlign"];
				return (objState == null) ? VerticalAlign.Bottom : (VerticalAlign)objState;
			}
			set
			{
				ViewState["LabelVerticalAlign"] = value;
			}
		}

		/// <summary>
		/// Gets/sets the horizontal alignment of the label.
		/// </summary>
		[
		Category("Appearance"),
		Description("Gets/sets the horizontal alignment of the label."),
		DefaultValue((int)HorizontalAlign.Center)
		]
		public HorizontalAlign LabelHorizontalAlign
		{
			get
			{
				object objState = ViewState["LabelHorizontalAlign"];
				return (objState == null) ? HorizontalAlign.Center : (HorizontalAlign)objState;
			}
			set
			{
				ViewState["LabelHorizontalAlign"] = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.Web.UI.WebControls.Style"/> object
		/// associated with the content row.
		/// </summary>
		[
		NotifyParentProperty(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Category("Styles"),
		PersistenceMode(PersistenceMode.InnerProperty),
		Description("WebControl_RowStyle")
		]
		public TableItemStyle BarcodeRowStyle
		{
			get
			{
				if (_barcodeRowStyle == null)
				{
					_barcodeRowStyle = new TableItemStyle();
					_barcodeRowStyle.CssClass = "barcodeRow";
					if (base.IsTrackingViewState)
					{
						((IStateManager)_barcodeRowStyle).TrackViewState();
					}
				}
				return _barcodeRowStyle;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.Web.UI.WebControls.Style"/> object
		/// associated with the content row.
		/// </summary>
		[
		NotifyParentProperty(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Category("Styles"),
		PersistenceMode(PersistenceMode.InnerProperty),
		Description("WebControl_RowStyle")
		]
		public TableItemStyle LabelRowStyle
		{
			get
			{
				if (_labelRowStyle == null)
				{
					_labelRowStyle = new TableItemStyle();
					_labelRowStyle.CssClass = "barcodeLabelRow";
					if (base.IsTrackingViewState)
					{
						((IStateManager)_labelRowStyle).TrackViewState();
					}
				}
				return _labelRowStyle;
			}
		}
		#endregion

		#region Public Methods
		#endregion

		#region Protected Methods
		/// <summary>
		/// Overridden. Creates the control style object.
		/// </summary>
		/// <returns></returns>
		protected override Style CreateControlStyle()
		{
			TableStyle style = new TableStyle();
			style.CssClass = "barcodeTable";
			style.BorderStyle = BorderStyle.None;
			style.BorderWidth = new Unit(0);
			style.CellPadding = 10;
			style.CellSpacing = 0;
			return style;
		}

		/// <summary>
		/// Overridden. Creates the child nested controls.
		/// </summary>
		protected override void CreateChildControls()
		{
			Table mainTable = new Table();

			TableRow labelRowTop = new TableRow();
			labelRowTop.ID = "labelRowTop";
			TableCell labelCellTop = new TableCell();
			labelCellTop.ID = "labelCellTop";
			labelRowTop.Controls.Add(labelCellTop);
			mainTable.Controls.Add(labelRowTop);

			TableRow barcodeRow = new TableRow();
			barcodeRow.ID = "barcodeRow";
			TableCell barcodeCell = new TableCell();
			barcodeCell.ID = "barcodeCell";
			Image barcodeImage = new Image();
			barcodeImage.ID = "barcodeImage";
			barcodeCell.Controls.Add(barcodeImage);
			barcodeRow.Controls.Add(barcodeCell);
			mainTable.Controls.Add(barcodeRow);

			TableRow labelRowBottom = new TableRow();
			labelRowBottom.ID = "labelRowBottom";
			TableCell labelCellBottom = new TableCell();
			labelCellBottom.ID = "labelCellBottom";
			labelRowBottom.Controls.Add(labelCellBottom);
			mainTable.Controls.Add(labelRowBottom);

			Controls.Add(mainTable);
		}

		/// <summary>
		/// Overridden. Renders this control to the specified
		/// <see cref="T:System.Web.UI.HtmlTextWriter"/> object.
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}

			PrepareControlHierarchy();
			RenderContents(writer);
		}

		/// <summary>
		/// Prepares our composite controls for rendering by setting
		/// last-minute (ie: non-viewstate tracked) properties.
		/// </summary>
		protected virtual void PrepareControlHierarchy()
		{
			if (Controls.Count > 0)
			{
				// Setup base table control style
				Table table = (Table)Controls[0];
				table.CopyBaseAttributes(this);
				if (ControlStyleCreated && !ControlStyle.IsEmpty)
				{
					table.ApplyStyle(ControlStyle);
				}
				else
				{
					table.GridLines = GridLines.None;
					table.CellPadding = 10;
					table.CellSpacing = 0;
				}

				// Setup label controls.
				TableRow labelRowTop = (TableRow)FindControl("labelRowTop");
				TableRow labelRowBottom = (TableRow)FindControl("labelRowBottom");
				labelRowTop.Visible = false;
				labelRowBottom.Visible = false;
				if (ShowLabel)
				{
					// Setup label row style
					TableItemStyle style = new TableItemStyle();
					style.CopyFrom(LabelRowStyle);
					style.HorizontalAlign = LabelHorizontalAlign;

					// Setup appropriate row
					if (LabelVerticalAlign == VerticalAlign.Top)
					{
						labelRowTop.MergeStyle(style);
						labelRowTop.Visible = true;

						TableCell labelCell = (TableCell)FindControl("labelCellTop");
						labelCell.Text = Text;
					}
					else
					{
						labelRowBottom.MergeStyle(style);
						labelRowBottom.Visible = true;

						TableCell labelCell = (TableCell)FindControl("labelCellBottom");
						labelCell.Text = Text;
					}
				}

				// Setup barcode row style
				TableRow barcodeRow = (TableRow)FindControl("barcodeRow");
				barcodeRow.MergeStyle(BarcodeRowStyle);

				// Setup barcode image url
				BarcodeImageUriBuilder builder = new BarcodeImageUriBuilder();
				builder.Text = Text;
				builder.Scale = Scale;
				builder.EncodingScheme = BarcodeEncoding;
				builder.BarMinHeight = BarMinHeight;
				builder.BarMaxHeight = BarMaxHeight;
				builder.BarMinWidth = BarMinWidth;
				builder.BarMaxWidth = BarMaxWidth;
				Image barcodeImage = (Image)FindControl("barcodeImage");
				barcodeImage.ImageUrl = builder.ToString();
			}
		}

		/// <summary>
		/// Overridden. Loads the view-state information previously saved by
		/// invoking <see cref="T:Noble.Portal.UI.BarcodeLabel.SaveViewState"/>
		/// method.
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] stateArray = (object[])savedState;

				base.LoadViewState(stateArray[0]);
				if (stateArray[1] != null)
				{
					((IStateManager)BarcodeRowStyle).LoadViewState(stateArray[1]);
				}
				if (stateArray[2] != null)
				{
					((IStateManager)LabelRowStyle).LoadViewState(stateArray[2]);
				}
				if (stateArray[3] != null)
				{
					((IStateManager)base.ControlStyle).LoadViewState(stateArray[4]);
				}
			}
			else
			{
				base.LoadViewState(null);
			}
		}

		/// <summary>
		/// Overridden. Saves any state that was modified after the 
		/// <see cref="T:System.Web.UI.WebControls.Style.TrackViewState"/>
		/// method was invoked.
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object barcodeRowStyleState = (_barcodeRowStyle != null) ? ((IStateManager)_barcodeRowStyle).SaveViewState() : null;
			object labelRowStyleState = (_labelRowStyle != null) ? ((IStateManager)_labelRowStyle).SaveViewState() : null;
			object controlStyleState = base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null;
			return new object[] { baseState, barcodeRowStyleState, labelRowStyleState, controlStyleState };
		}

		/// <summary>
		/// Instructs the control to start tracking changes to view-state.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_barcodeRowStyle != null)
			{
				((IStateManager)_barcodeRowStyle).TrackViewState();
			}
			if (_labelRowStyle != null)
			{
				((IStateManager)_labelRowStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
		}
		#endregion
	}
}
