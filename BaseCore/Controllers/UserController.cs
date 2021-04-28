using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BaseCore.BusinessLogic;

namespace BaseCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("add")]
        public JsonResultModel CreateUser(string s)
        {
            return new JsonResultModel(1, "thành công", null);
        }
    }
}
