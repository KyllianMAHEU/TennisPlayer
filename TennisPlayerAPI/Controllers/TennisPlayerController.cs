using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisPlayerAPI.Models;
using TennisPlayerAPI.Data;
using TennisPlayerAPI.DTO;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;

namespace TennisPlayerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TennisPlayerController : ControllerBase
    {
        private readonly ApiContext _context;

        public TennisPlayerController(ApiContext context)
        {
            _context = context;
            InitPlayers();
        }


        internal void SavesNewPlayer(Country country, PlayerData data, TennisPlayer player)
        {
            var result = _context.Countries.Find(country.Code);
            if(result == null)
            {
                _context.Countries.Add(country);
            }
            
            _context.PlayerDatas.Add(data);
            _context.TennisPlayers.Add(player);
            _context.SaveChanges();
        }
        internal void InitPlayers()
        {

            var result = _context.TennisPlayers.ToList();
            
            if(result.Count == 0)
            {
                Country Serbie = new Country("https://tenisu.latelier.co/resources/Serbie.png", "SRB");
                PlayerData data1= new PlayerData(1, 2, 2542, 80000, 188, 31, [1, 1, 1, 1, 1]);
                TennisPlayer player1 = new TennisPlayer(52, "Novak", "Djokovic", "N.DJO", 'M', "SRB", "https://tenisu.latelier.co/resources/Djokovic.png", 1);
                SavesNewPlayer(Serbie, data1, player1 );

                Country Usa = new Country("https://tenisu.latelier.co/resources/USA.png", "USA");
                PlayerData data2 = new PlayerData(2, 52, 1105, 74000, 185, 38, [0, 1, 0, 0, 1]);
                TennisPlayer player2 = new TennisPlayer(95, "Venus", "Williams", "V.WIL", 'F', "USA", "https://tenisu.latelier.co/resources/Venus.webp", 2);
                SavesNewPlayer(Usa, data2, player2);

                Country Sui = new Country("https://tenisu.latelier.co/resources/Suisse.png", "SUI");
                PlayerData data3 = new PlayerData(3, 21, 1784, 81000, 183, 33, [1, 1, 1, 0, 1]);
                TennisPlayer player3 = new TennisPlayer(65, "Stan", "Wawrinka", "S.WAW", 'M', "SUI", "https://tenisu.latelier.co/resources/Wawrinka.png", 3);
                SavesNewPlayer(Sui, data3, player3);

                PlayerData data4 = new PlayerData(4, 10, 3521, 72000, 175, 37, [0, 1, 1, 1, 0]);
                TennisPlayer player4 = new TennisPlayer(102, "Serena", "Williams", "S.WIL", 'F', "USA", "https://tenisu.latelier.co/resources/Serena.png", 4);
                SavesNewPlayer(Usa, data4, player4);

                Country Esp = new Country("https://tenisu.latelier.co/resources/Espagne.png", "ESP");
                PlayerData data5 = new PlayerData(5, 1, 1982, 85000, 185, 33, [1, 0, 0, 0, 1]);
                TennisPlayer player5 = new TennisPlayer(17, "Rafael", "Nadal", "R.NAD", 'M', "ESP", "https://tenisu.latelier.co/resources/Nadal.pngg", 5);
                SavesNewPlayer(Esp, data5, player5);

            }
            

        }
        // Utility functions
        internal PlayerDTO convertPlayertoPlayerDTO(TennisPlayer player)
        {
            Country tempC = _context.Countries.Find(player.CountryCode);
            PlayerData tempD = _context.PlayerDatas.Find(player.PlayerDataId);
            PlayerDTO playerDTO = new PlayerDTO(player, tempC, tempD);
            return playerDTO;
        }

        internal List<PlayerDTO> GetAllPlayerDTO()
        {
            List<PlayerDTO> allPlayerDTO = new List<PlayerDTO>();
            var result = _context.TennisPlayers.ToList();
            foreach (TennisPlayer tp in result)
            {
                allPlayerDTO.Add(convertPlayertoPlayerDTO(tp));
            }
            return allPlayerDTO;
        }

        // Stats Functions
        internal int GetHeightMediane(List<PlayerDTO> allPlayerDTO)
        {

            float avgIMC = 0;

            // Mediane
            allPlayerDTO = allPlayerDTO.OrderBy(x => x.Data.Height).ToList();
            int pos = (allPlayerDTO.Count() / 2) + 1;
            int mediane = allPlayerDTO[pos].Data.Height;
            return mediane;
        }

        internal float GetAvgIMC(List<PlayerDTO> allPlayerDTO)
        {
            float avgIMC = 0;
            foreach (PlayerDTO playerDTO in allPlayerDTO)
            {
                avgIMC += playerDTO.Data.Imc();
            }
            avgIMC /= allPlayerDTO.Count();
            return avgIMC;
        }

        internal string GetBestWinRatio(List<PlayerDTO> allPlayerDTO)
        {
            allPlayerDTO = allPlayerDTO.OrderBy(x => x.Country.Code).ToList();
            int count = 0;
            int currentWin = 0;
            int bestRatio = 0;
            string bestCountry = "";
            string lastCountryCode = allPlayerDTO[0].Country.Code;

            foreach (PlayerDTO playerDTO in allPlayerDTO)
            {
                if (playerDTO.Country.Code != lastCountryCode)
                {
                    if (currentWin / count > bestRatio)
                    {
                        bestRatio = currentWin / count;
                        bestCountry = lastCountryCode;
                    }
                    count = 0;
                    currentWin = 0;
                }
                count++;
                foreach (int win in playerDTO.Data.Last)
                {
                    currentWin += win;
                }
                lastCountryCode = playerDTO.Country.Code;
            }
            if (currentWin / count > bestRatio)
            {
                bestCountry = lastCountryCode;
            }
            return bestCountry;

        }

        // GET Functions

        // Get Player by Id
        [HttpGet]
        public JsonResult GetPlayerById(int id)
        {
            var result = _context.TennisPlayers.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            PlayerDTO player = convertPlayertoPlayerDTO(result);

            return new JsonResult(Ok(player));
        }

        // Get All Player
        [HttpGet]

        public JsonResult GetAllPlayers()
        {
            List<PlayerDTO> allPlayerDTO = GetAllPlayerDTO();

            allPlayerDTO = allPlayerDTO.OrderBy(x => x.Data.Rank).ToList();
            return new JsonResult(Ok(allPlayerDTO));
        }

        // Get Stats
        [HttpGet]

        public JsonResult GetStats()
        {
            List<PlayerDTO> allPlayerDTO = GetAllPlayerDTO();

            StatsDTO stats = new StatsDTO(GetBestWinRatio(allPlayerDTO), GetAvgIMC(allPlayerDTO), GetHeightMediane(allPlayerDTO));

            return new JsonResult(Ok(stats));
        }

    }
}
