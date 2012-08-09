using System;
using System.Web.UI.WebControls;

namespace TelerikGreed.Linq
{
    public class TouristInfo
    {
        private bool _checked;
        public int PolTuristiSaraksts { get; set; }
        public int Polises_ID { get; set; }
        public string Vards { get; set; }
        public string Uzvards { get; set; }
        public string PersKods { get; set; }
        public bool? ApdrNemajs { get; set; }
        public int? Apstaklis_ID { get; set; }
        public string Apstaklis { get; set; }
        public DateTime? SpecDatumsNo { get; set; }
        public DateTime? SpecDatumsLi { get; set; }
        public decimal Fransize { get; set; }
        public int? PolDarbDienas { get; set; }
        public DateTime? DzDatums { get; set; }
        public bool IsResident  { get; set; }
        public string HomeAddress { get; set; }
        public string GuestAddress { get; set; }
        public bool? IsLegal { get; set; }
        public string PassID { get; set; }
    }

    public class TouristApstInfo
    {
        public int TuristApstakli_ID { get; set; }
        public int TuristTeritorija_ID { get; set; }
        public string TuristApstakli { get; set; }
        public decimal Koef { get; set; }
        public DateTime? DatumsNo { get; set; }
        public DateTime? DatumsLi { get; set; }
        public int TarifGroup { get; set; }
    }

    //public class CustomCheckBox : CheckBox
    //{
    //    private bool _checked;

    //    public CustomCheckBox()
    //    {
    //        _checked = base.Checked;
    //    }

    //    public override bool Checked
    //    {
    //        get
    //        {
    //            return _checked;
    //        }
    //        set
    //        {
    //            if (value.GetType() == DBNull.Value.GetType())
    //                _checked = false;
    //            else if (value.GetType() == typeof(bool))
    //                _checked = (bool)value;
    //            else
    //                _checked = false;
    //        }
    //    }
    //} 
}