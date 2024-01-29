using System;
using System.Collections.Generic;

namespace Produ_project.Enitity
{
    public partial class ArtWork
    {
        public string Awid { get; set; } = null!;
        public string? NameAw { get; set; }
        public string? MainProductId { get; set; }
        public string? ImgagesUrl { get; set; }

        public virtual MainProduct? MainProduct { get; set; }
        public virtual Quality? Quality { get; set; }
    }
}
