using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TennisPlayerAPI.Models
{
    public class PlayerData
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; }

        public PlayerData() { }
        public PlayerData(int id, int rank, int points, int weight, int height, int age, List<int> last)
        {
            Id = id;
            Rank = rank;
            Points = points;
            Weight = weight;
            Height = height;
            Age = age;
            Last = last;
        }

    }
}
