namespace Project_NZWalks.API.Querying
{
    public class QueryWalks
    {
        public string? WalksName { get; set; } = null;
        public string? RegionName { get; set; } = null;
        public string? DifficulryLevel { get; set; } = null;
        public bool SortByDistance { get; set; } = false;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
