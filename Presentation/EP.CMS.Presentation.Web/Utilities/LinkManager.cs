using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EP.CMS.Presentation.Web.Utilities
{
    public static class LinkManager
    {


        public static string ViewFrontEndSite()
        {
            return GetConfiguration("frontend-path");
        }

        public static string ViewFrontEndHome(Language language)
        {
            return language == Language.Arabic ?
                GetConfiguration("frontend-ar-path") + GetConfiguration("index-relpath") :
                GetConfiguration("frontend-en-path") + GetConfiguration("index-relpath");
        }


        public static string ViewFrontEndInfo(Info info)
        {

            if (info.LanguageId == (int)Language.Arabic)
            {
                return String.Format(GetConfiguration("frontend-ar-path") + GetConfiguration("info-relpath"), info.LinkedId);
            }
            else
            {
                return String.Format(GetConfiguration("frontend-en-path") + GetConfiguration("info-relpath"), info.Id);
            }
        }

        public static string ViewFrontEndInfoRaw(Language language)
        {
            return language == Language.Arabic ?
                GetConfiguration("frontend-ar-path") + GetConfiguration("info-relpath") :
                GetConfiguration("frontend-en-path") + GetConfiguration("info-relpath");
        }


        public static string ViewFrontEndNews(News news)
        {
            if(news.LanguageId == (int)Language.Arabic)
            {
                return String.Format(GetConfiguration("frontend-ar-path") + GetConfiguration("news-relpath"), news.LinkedId);
            }
            else
            {
                return String.Format(GetConfiguration("frontend-en-path") + GetConfiguration("news-relpath"), news.Id);
            }
        }

        public static string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}