using TennisPlayerAPI.Models;

namespace TennisPlayerAPI.DTO
{
    public class PlayerDataDTO
    {

        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; }

        public PlayerDataDTO() { }
        public PlayerDataDTO(PlayerData data)
        {
            Rank = data.Rank;
            Points = data.Points;
            Weight = data.Weight;
            Height = data.Height;
            Age = data.Age;
            Last = data.Last;
        }

        public float Imc()
        {
            float w = Weight/1000;
            float h = (float)Height/100;
            float test = w / (h * h);
            return w / (h * h);
        }
    }
}
