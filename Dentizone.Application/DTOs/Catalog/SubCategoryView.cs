namespace Dentizone.Application.DTOs.Catalog;

public class SubCategoryView
{
    public required string Name { get; set; }
    public required string Id { get; set; }
    public required CategoryView Category { get; set; }
}