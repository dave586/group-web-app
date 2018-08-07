using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public enum ErrorTypes
    {
        ClientNotFound = 1,
        IntakesNotFound,
        TestNotFound,
        TestNotSaved,
        Exception
    }

    public class ErrorModel
    {
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public DateTime? ClientBirthDate { get; set; }
        public int? ClientBD_Day { get; set; }
        public int? ClientBD_Month { get; set; }
        public int? ClientBD_Year { get; set; }

        public Exception Sys_Exception { get; set; }

        public ErrorTypes? ErrorType { get; set; }
        public string ErrorMessage 
        { 
            get{
                if (ErrorType != null)
                {
                    switch (ErrorType.Value)
                    {
                        case ErrorTypes.ClientNotFound:
                            return Resources.Error.Models.Resource.ClientNotFound;

                        case ErrorTypes.IntakesNotFound:
                            return Resources.Error.Models.Resource.IntakesNotFound;

                        case ErrorTypes.TestNotFound:
                            return Resources.Error.Models.Resource.TestNotFound;

                        case ErrorTypes.TestNotSaved:
                            return Resources.Error.Models.Resource.TestNotSaved;

                        case ErrorTypes.Exception:
                            return Sys_Exception.Message;
                        default:
                            break;
                    }
                }
                return "";
            } 
        }
    }
}