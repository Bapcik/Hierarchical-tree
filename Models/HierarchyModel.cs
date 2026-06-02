namespace testProject.Models;

public class HierarchyModel
{

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? ParentId { get; set; }

}
