<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
CodeBehind="Default.aspx.cs" Inherits="TelerikGreed._Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc" TagName="TouristGrid" Src="~/UC/Tourists.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadFormDecorator runat="server" ID="m_FormDecorator" DecoratedControls="All" />
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ucTourists">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TouristUC" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
    <h2>
        Welcome to ASP.NET!
    </h2>
    <div>
        <uc:TouristGrid ID="ucTourists"  runat="server" OnTouristInserted="ucTourists_onTouristInserted" 
                        OnTouristUpdated="ucTourists_onTouristUpdated" 
                        onTouristDeleted="ucTourists_onTouristDeleted" />
    </div>
    <div>
        <asp:Label ID="lblDeleted" runat="server" Text="" /><br />
        <asp:Label ID="lblInserted" runat="server" Text="" /><br />
        <asp:Label ID="lblUpdated" runat="server" Text="" />
    </div>
</asp:Content>
