using System;
using System.Collections.Generic;
using System.Linq;
using TelerikGreed.UC;

namespace TelerikGreed
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Definitions


        const int intTerritoryID = 1;
        const int intTouristsTableID = 2820052;  //   2819550 

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucTourists.intTerritoryID = intTerritoryID;
                ucTourists.TouristsList = MethodTour.GetTouristList(intTouristsTableID, intTerritoryID);
            }
        }
    }
}