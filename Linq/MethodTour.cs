using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace TelerikGreed.Linq
{
    public class MethodTour
    {
        static readonly OzolsCopyDataContext linqContext = new OzolsCopyDataContext(TelerikGreed.Properties.Settings.Default.DBSConnection);

        public static List<TouristInfo> GetTouristList(int intPolNumber, int intTerritoryID)
        {
            var lstTourists = (from oneRow in linqContext.pusT_PolTuristiSaraksts
                               where oneRow.Polises_ID == intPolNumber
                               orderby oneRow.PolTuristiSaraksts
                               select new TouristInfo()
                               {
                                   PolTuristiSaraksts = oneRow.PolTuristiSaraksts,
                                   Vards = oneRow.Vards,
                                   Uzvards = oneRow.Uzvards,
                                   PersKods = oneRow.PersKods,
                                   ApdrNemajs = oneRow.ApdrNemajs.Value,
                                   Apstaklis_ID = oneRow.Apstaklis_ID.Value,
                                   SpecDatumsNo = oneRow.SpecDatumsNo,
                                   SpecDatumsLi = oneRow.SpecDatumsLi,
                                   Fransize = oneRow.Fransize.Value,
                                   PolDarbDienas = oneRow.PolDarbDienas.Value,
                                   DzDatums = oneRow.DzDatums,
                                   IsResident = oneRow.IsResident,
                                   HomeAddress = oneRow.HomeAddress,
                                   GuestAddress = oneRow.GuestAddress,
                                   IsLegal = oneRow.IsLegal.Value,
                                   PassID = oneRow.PassID
                               }).ToList();

            List<TouristApstInfo> lstApst = GetApstList(intTerritoryID);
            foreach (TouristInfo n in lstTourists)
                n.Apstaklis = lstApst.Find(e => e.TuristApstakli_ID == (n.Apstaklis_ID.HasValue ? n.Apstaklis_ID.Value : 0)).TuristApstakli;
            
            return lstTourists;
        }

        public static List<TouristApstInfo> GetApstList(int intTerritoryID)
        {
            var lstApst = (from oneRow in linqContext.pusT_kTuristApstakliTarifs
                           where oneRow.TuristTeritorija_ID == intTerritoryID
                           orderby oneRow.TuristApstakli
                           select new TouristApstInfo()
                           {
                               TuristApstakli_ID = oneRow.TuristApstakli_ID,
                               TuristApstakli = oneRow.TuristApstakli
                           }).ToList();
            lstApst.Insert(0, new TouristApstInfo()
            {
                TuristApstakli_ID = 0,
                TuristApstakli = "---"
            });
            return lstApst;
        }

        public static void FillApstDDL(RadComboBox ddlApst, int intSelected, List<TouristApstInfo> lstApst)
        {
            try
            {
                ddlApst.DataSource = lstApst;
                ddlApst.DataTextField = "TuristApstakli";
                ddlApst.DataValueField = "TuristApstakli_ID";
                ddlApst.DataBind();
                ddlApst.SelectedValue = intSelected.ToString();
            }
            catch (Exception)
            {
            }
        }

        public static void DeleteTouristFromList(List<TouristInfo> lstTourists, GridEditableItem editableItem)
        {
            var intTouristId = (int)editableItem.GetDataKeyValue("PolTuristiSaraksts");
            var itemTourist = lstTourists.Where(n => n.PolTuristiSaraksts == intTouristId).FirstOrDefault();
            lstTourists.Remove(itemTourist);
        }

        public static void UpdateTouristFromList(List<TouristInfo> lstTourists, int intTerritoryID, GridEditableItem editableItem)
        {
            var intTouristId = (int)editableItem.GetDataKeyValue("PolTuristiSaraksts");
            var itemTourist = lstTourists.Where(n => n.PolTuristiSaraksts == intTouristId).FirstOrDefault();
            
            if (itemTourist != null)
            {
                editableItem.UpdateValues(itemTourist);
                itemTourist.Apstaklis = ((RadComboBox)editableItem.FindControl("ddlApstaklis")).Text;
                int intSelectedIndex = ((RadComboBox)editableItem.FindControl("ddlApstaklis")).SelectedIndex;
                itemTourist.Apstaklis_ID = GetApstList(intTerritoryID)[intSelectedIndex].TuristApstakli_ID;

                itemTourist.IsResident = ((CheckBox)editableItem.FindControl("chkResidents")).Checked;
                if (itemTourist.IsResident)
                {
                    itemTourist.DzDatums = null;
                }
                else
                {
                    itemTourist.PersKods = string.Empty;
                }
            }
        }

        public static TouristInfo GetTouristVardUzvard(string strPersKods)
        {
            return (from oneRow in linqContext.pusT_PolTuristiSaraksts
                    where oneRow.PersKods.Equals(strPersKods)
                    select new TouristInfo()
                    {
                        Vards = oneRow.Vards,
                        Uzvards = oneRow.Uzvards
                    }).FirstOrDefault();
        }
    }
}