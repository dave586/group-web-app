using SnapFramework.EFModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Models
{
    public class SelectPrePostSessionModel
    {
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int IntakeID { get; set; }

        public bool IsPreSession { get { return SelectedPrePostSession == "IsPreSession"; } }

        public bool IsPostSession { get { return SelectedPrePostSession == "IsPostSession"; } }

        public string SelectedPrePostSession { get; set; }


        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        
        public int ClientBD_Day { get; set; }
        public int ClientBD_Month { get; set; }
        public int ClientBD_Year { get; set; }

        public DateTime SessionTimeStamp { get; set; }

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

                return LanguageList.Select(l => new SelectListItem { Text = l.Name, Value = l.Language.ToString(), Selected = l.Language == "en-CA" }).ToList();
            }
        }

    }
}