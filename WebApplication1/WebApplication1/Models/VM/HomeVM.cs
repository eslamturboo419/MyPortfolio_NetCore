using System.Collections.Generic;

namespace WebApplication1.Models.VM
{
    public class HomeVM
    {
            public Owner Owner { get; set; }
            public List<PortfolioItem> PortfolioItems { get; set; }
    }

}
