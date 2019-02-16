namespace GDL.Sampark.Models
{
    [Dapper.Table("A00000")]
    public class Members
    {
        public int PART_NO { get; set; }

        public int Slno { get; set; }

        public string HOUSE_NO { get; set; }

        public int SlnInParts { get; set; }

        public string FM_NAME { get; set; }

        public string LastName { get; set; }

        public string RLN_TYPE { get; set; }

        public string RLN_FM_NM { get; set; }

        public string RLN_L_NM { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string IDCARD_NO { get; set; }

        public string AREAID { get; set; }

        public string STATUSTYPE { get; set; }

        public string PollingName { get; set; }

        public string FullName => $"{FM_NAME} {LastName}";

        public string RelativeFullName => $"{RLN_FM_NM} {RLN_L_NM}";
    }
}