using System.ComponentModel.DataAnnotations;

namespace TennisPlayerAPI.Models
{
    public class Country
    {
        public string Picture { get; set; }
        [Key]
        public string Code { get; set; }

        public Country(string picture, string code)
        {
            Picture = picture;
            Code = code;
        }

        public Country() { }

        

    }
}
