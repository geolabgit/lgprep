<%@ Control  Language="C#" AutoEventWireup="true" CodeBehind="Tourists.ascx.cs" Inherits="TelerikGreed.UC.TouristsUC" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript">
        function rowDblClick(sender, eventArgs) {
            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
        }
    </script>

</telerik:RadCodeBlock>
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
                <table id="tblEditTemplate" width="350"  >

                    <tr>
                        <td>
                            &nbsp;Residents:
                        </td>
                        <td>
                            <asp:checkbox ID="chkResidents" 
                                          OnCheckedChanged="chkResidents_OnCheckedChanged" AutoPostBack="True" 
                                          Checked='<%# (Container is GridEditFormInsertItem) ? true : Eval("IsResident") %>'  
                                          runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Pers. Kods:
                        </td>
                        <td>
                            <telerik:RadMaskedTextBox ID="txtPersKods" runat="server" SelectionOnFocus="SelectAll" AutoPostBack="True" Text='<%# Bind("PersKods") %>'
                                                      PromptChar="_" Width="85px" Mask="###########" OnTextChanged="txtPersKods_OnTextChanged" Visible="true"/>
                            <%--PromptChar="_" Width="85px" Mask="<0..4><0..9><0..9><0..9><0..9><0..9><0..9><0..9><0..9><0..9><0..9>" OnTextChanged="txtPersKods_OnTextChanged">--%>
                            <telerik:RadDatePicker ID="dteDzimDate" runat="server" MinDate="1/1/1910" MaxDate="1/1/2015" DbSelectedDate='<%# Bind("DzDatums") %>' Width="70pt" 
                                                   Calendar-CultureInfo="(Default)"
                                                   DateInput-DateFormat="dd.MM.yyyy" DateInput-DisplayDateFormat="dd.MM.yyyy" Culture="lv-LV"
                                                   DateInput-EnableSingleInputRendering="false" Visible = "false"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Vārds:
                        </td>
                        <td>
                            <asp:textbox ID="txtVards" Text='<%# Bind( "Vards") %>' runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Uzvārds:
                        </td>
                        <td>
                            <asp:textbox ID="txtUzvards" Text='<%# Bind( "Uzvards") %>' runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr width="400" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            &nbsp;Apstaklis:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlApstaklis" runat="server"  Width="200" AutoPostBack="True" SelectedValue ='<%# DataBinder.Eval(Container.DataItem, "Apstaklis_ID") %>' 
                            OnSelectedIndexChanged="ddlApstaklis_OnSelectedIndexChanged" DataTextField="TuristApstakli" DataValueField="TuristApstakli_ID" />

                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Spec. Datums no:
                        </td>
                        <td>

                            <telerik:RadDatePicker ID="dteSpecDatumsNo" runat="server" DbSelectedDate='<%# Bind("SpecDatumsNo") %>' Width="70pt" 
                                                   Calendar-CultureInfo="lv-LV" Enabled='<%# (Container is GridEditFormInsertItem) ? false : !(Convert.ToInt32(Eval("Apstaklis_ID")) == 0) %>'
                                                   DateInput-DateFormat="dd.MM.yyyy" DateInput-DisplayDateFormat="dd.MM.yyyy" Culture="lv-LV"
                                                   DateInput-EnableSingleInputRendering="false" />

                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Spec. Datums līdz:
                        </td>
                        <td>

                            <telerik:RadDatePicker ID="dteSpecDatumsLi" runat="server" DbSelectedDate='<%# Bind("SpecDatumsLi") %>'  Width="70pt"
                                                   Calendar-CultureInfo="lv-LV"  Enabled='<%# (Container is GridEditFormInsertItem) ? false : !(Convert.ToInt32(Eval("Apstaklis_ID")) == 0) %>'
                                                   DateInput-DateFormat="dd.MM.yyyy" DateInput-DisplayDateFormat="dd.MM.yyyy" Culture="lv-LV"
                                                   DateInput-EnableSingleInputRendering="false" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td align="right" colspan="2">
                            <asp:button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Pievienot" : "Labot" %>'
                                        runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' />
                            &nbsp;
                            <asp:button ID="Button2" Text="Atcelt" runat="server" CausesValidation="False" CommandName="Cancel" />&nbsp;
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