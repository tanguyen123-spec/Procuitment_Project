using System;
using System.Collections.Generic;

namespace Produ_project.Enitity
{
    public partial class Category
    {
        public Category()
        {
            MainProducts = new HashSet<MainProduct>();
        }

        public string CategoriesId { get; set; } = null!;
        public string? NameCategories { get; set; }

        public virtual ICollection<MainProduct> MainProducts { get; set; }
    }
}
