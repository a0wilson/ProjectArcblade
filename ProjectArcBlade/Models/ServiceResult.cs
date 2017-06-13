using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    [NotMapped]
    public class ServiceResult<T> where T : class
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public T ReturnValue { get; set; }
        public bool Success { get; set; }
        public bool HasSuccessMessage { get { return Success && (SuccessMessage != String.Empty); } }
    }
}
