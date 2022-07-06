namespace API.DTOs
{
    public class VBSDailyGraph
    {
        public string? Type { get; set; } = "line";
        public GraphData? Data { get; set; }
    }
    public class GraphLabel
    {
        public string? Name { get; set; }
    }
    public class GraphData
    {
        public GraphDataset[]? Datasets { get; set; }
    }
    public class GraphDataset
    {
        public int[]? Data { get; set; }
    }
}