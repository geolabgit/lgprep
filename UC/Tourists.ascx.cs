using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;

namespace TelerikGreed.UC
{

    public partial class TouristsUC : System.Web.UI.UserControl
    {
        #region Definitions
        public event GridCommandEventHandler onTouristDeleted;
        public event GridCommandEventHandler onTouristInserted;
        public event GridCommandEventHandler onTouristUpdated;

        public List<TouristInfo> TouristsList
        {
            get
            {
                return (List<TouristInfo>)Session["touristsList"];
            }
            set
            {
                Session["touristsList"] = value;
            }
        }
        private GridEditableItem EditableItem
        {
            get
            {
                return (GridEditableItem)Session["editableItem"];
            }
            set
            {
                Session["editableItem"] = value;
            }
        }
        public int TerritoryID
        {
            get
            {
                return (int)ViewState["territoryID"];
            }
            set
            {
                ViewState["territoryID"] = value;
            }
        }
        #endregion
  
        protected void OnInit(EventArgs e)
        {
            grdTouristsList.DeleteCommand += new GridCommandEventHandler(onTouristDeleted);
            grdTouristsList.InsertCommand += new GridCommandEventHandler(onTouristInserted);
            grdTouristsList.UpdateCommand += new GridCommandEventHandler(onTouristUpdated);
        }

        protected void grdTouristsList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.grdTouristsList.DataSource = TouristsList;
        }

        protected void grdTouristsList_ItemCreated(object sender, GridItemEventArgs e)
        { 
            if (e.Item is GridEditableItem && (e.Item.IsInEditMode))
            {
                EditableItem = (GridEditableItem)e.Item;
                SetupInputManager(EditableItem);
            }
        }

        private void SetupInputManager(GridEditableItem editableItem)
        {
            var ddlApstaklisontrol = (RadComboBox)editableItem.FindControl("ddlApstaklis");
            MethodTour.FillApstDDL(ddlApstaklisontrol, 0, MethodTour.GetApstList(TerritoryID));
            if (editableItem.ItemIndex > -1)
            {
                var intTouristId = (int)editableItem.GetDataKeyValue("PolTuristiSaraksts");
                var itemTourist = TouristsList.Where(n => n.PolTuristiSaraksts == intTouristId).FirstOrDefault();
                ((RadMaskedTextBox)editableItem.FindControl("txtPersKods")).Visible = itemTourist.IsResident;
                ((RadDatePicker)editableItem.FindControl("dteDzimDate")).Visible = !itemTourist.IsResident;
            }
        }

        #region Insert, Update, Delete in grid

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
            if (values["DzDatums"] != null)
            {
                itemTourist.DzDatums = (DateTime)values["DzDatums"];
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
            itemTourist.Apstaklis_ID = MethodTour.GetApstList(TerritoryID)[intSelectedIndex].TuristApstakli_ID;
            itemTourist.Apstaklis = ((RadComboBox)editableItem.FindControl("ddlApstaklis")).Text;

            TouristsList.Add(itemTourist);
            this.grdTouristsList.DataSource = TouristsList;
            if (onTouristInserted != null) onTouristInserted(this, e);
        }

        protected void grdTouristsList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            MethodTour.DeleteTouristFromList(TouristsList, (GridEditableItem)e.Item);
            this.grdTouristsList.DataSource = TouristsList;
            if (onTouristDeleted != null) onTouristDeleted(this, e);
           
        }

        protected void grdTouristsList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            MethodTour.UpdateTouristFromList(TouristsList, TerritoryID, (GridEditableItem)e.Item);
            this.grdTouristsList.DataSource = TouristsList;
            if (onTouristUpdated != null) onTouristUpdated(this, e);
           
        }

        #endregion

        #region Fields events
        protected void txtPersKods_OnTextChanged(object sender, System.EventArgs e)
        {
            var txtPersKods = (RadMaskedTextBox)EditableItem.FindControl("txtPersKods");
            if (txtPersKods.Text.Length < 11)
            {
                txtPersKods.Focus();
                return;
            }
            var TouristVU = MethodTour.GetTouristVardUzvard(txtPersKods.Text);
            var txtVards = (TextBox)EditableItem.FindControl("txtVards");
            var txtUzVards = (TextBox)EditableItem.FindControl("txtUzVards");
            if (TouristVU != null)
            {
                txtVards.Text = TouristVU.Vards;
                txtUzVards.Text = TouristVU.Uzvards;
            }
            else
            {
                txtVards.Text = string.Empty;
                txtUzVards.Text = string.Empty;
            }
            txtVards.Focus();
        }

        protected void chkResidents_OnCheckedChanged(object sender, System.EventArgs e)
        {
            var blnResidents = ((CheckBox)EditableItem.FindControl("chkResidents")).Checked;

            ((RadMaskedTextBox)EditableItem.FindControl("txtPersKods")).Visible = blnResidents;
            ((RadDatePicker)EditableItem.FindControl("dteDzimDate")).Visible = !blnResidents;
        }
        #endregion
    }
}