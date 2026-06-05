namespace testProject.Models;

public class MockData
{
    public static void Seed(ApplicationContext db)
    {
        // if (db.HierarchyNodes.Any()) return;
        db.HierarchyNodes.RemoveRange(db.HierarchyNodes);
        db.SaveChanges();

        db.HierarchyNodes.AddRange(
            new HierarchyModel { Id = 1, Name = "Layout", ParentId = null },
            new HierarchyModel { Id = 2, Name = "Navigator", ParentId = 1 },
            new HierarchyModel { Id = 3, Name = "TopBar", ParentId = 1 },
            new HierarchyModel { Id = 4, Name = "SiteSetting", ParentId = 1 },
            new HierarchyModel { Id = 5, Name = "Footer", ParentId = 1 },
            new HierarchyModel { Id = 6, Name = "Выход", ParentId = 3 },
            new HierarchyModel { Id = 7, Name = "Язык интерфейса", ParentId = 3 },
            new HierarchyModel { Id = 8, Name = "Калькулятор валют", ParentId = 3 }
        );
        db.SaveChanges();
    }
}