//-----------------------------------------------------------------------
// <copyright file="BarcodeImageEditor.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.SSRS.Design
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.ComponentModel;

	public class BarcodeImageEditor : ComponentEditor
	{
		public override bool EditComponent(ITypeDescriptorContext context, object component)
		{
			BarcodeImageDesigner designer = (BarcodeImageDesigner)component;
			// TODO: Invoke dialog to edit designer.
			return false;
		}
	}
}
