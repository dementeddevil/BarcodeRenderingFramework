<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Zen.SampleSite.DefaultPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<table>
			<tr>
				<td><asp:Label ID="barcodeTextLabel" runat="server" Text="Barcode Text" /></td>
				<td><asp:TextBox ID="barcodeText" runat="server" Text="1234" /></td>
			</tr>
			<tr>
				<td><asp:Label ID="barcodeSymbologyLabel" runat="server" Text="Barcode Symbology" /></td>
				<td><asp:DropDownList ID="barcodeSymbology" runat="server" /></td>
			</tr>
			<tr>
				<td colspan="2"><asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click" /></td>
			</tr>
			<tr>
				<td colspan="2">
					<barcode:BarcodeLabel ID="barcodeRender" runat="server" Text="1234" BarcodeEncoding="Code128" BarMinWidth="1" BarMaxWidth="2" BarMinHeight="30" BarMaxHeight="40" />
				</td>
			</tr>
		</table>
	</div>
    </form>
</body>
</html>
