using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.BusinessLogic
{
    public class JsonResultModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public JsonResultModel(int Code, string Message, object Data) {
            code = Code;
            message = Message;
            data = Data;
        }
    }
}
