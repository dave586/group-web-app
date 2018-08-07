using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Attributes;
using SnapFramework;
using SnapFramework.EFModel;

namespace Questionnaire.Models
{
    public enum Months
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public class ValidateClient
    {
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        [Display(Name = "ClientFirstName", ResourceType = typeof(Resources.Home.Models.Resource))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        public string ClientFirstName { get; set; }

        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        [Display(Name = "ClientLastName", ResourceType = typeof(Resources.Home.Models.Resource))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        public string ClientLastName { get; set; }

        //[Required(ErrorMessageResourceName = "BirthDateRequired", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        [Display(Name = "ClientBirthDate", ResourceType = typeof(Resources.Home.Models.Resource))]
        [ValidBirthDate]
        public DateTime? ClientBirthDate { get; set; }

        [Display(Name = "Day", ResourceType = typeof(Resources.Home.Models.Resource))]
        public int? ClientBD_Day { get; set; }
        
        [Display(Name = "Month", ResourceType = typeof(Resources.Home.Models.Resource))]
        public int? ClientBD_Month { get; set; }
        
        [Display(Name = "Year", ResourceType = typeof(Resources.Home.Models.Resource))]
        //[ValidYear]
        public int? ClientBD_Year { get; set; }

        public string MonthDisplayStr { get { return Resources.Home.Models.Resource.Month; } }

        public DateTime SessionTimeStamp { get; set; }

        public IList<SelectListItem> MonthSelectList
        {
            get
            {
                var list = new List<SelectListItem>();
                var months = Enum.GetValues(typeof (Months));
                foreach (var month in months)
                {
                    list.Add(new SelectListItem(){Text = month.ToString(), Value = month.GetHashCode().ToString()});
                }

                return list;
            }
        }
        
    }

    public class ValidateChallenge
    {
        [Required]
        [Display(Name = "ClientFirstName", ResourceType = typeof(Resources.Home.Models.Resource))]
        public string ClientFirstName { get; set; }

        [Required]
        [Display(Name = "ClientLastName", ResourceType = typeof(Resources.Home.Models.Resource))]
        public string ClientLastName { get; set; }

        [Required]
        [Display(Name = "ClientBirthDate", ResourceType = typeof(Resources.Home.Models.Resource))]
        public DateTime ClientBirthDate { get; set; }

        public int ClientBD_Day { get; set; }
        public int ClientBD_Month { get; set; }
        public int ClientBD_Year { get; set; }

        public DateTime SessionTimeStamp { get; set; }

        public SnapFramework.Signals.ClientValidation.ValidationResults Results { get; set; }

        [Required(ErrorMessageResourceName = "ChallengeResponseInvalid", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        public string ChallengeResponse { get; set; }
    }

    public class LanguageSelect
    {
        [Required(ErrorMessageResourceName = "LanguageSelectionRequired", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        [Display(Name = "Language", ResourceType = typeof(Resources.Home.Models.Resource))]
        public string Language { get; set; }

        public string SelectLanguageDisplayStr { get { return Resources.Home.Views.Resource.SelectLanguage; } }

        public IList<QuestionnaireLanguage> LanguageList { get; set; }
        public IList<SelectListItem> LanguageSelectList
        {
            get
            {
                if (LanguageList == null || !LanguageList.Any())
                    return new List<SelectListItem>();

                return LanguageList.Select(l => new SelectListItem { Text = l.Name, Value = l.Language.ToString() }).ToList();
            }
        }
        
    }

    public class OverrideClient
    {
        [Required(ErrorMessageResourceName = "IntakeID", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        [Display(Name = "IntakeID", ResourceType = typeof(Resources.Home.Models.Resource))]
        public int? IntakeId { get; set; }

        [Required(ErrorMessageResourceName = "ClientID", ErrorMessageResourceType = typeof(Resources.Home.Views.Resource))]
        [Display(Name = "ClientID", ResourceType = typeof(Resources.Home.Models.Resource))]
        public int? ClientId { get; set; }
    }
}