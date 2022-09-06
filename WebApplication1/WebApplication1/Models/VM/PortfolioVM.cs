using Microsoft.AspNetCore.Http;
using System;

namespace WebApplication1.Models.VM
{
    public class PortfolioVM
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImgURL { get; set; }
        public IFormFile File { get; set; }

    }
}
