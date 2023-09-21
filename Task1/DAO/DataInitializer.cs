namespace Task1.DAO;
public static class DataInitializer
{
    public static void InitializeData(this SampleContext context)
    {
        if (context.Cataloges.Any()) return;
        var cataloges = new[]
        {
            new Catalog { Id = 1, Name = "Creating Digital Images", ParentId = null },
            new Catalog { Id = 2, Name = "Resources", ParentId = 1 },
            new Catalog { Id = 3, Name = "Evidence", ParentId = 1 },
            new Catalog { Id = 4, Name = "Graphic Products", ParentId = 1 },
            new Catalog { Id = 5, Name = "Primary Sources", ParentId = 2 },
            new Catalog { Id = 6, Name = "Secondary Sources", ParentId = 2 },
            new Catalog { Id = 7, Name = "Process", ParentId = 4 },
            new Catalog { Id = 8, Name = "Final Product", ParentId = 4},
        };
        context.Cataloges.AddRange(cataloges);
        context.SaveChanges();
    }
}