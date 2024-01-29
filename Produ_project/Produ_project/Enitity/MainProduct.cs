using System;
using System.Collections.Generic;

namespace Produ_project.Enitity
{
    public partial class MainProduct
    {
        public MainProduct()
        {
            ArtWorks = new HashSet<ArtWork>();
        }

        public string MainProductId { get; set; } = null!;
        public string? NameMp { get; set; }
        public string? CategoriesId { get; set; }

        public virtual Category? Categories { get; set; }
        public virtual ICollection<ArtWork> ArtWorks { get; set; }
    }
}
