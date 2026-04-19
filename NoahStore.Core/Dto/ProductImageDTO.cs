namespace NoahStore.API.DTOs
{
    public sealed record ProductImageDTO
    {
        public string ImageURL { get; set; }
        public string? AltText { get; set; }

    }
}