using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItemsRental_FE.Models
{
    public class Workspace
    {
        public int WorkspaceId { get; set; }
        [Display(Name = "Work space")]
        public string Workspace1 { get; set; }
    }
}