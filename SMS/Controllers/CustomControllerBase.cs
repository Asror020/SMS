using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class CustomControllerBase : Controller
    {
        protected readonly IWebHostEnvironment HostEnvironment;
        protected readonly ILogger Logger;
        protected readonly IMapper Mapper;

        public CustomControllerBase(IWebHostEnvironment hostEnvironment, ILogger logger, IMapper mapper)
        {
            HostEnvironment = hostEnvironment;
            Logger = logger;
            Mapper = mapper;
        }
    }
}