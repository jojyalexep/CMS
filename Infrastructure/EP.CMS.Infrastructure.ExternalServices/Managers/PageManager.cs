using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.PageModule;
using EP.CMS.Domain.PageModule.Entities;
using EP.CMS.Infrastructure.ExternalServices.Templates;
using EP.CMS.Infrastructure.ExternalServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Managers
{
    public class PageManager : IPageExtRepository
    {

        RepoService.English.CMSRepositorySEIClient englishService;
        RepoService.Arabic.CMSRepositorySEIClient arabicService;
        public PageManager()
        {

        }


        public string UpdatePage(Page page)
        {
            string response = String.Empty;

            englishService = new RepoService.English.CMSRepositorySEIClient();
            arabicService = new RepoService.Arabic.CMSRepositorySEIClient();

            if (page.GetType() == typeof(News))
            {
                response =  UpdateNews((News)page);
            }

            else if (page.GetType() == typeof(Info))
            {
                response = UpdateInfo((Info)page);
            }
            WSReponseHandler.Handle(response);
            return response;
        }

        public string ReOrderNews(string[] newsIds)
        {
            englishService = new RepoService.English.CMSRepositorySEIClient();
            arabicService = new RepoService.Arabic.CMSRepositorySEIClient();

            string response = englishService.opn_updateContentOrdering("", "NEWS", newsIds);
            response = arabicService.opn_updateContentOrdering("", "NEWS", newsIds);
            WSReponseHandler.Handle(response);
            return response;
        }

        private string UpdateInfo(Info info)
        {
            string response = "";
            if (info.Banner != null)
            {

                if (info.Banner.Data != null)
                {
                    info.Banner.CategoryId = (int)ImageCategoryEnum.Info_Banner;
                    info.Banner.LanguageId = info.LanguageId;
                    response += new BinaryManager().UpdateContent(info.Banner);
                }
                response += UpdateInfoBanner(info);
            }
            response += UpdateInfoStyled(info);
            return response;
        }

        private string UpdateInfoBanner(Info info)
        {
            string response = String.Empty;
            if (info.LanguageId == (int)Language.English)
            {
                var englishBanner = new RepoService.English.styledContent();
                englishBanner.pageID = "infopage";
                englishBanner.zoneID = "article" + info.Id + "_banner";
                englishBanner.styleType = "HTML";
                englishBanner.status = StatusUtil.GetStatusString(info.StatusId, info.IsDeleted);
                englishBanner.content = GetImageBannerHtml(info);
                response = englishService.opn_updateStyledContent("", englishBanner);
            }
            else
            {
                var arabicBanner = new RepoService.Arabic.styledContent();
                arabicBanner.pageID = "infopage";
                arabicBanner.zoneID = "article" + info.LinkedId + "_banner";
                arabicBanner.styleType = "HTML";
                arabicBanner.status = StatusUtil.GetStatusString(info.StatusId, info.IsDeleted);
                arabicBanner.content = GetImageBannerHtml(info);
                response = arabicService.opn_updateStyledContent("", arabicBanner);
            }
            return response;
        }

        private string GetImageBannerHtml(Info info)
        {
            return String.Format(InfoBannerTemplate.GetContent(), info.Title, info.Banner.Id);
        }

        private string UpdateInfoStyled(Info info)
        {
            string response = String.Empty;
            if (info.LanguageId == (int)Language.English)
            {

                var englishContent = new RepoService.English.styledContent();
                englishContent.pageID = "infopage";
                englishContent.zoneID = "article" + info.Id.ToString();
                englishContent.styleType = "HTML";
                englishContent.status = StatusUtil.GetStatusString(info.StatusId, info.IsDeleted);
                englishContent.style = null;
                englishContent.styleLink = null;
                englishContent.title = info.Name;
                englishContent.keyWords = info.Keywords;
                englishContent.summary = info.Summary;
                englishContent.content = info.Content;
                response = englishService.opn_updateStyledContent("", englishContent);
            }
            else
            {
                var arabicContent = new RepoService.Arabic.styledContent();
                arabicContent.pageID = "infopage";
                arabicContent.zoneID = "article" + info.LinkedId.ToString();
                arabicContent.styleType = "HTML";
                arabicContent.status = StatusUtil.GetStatusString(info.StatusId, info.IsDeleted);
                arabicContent.style = null;
                arabicContent.styleLink = null;
                arabicContent.title = info.Name;
                arabicContent.keyWords = info.Keywords;
                arabicContent.summary = info.Summary;
                arabicContent.content = info.Content;
                response = arabicService.opn_updateStyledContent("", arabicContent);
            }

            return response;
        }

        private string UpdateNews(News news)
        {
            string response = String.Empty;
            if (news.NewsImage != null && news.NewsImage.Data != null)
            {
                news.NewsImage.CategoryId = (int)ImageCategoryEnum.News;
                news.NewsImage.LanguageId = news.LanguageId;
                response += new BinaryManager().UpdateContent(news.NewsImage);
            }
            if (news.LanguageId == (int)Language.English)
            {
                var englishNews = new RepoService.English.newsModel();
                englishNews.contextID = "GLOBAL";
                englishNews.newsID = "news" + news.Id.ToString();
                if (news.NewsImage != null)
                    englishNews.newsImageID = "content" + news.NewsImage.Id;
                englishNews.newsTitle = news.Title;
                englishNews.newsSubTitle = String.IsNullOrWhiteSpace(news.SubTitle) ? " " : news.SubTitle;
                englishNews.newsKeyWords = news.Keywords;
                englishNews.newsDate = String.Format("{0:ddMMMyyyy}", news.NewsDate).ToUpper();
                englishNews.newsSummary = news.Summary;
                englishNews.newsReadMore = news.Content;
                englishNews.status = StatusUtil.GetStatusString(news.StatusId, news.IsDeleted);
                response = englishService.opn_updateNews("", englishNews, news.WhatsNew ? "WHATSNEW" : "");
            }
            else
            {
                var arabicNews = new RepoService.Arabic.newsModel();
                arabicNews.contextID = "GLOBAL";
                arabicNews.newsID = "news" + news.LinkedId.ToString();
                if (news.NewsImage != null)
                    arabicNews.newsImageID = "content" + news.NewsImage.Id;
                arabicNews.newsTitle = news.Title;
                arabicNews.newsSubTitle = String.IsNullOrWhiteSpace(news.SubTitle) ? " " : news.SubTitle;
                arabicNews.newsKeyWords = news.Keywords;
                arabicNews.newsDate = String.Format("{0:ddMMMyyyy}", news.NewsDate).ToUpper();
                arabicNews.newsSummary = news.Summary;
                arabicNews.newsReadMore = news.Content;
                arabicNews.status = StatusUtil.GetStatusString(news.StatusId, news.IsDeleted);
                response = arabicService.opn_updateNews("", arabicNews, news.WhatsNew ? "WHATSNEW" : "");
            }
            return response;
        }
    }
}
