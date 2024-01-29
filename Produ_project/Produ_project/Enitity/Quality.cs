using System;
using System.Collections.Generic;

namespace Produ_project.Enitity
{
    public partial class Quality
    {
        public string Awid { get; set; } = null!;
        public int? Pcscustomer { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Note { get; set; }

        public virtual ArtWork Aw { get; set; } = null!;
    }
}
