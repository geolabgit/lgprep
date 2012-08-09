using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;
using TelerikGreed.Linq;

namespace TelerikGreed
{
    public partial class _Default : System.Web.UI.Page
    {
        //List<TouristInfo> lstTourists;
        int intTerritoryID = 1;
        int intTouristsTableID = 2820052;  //   2819550
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session["lstTourists"] = MethodTour.GetTouristList(intTouristsTableID, intTerritoryID);
        }

        protected void grdTouristsList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.grdTouristsList.DataSource = (List<TouristInfo>)Session["lstTourists"];
        }

        private void ShowErrorMessage()
        {
            RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"Please enter valid data!\")"));
        }

        protected void grdTouristsList_ItemCreated(object sender, GridItemEventArgs e)
        { 
            if (e.Item is GridEditableItem && (e.Item.IsInEditMode))
            {
                Session["editableItem"] = (GridEditableItem)e.Item;
                SetupInputManager((GridEditableItem)e.Item);
            }
        }

        private void SetupInputManager(GridEditableItem editableItem)
        {
            var ddlApstaklisontrol = (RadComboBox)editableItem.FindControl("ddlApstaklis");
            MethodTour.FillApstDDL(ddlApstaklisontrol, 0, MethodTour.GetApstList(intTerritoryID));
            
        }

        protected void grdTouristsList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            //create new entity
            var itemTourist = new TouristInfo();
            //populate its properties
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            if (values["PersKods"] != null)
            {
                itemTourist.PersKods = (string)values["PersKods"];
            }
            if (values["Vards"] != null)
            {
                itemTourist.Vards = (string)values["Vards"];
            }
            if (values["Uzvards"] != null)
            {
                itemTourist.Uzvards = (string)values["Uzvards"];
            }

            if (values["SpecDatumsNo"] != null)
            {
                itemTourist.SpecDatumsNo = (DateTime)values["SpecDatumsNo"];
            }
            if (values["SpecDatumsLi"] != null)
            {
                itemTourist.SpecDatumsLi = (DateTime)values["SpecDatumsLi"];
            }
            itemTourist.IsResident = ((CheckBox)editableItem.FindControl("chkResidents")).Checked;
            int intSelectedIndex = ((RadComboBox)editableItem.FindControl("ddlApstaklis")).SelectedIndex;
            itemTourist.Apstaklis_ID = MethodTour.GetApstList(intTerritoryID)[intSelectedIndex].TuristApstakli_ID;
            itemTourist.Apstaklis = ((RadComboBox)editableItem.FindControl("ddlApstaklis")).Text;

            ((List<TouristInfo>)Session["lstTourists"]).Add(itemTourist);
            this.grdTouristsList.DataSource = (List<TouristInfo>)Session["lstTourists"];
        }

        protected void grdTouristsList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var intTouristId = (int)((GridDataItem)e.Item).GetDataKeyValue("PolTuristiSaraksts");
            MethodTour.DeleteTouristFromList((List<TouristInfo>)Session["lstTourists"], intTouristId);
            this.grdTouristsList.DataSource = (List<TouristInfo>)Session["lstTourists"];
        }

        protected void grdTouristsList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var intTouristId = (int)editableItem.GetDataKeyValue("PolTuristiSaraksts");

            MethodTour.UpdateTouristFromList((List<TouristInfo>)Session["lstTourists"], intTouristId, intTerritoryID, editableItem);
            this.grdTouristsList.DataSource = (List<TouristInfo>)Session["lstTourists"];
        }

        protected void txtPersKods_OnTextChanged(object sender, System.EventArgs e)
        {
            var editableItem = (GridEditableItem)Session["editableItem"];
            var txtPersKods = (RadMaskedTextBox)editableItem.FindControl("txtPersKods");
            if (txtPersKods.Text.Length < 11)
            {
                txtPersKods.Focus();
                return;
            }
            var TouristVU = MethodTour.GetTouristVardUzvard(txtPersKods.Text);
            if (TouristVU == null)
            {
                ShowErrorMessage();
                txtPersKods.Focus();
            }
            else
            {
                var txtVards = (TextBox)editableItem.FindControl("txtVards");
                var txtUzVards = (TextBox)editableItem.FindControl("txtUzVards");
                txtVards.Text = TouristVU.Vards;
                txtUzVards.Text = TouristVU.Uzvards;
                txtVards.Focus();
            }
        }

        protected void chkResidents_OnCheckedChanged(object sender, System.EventArgs e)
        {
            var editableItem = (GridEditableItem)Session["editableItem"];
            var blnResidents = ((CheckBox)editableItem.FindControl("chkResidents")).Checked;

            ((RadMaskedTextBox)editableItem.FindControl("txtPersKods")).Visible = blnResidents;
            ((RadDatePicker)editableItem.FindControl("dteDzimDate")).Visible = !blnResidents;
        }
    }
}