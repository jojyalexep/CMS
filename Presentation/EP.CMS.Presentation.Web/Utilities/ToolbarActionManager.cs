using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EP.CMS.Presentation.Web.Utilities
{
    

    public static class ToolbarActionManager
    {
        public static ToolbarActionEnum GetAction()
        {
            string actionValue = HttpContext.Current.Request["Action"];
            ToolbarActionEnum actionEnum = ToolbarActionEnum.None;
            if(!String.IsNullOrEmpty(actionValue))
            {
                switch(actionValue)
                {
                        case "apply":
                        {
                            actionEnum = ToolbarActionEnum.Apply;
                            break;
                        }
                        case "save":
                        {
                            actionEnum = ToolbarActionEnum.Save;
                            break;
                        }
                        case "savenew":
                        {
                            actionEnum = ToolbarActionEnum.SaveNew;
                            break;
                        }
                        case "cancel":
                        {
                            actionEnum = ToolbarActionEnum.Cancel;
                            break;
                        }
                        default:
                        {
                            actionEnum = ToolbarActionEnum.None;
                            break;
                        }

                }
				return actionEnum;
            }
            else
            {
                throw new ArgumentNullException("Action");
            }

            
        }
    }

    public enum ToolbarActionEnum
    {
        Apply = 1,
        Save = 2,
        SaveNew = 3,
        Cancel = 4,
		None = 5
    }

    public class Toolbar
    {


        public ToolbarActionEnum Action { get; set; }
        public string Url { get; set; }
        public string Name
        {
            get
            {
                return Action.ToString();
            }
        }

        public string Controller
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            }
        }

        public string DefaultUrl
        {
            get
            {
                switch (Action)
                {
                    case ToolbarActionEnum.Apply:
                        {
                            return Controller + "/Apply";
                        }
                    default:
                        {
                            return Controller + "/Index";
                        }
                }
            }
        
        }
        public bool IsAjax { get; set; }

    }


}