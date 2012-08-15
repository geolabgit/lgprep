using System;
using System.Linq;
using TelerikGreed.UC;
using Telerik.Web.UI;

namespace TelerikGreed
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Definitions
        const int intTerritoryID = 1;
        const int intPolisesID = 2820052;  //   2819550 2820052

        #endregion
        private void Page_Init(object sender, EventArgs e)
        {
            ucTourists.onTouristDeleted += new GridCommandEventHandler(ucTourists_onTouristDeleted);
            ucTourists.onTouristInserted += new GridCommandEventHandler(ucTourists_onTouristInserted);
            ucTourists.onTouristUpdated += new GridCommandEventHandler(ucTourists_onTouristUpdated);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucTourists.MinSpecDatumsNo = DateTime.Today;
                ucTourists.MaxSpecDatumsNo = DateTime.Today.AddDays(30);
                ucTourists.MinSpecDatumsLi = DateTime.Today.AddDays(1);
                ucTourists.MaxSpecDatumsLi = DateTime.Today.AddDays(180);
                ucTourists.TerritoryID = intTerritoryID;
                ucTourists.TouristsApstList = MethodTour.GetTouristApstList(intTerritoryID);
                ucTourists.TouristsList = MethodTour.GetTouristList(intPolisesID, intTerritoryID);

            }
        }

        protected void ucTourists_onTouristDeleted(object sender, GridCommandEventArgs e)
        {
            this.lblDeleted.Text = "Deleted: " + ((GridEditableItem)e.Item).GetDataKeyValue("PolTuristiSaraksts").ToString();
        }
        protected void ucTourists_onTouristInserted(object sender, GridCommandEventArgs e)
        {
           this.lblInserted.Text = "Inserted: " + ucTourists.TouristsList.Last().Uzvards; 
        }
        protected void ucTourists_onTouristUpdated(object sender, GridCommandEventArgs e)
        {
            this.lblUpdated.Text = "Updated: " + ((GridEditableItem)e.Item).GetDataKeyValue("PolTuristiSaraksts").ToString();
            var intTouristId = (int)((GridEditableItem)e.Item).GetDataKeyValue("PolTuristiSaraksts");
            var itemTourist = ucTourists.TouristsList.Where(n => n.PolTuristiSaraksts == intTouristId).FirstOrDefault();
            itemTourist.Fransize = itemTourist.PolTuristiSaraksts * 5;
        }
    }
}