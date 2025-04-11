namespace TennisPlayerAPI.Models
{
    public class TennisPlayer
    {
        public int Id {  get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Shortname { get; set; }
        public char Sex { get; set; }
        public string CountryCode { get; set; }

        //public Country? Country { get; set; }
        public string Picture { get; set; }

        public int PlayerDataId { get; set; }
        //public PlayerData data { get; set; }
        
        public TennisPlayer(int id, string firstname, string lastname, string shortname, char sex,string countryCode, string picture,int playerDataId)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Shortname = shortname;
            Sex = sex;
            CountryCode = countryCode;
            Picture = picture;
            PlayerDataId = playerDataId;
        }

        public TennisPlayer()
        {

        }
    }
    
    
}
