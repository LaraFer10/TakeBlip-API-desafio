using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeBlipAPI.Models;
using TakeBlipAPI.Service;

namespace TakeBlipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakeController : ControllerBase
    {
        private readonly GithubAPIService Service;

        public TakeController(GithubAPIService service)
        {
            Service = service;
        }

        public async Task<IEnumerable<Repository>> GetRepositoryListAsync()
        {
            return await Service.ConsultarReposAPIAsync();
        }
    }
}