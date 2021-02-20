using System;

namespace Warehouse.DomainModels.Models
{
    public class Result
    {
        public bool Success = true;
        public int Code = 200;
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Object Data { get; set; }
        public long ListCount { get; set; }
    }
}
