using Microsoft.AspNetCore.Mvc;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.API.Controllers
{
    public class PartController : BaseController<Part, IPartService>
    {
        public PartController(IPartService service) : base(service)
        {
        }
    }
}
