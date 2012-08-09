<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
CodeBehind="Default.aspx.cs" Inherits="TelerikGreed._Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadFormDecorator runat="server" ID="m_FormDecorator" DecoratedControls="All" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function rowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdTouristsList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTouristsList" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
    <h2>
        Welcome to ASP.NET!
    </h2>
    <div>
        <telerik:RadGrid runat="server" ID="grdTouristsList" AutoGenerateColumns="false"
                         AllowPaging="true" OnNeedDataSource="grdTouristsList_NeedDataSource" OnUpdateCommand="grdTouristsList_UpdateCommand"
                         OnItemCreated="grdTouristsList_ItemCreated" OnDeleteCommand="grdTouristsList_DeleteCommand"
                         OnInsertCommand="grdTouristsList_InsertCommand" Skin="Office2007" EnableEmbeddedSkins="true">
            <MasterTableView DataKeyNames="PolTuristiSaraksts" CommandItemDisplay="Top"  EditMode="PopUp" InsertItemPageIndexAction="ShowItemOnCurrentPage">
                <CommandItemSettings AddNewRecordText="Pievienot nākamo apdrošināto"  />

                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="10" />
                    <telerik:GridCheckBoxColumn DataField="IsResident" HeaderText="Residents"  ReadOnly="True" ItemStyle-Width="10" />
                    <telerik:GridBoundColumn DataField="PersKods" HeaderText="Pers. Kods" ItemStyle-Width="20"/>
                    <telerik:GridBoundColumn DataField="Vards" HeaderText="Vards" />
                    <telerik:GridBoundColumn DataField="Uzvards" HeaderText="Uzvards" />
                    <%--                    <telerik:GridBoundColumn DataField="Apstaklis_ID"  HeaderText="Apstaklis_ID"  />--%>
                    <telerik:GridBoundColumn DataField="Apstaklis"  HeaderText="Apstaklis"  ItemStyle-Width="170"  />
                    
                    <telerik:GridDateTimeColumn DataField="SpecDatumsNo" HeaderText="Spec. Datums No" ItemStyle-Width="20"  DataType="System.DateTime" DataFormatString="{0:d}"  />
                    <telerik:GridDateTimeColumn DataField="SpecDatumsLi" HeaderText="Spec. Datums Līdz" ItemStyle-Width="20"   DataType="System.DateTime" DataFormatString="{0:d}" />
                    <telerik:GridBoundColumn DataField="PolDarbDienas" HeaderText="Dienas" />
                    <telerik:GridBoundColumn DataField="Fransize" HeaderText="Prēmija" />
                    <telerik:GridButtonColumn ConfirmText="Dzēst apdrošināto?" ConfirmDialogType="RadWindow"
                                              ConfirmTitle="Dzēst" ButtonType="ImageButton" CommandName="Delete"  ItemStyle-Width="10"  />
                </Columns>
                <EditFormSettings InsertCaption="Pievienot nākamo apdrošināto" CaptionFormatString="Labot apdrošināto" EditFormType="Template" PopUpSettings-Modal="true" >
                    <EditColumn ButtonType="ImageButton"  />
                    <FormTemplate>
                        <table id="tblEditTemplate" cellspacing="1" cellpadding="1" width="350" border="0">
                            <tr>
                            <tr>
                                <td>
                                    Residents:
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkResidents"  Checked ='<%# Bind("IsResident") %>' OnCheckedChanged="chkResidents_OnCheckedChanged" AutoPostBack="True" runat="server" />
                        
                                </td>
                            </tr>
                                <td>
                                    Pers. Kods:
                                </td>
                                <td>
                                    <telerik:RadMaskedTextBox ID="txtPersKods" runat="server" SelectionOnFocus="SelectAll" AutoPostBack="True" Text='<%# Bind( "PersKods") %>'
                                                              PromptChar="_" Width="85px" Mask="###########" OnTextChanged="txtPersKods_OnTextChanged" Visible="true"/>
                                    <%--PromptChar="_" Width="85px" Mask="<0..4><0..9><0..9><0..9><0..9><0..9><0..9><0..9><0..9><0..9><0..9>" OnTextChanged="txtPersKods_OnTextChanged">--%>
                                   <telerik:RadDatePicker ID="dteDzimDate" runat="server" MinDate="1/1/1910" MaxDate="1/1/2015" DbSelectedDate='<%# Bind("SpecDatumsNo") %>' Width="70pt" 
                                                           Calendar-CultureInfo="(Default)"
                                                           DateInput-DateFormat="dd.MM.yyyy" DateInput-DisplayDateFormat="dd.MM.yyyy" Culture="lv-LV"
                                                           DateInput-EnableSingleInputRendering="false" Visible = "false"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vārds:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVards" Text='<%# Bind( "Vards") %>' runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Uzvārds:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUzvards" Text='<%# Bind( "Uzvards") %>' runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Apstaklis:
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlApstaklis" runat="server"  Width="200" SelectedValue ='<%# Bind( "Apstaklis_ID") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Spec. Datums no:
                                </td>
                                <td>

                                    <telerik:RadDatePicker ID="dteSpecDatumsNo" runat="server" MinDate="1/1/1990" DbSelectedDate='<%# Bind("SpecDatumsNo") %>' Width="70pt" 
                                                           Calendar-CultureInfo="(Default)"
                                                           DateInput-DateFormat="dd.MM.yyyy" DateInput-DisplayDateFormat="dd.MM.yyyy" Culture="lv-LV"
                                                           DateInput-EnableSingleInputRendering="false"> </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Spec. Datums līdz:
                                </td>
                                <td>

                                    <telerik:RadDatePicker ID="dteSpecDatumsLi" runat="server" MinDate="1/1/1990" DbSelectedDate='<%# Bind("SpecDatumsLi") %>'  Width="70pt"
                                                           Calendar-CultureInfo="(Default)"
                                                           DateInput-DateFormat="dd.MM.yyyy" DateInput-DisplayDateFormat="dd.MM.yyyy" Culture="lv-LV"
                                                           DateInput-EnableSingleInputRendering="false"> </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Pievienot" : "Labot" %>'
                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="Button2" Text="Atcelt" runat="server" CausesValidation="False" CommandName="Cancel">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>

            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ClientSettings>
                <ClientEvents OnRowDblClick="rowDblClick" />
            </ClientSettings>
        </telerik:RadGrid>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>

    </div>
</asp:Content>
