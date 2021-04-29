using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BaseCore.BusinessLogic;
using Microsoft.Extensions.Configuration;
using BaseCore.MailServices;
using BaseCore.Common.Models.Emails;

namespace BaseCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IMailSerivces mailServices;
        public AccountController(IConfiguration config, IMailSerivces mailService)
        {
            _config = config;
            this.mailServices = mailService;
        }
        [HttpGet]
        [Route("add")]
        public JsonResultModel Token(string s)
        {
            var jwt = new JWTServices(_config);
            var token = jwt.GenerateSecurityToken(DateTime.Now.ToString());
            return new JsonResultModel(1, "thành công", token);
        }
        [HttpPost]
        [Route("sendmail")]
        public async Task<JsonResultModel> SendMail([FromForm]EmailModel emailModel)
        {
            await mailServices.SenMailTemplateHTML(emailModel);
            return new JsonResultModel(200, "send to success", null);
        }
    }
}
