namespace Domain
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
