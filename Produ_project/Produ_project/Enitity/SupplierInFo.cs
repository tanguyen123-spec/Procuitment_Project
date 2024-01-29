using System;
using System.Collections.Generic;

namespace Produ_project.Enitity
{
    public partial class SupplierInFo
    {
        public string? SlId { get; set; }
        public string? SupplierName { get; set; }
        public string? CategoriesId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? EstablishedYear { get; set; }
        public int? Numberofworkers { get; set; }
        public string? MainProductId { get; set; }
        public string? Moq { get; set; }
        public string? Certificate { get; set; }
        public string? Customized { get; set; }
        public string? SampleProcess { get; set; }
        public string? Leadtime { get; set; }
        public bool? ExportUs { get; set; }
        public string? Websitelink { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ContactPerson { get; set; }
        public string? Note { get; set; }
        public string? UserId { get; set; }
        public bool? ReviewQa { get; set; }
        public DateTime? DateQa { get; set; }

        public virtual Category? Categories { get; set; }
        public virtual MainProduct? MainProduct { get; set; }
        public virtual User? User { get; set; }
    }
}
