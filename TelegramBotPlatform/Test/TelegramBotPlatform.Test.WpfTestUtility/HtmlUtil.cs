using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using TelegramBotPlatform.Test.WpfTestUtility.Models;

namespace TelegramBotPlatform.Test.WpfTestUtility
{
    public class HtmlTool
    {

        public static OgMeta FetchOg(string url)
        {
            OgMeta meta = new OgMeta();

            string html = FetchHtml(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var list = doc.DocumentNode.SelectNodes("//meta");
            foreach (var node in list)
            {
                if (!node.HasAttributes) continue;
                if (node.Attributes["property"] != null && node.Attributes["content"] != null)
                {
                    switch (node.Attributes["property"].Value)
                    {
                        case "fb:app_id":
                            meta.AppId = node.Attributes["content"].Value;
                            break;
                        case "og:site_name":
                            meta.SiteName = node.Attributes["content"].Value;
                            break;
                        case "og:locale":
                            meta.Locale = node.Attributes["content"].Value;
                            break;
                        case "og:type":
                            meta.Type = node.Attributes["content"].Value;
                            break;
                        case "og:title":
                            meta.Title = node.Attributes["content"].Value;
                            break;
                        case "og:description":
                            meta.Description = node.Attributes["content"].Value;
                            break;
                        case "description":
                            if (meta.Description != null && meta.Description.Length < 1)
                            {
                                meta.Description = node.Attributes["content"].Value;
                            }
                            break;
                        case "og:url":
                            meta.Url = node.Attributes["content"].Value;
                            break;
                        case "og:image":
                            meta.Image = node.Attributes["content"].Value;
                            break;
                        case "og:audio":
                            meta.Audio = node.Attributes["content"].Value;
                            break;
                        case "og:video":
                            meta.Video.Content = node.Attributes["content"].Value;
                            break;
                        case "og:video:type":
                            meta.Video.Type = node.Attributes["content"].Value;
                            break;
                        case "og:video:width":
                            meta.Video.Width = node.Attributes["content"].Value;
                            break;
                        case "og:video:height":
                            meta.Video.Height = node.Attributes["content"].Value;
                            break;
                        case "og:video:tag":
                            meta.Video.Tag = node.Attributes["content"].Value;
                            break;
                        case "og:video:secure_url":
                            meta.Video.SecureUrl = node.Attributes["content"].Value;
                            break;
                    }
                }

                if (node.Attributes["name"] != null && node.Attributes["content"] != null)
                {
                    switch (node.Attributes["name"].Value)
                    {

                        case "description":
                            if (meta.Description != null && meta.Description.Length < 1)
                            {
                                meta.Description = node.Attributes["content"].Value;
                            }
                            break;
                    }
                }
            }

            return meta;
        }

        public static string FetchHtml(string url)
        {
            string o = "";

            try
            {
                HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
                oReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                o = reader.ReadToEnd();
            }
            catch (Exception ex) { }

            return o;
        }

    }
}