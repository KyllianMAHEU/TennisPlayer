using TennisPlayerAPI.Models;

namespace TennisPlayerAPI.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Shortname { get; set; }
        public char Sex { get; set; }
        public Country Country { get; set; }
        public string Picture { get; set; }
        public PlayerDataDTO Data { get; set; }

        public PlayerDTO() { }

        public PlayerDTO(TennisPlayer player, Country country, PlayerData data)
        {
            Id = player.Id;
            Firstname = player.Firstname;
            Lastname = player.Lastname;
            Shortname = player.Shortname;
            Sex = player.Sex;
            Country = country;
            Picture = player.Picture;
            Data = new PlayerDataDTO(data);
        }
    }
}

