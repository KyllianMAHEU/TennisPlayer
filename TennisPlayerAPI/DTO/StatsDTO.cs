namespace TennisPlayerAPI.DTO
{
    public class StatsDTO
    {
        public string CountryCode { get; set; }
        public float Imc {  get; set; }
        public int Mediane { get; set; }

        public StatsDTO(string countryCode, float imc, int mediane)
        {
            CountryCode = countryCode;
            Imc = imc;
            Mediane = mediane;
        }
    }
}
