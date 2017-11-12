//-----------------------------------------------------------------------
// <copyright file="BarcodeXmlDesigner.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Security.Permissions;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.Design;

    using Zen.Barcode.Web;

    /// <summary>
    /// Extends design-time behavior for the <see cref="T:BarcodeXml" /> Web
    /// server control.
    /// </summary>
    //[SecurityPermission (SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class BarcodeXmlDesigner : ControlDesigner
    {
        #region Private Fields
        private BarcodeXml _xml;
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initialises a new instance of the <see cref="T:BarcodeXmlDesigner" /> class.
        /// </summary>
        public BarcodeXmlDesigner()
        {
        }
        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves the HTML markup that is used to represent the control
        /// at design time.
        /// </summary>
        /// <returns>
        /// The HTML markup used to represent the control at design time.
        /// </returns>
        public override string GetDesignTimeHtml()
        {
            return GetEmptyDesignTimeHtml();
        }

        /// <summary>
        /// Initializes the designer with the control that this instance of
        /// the designer is associated with.
        /// </summary>
        /// <param name="component">The associated control. </param>
        public override void Initialize(IComponent component)
        {
            if (!typeof(BarcodeXml).IsInstanceOfType(component))
            {
                throw new ArgumentException("Component is not instance of BarcodeXml.");
            }
            _xml = (BarcodeXml) component;
            base.Initialize(component);
        }

        /// <summary>
        /// Retrieves the persistable inner HTML markup of the control.
        /// </summary>
        /// <returns>
        /// The persistable inner HTML markup of the control.
        /// </returns>
        [Obsolete]
        public override string GetPersistInnerHtml()
        {
            BarcodeXml xml = (BarcodeXml) base.Component;
            string text = (string) ((IControlDesignerAccessor) xml).GetDesignModeState()["OriginalContent"];
            if (text != null)
            {
                return text;
            }
            return xml.DocumentContent;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Releases the unmanaged resources that are used by the 
        /// <see cref="T:BarcodeXmlDesigner" /> control and optionally releases
        /// the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; 
        /// false to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _xml = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the HTML that is used to fill an empty control.
        /// </summary>
        /// <returns>
        /// The HTML used to fill an empty control.
        /// </returns>
        protected override string GetEmptyDesignTimeHtml()
        {
            return CreatePlaceHolderDesignTimeHtml();
        }
        #endregion
    }
}
