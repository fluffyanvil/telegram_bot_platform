using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Input;
using Ecologylab.BigSemantics.Collecting;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetaMetadataNS;
using HtmlAgilityPack;
using Prism.Commands;
using Prism.Mvvm;
using Simpl.Serialization;

namespace TelegramBotPlatform.Test.WpfTestUtility.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _queryString;
		private ICommand _searchCommand;

		public string QueryString
		{
			get { return _queryString; }
			set
			{
				_queryString = value;
				OnPropertyChanged();
			}
		}

		private const string Url = "http://www.google.com/search?num=100&q={0}";
		public MainWindowViewModel()
		{
			
			SearchResults = new ObservableCollection<string>();
			Metas = new ObservableCollection<Meta>();
			SemanticsSessionScope infoCollector ;
		}

		public ICommand SearchCommand
		{
			get { return _searchCommand ?? (_searchCommand = new DelegateCommand(OnExecuteSearchCommand)); }
		}

		public ObservableCollection<string> SearchResults { get; set; }
		public ObservableCollection<Meta> Metas { get; set; }

		private void OnExecuteSearchCommand()
		{
			var result = new HtmlWeb().Load(string.Format(Url, QueryString));
			//var nodes = result.DocumentNode.SelectNodes("//html//body//div[@class='g']").Select(r => r.InnerHtml).Take(10);
			//SearchResults.Clear();
			//foreach (var node in nodes)
			//{
			//	SearchResults.Add(node);
			//}

			// extract links
			SearchResults.Clear();
			var searchResultCount = 0;
			foreach (var link in result.DocumentNode.SelectNodes("//a[@href]"))
			{
				var hrefValue = link.GetAttributeValue("href", string.Empty);
				if (hrefValue.ToUpper().Contains("GOOGLE") || !hrefValue.Contains("/url?q=") ||
				    !hrefValue.ToUpper().Contains("HTTP://")) continue;
				var index = hrefValue.IndexOf("&", StringComparison.Ordinal);
				if (index <= 0) continue;
				hrefValue = hrefValue.Substring(0, index);
				var searchResultUrl = hrefValue.Replace("/url?q=", "");
				if (searchResultCount == 10) continue;
				searchResultCount++;
				SearchResults.Add(searchResultUrl);
				//Metas.Add(LoadMeta(searchResultUrl));
			}
		}

		//private void GetMetaTagsDetails(string strUrl)
		//{
		//	//Get Meta Tags
		//	var webGet = new HtmlWeb();
		//	var document = webGet.Load(strUrl);
		//	var metaTags = document.DocumentNode.SelectNodes("//meta");
		//	if (metaTags != null)
		//	{
		//		foreach (var tag in metaTags)
		//		{
		//			if (tag.Attributes["name"] != null && tag.Attributes["content"] != null && tag.Attributes["name"].Value == "description")
		//			{
		//				txtDesc.Text = tag.Attributes["content"].Value;
		//			}

		//			if (tag.Attributes["name"] != null && tag.Attributes["content"] != null && tag.Attributes["name"].Value == "keywords")
		//			{
		//				txtKeywords.Text = tag.Attributes["content"].Value;
		//			}
		//		}
		//	}
		//	List<String> imgs = (from x in doc.Descendants()
		//						 where x.Name.ToLower() == "img"
		//						 select x.Attributes["src"].Value).ToList<String>();
		//}

		private Meta LoadMeta(string url)
		{
			var result = new HtmlWeb().Load(url);
			var doc = result.DocumentNode;
			String title = (from x in doc.Descendants()
							where x.Name.ToLower() == "title"
							select x.InnerText).FirstOrDefault();

			String desc = (from x in doc.Descendants()
						   where x.Name.ToLower() == "meta"
						   && x.Attributes["name"] != null
						   && x.Attributes["name"].Value.ToLower() == "description"
						   select x.Attributes["content"].Value).FirstOrDefault();

			List<String> imgs = (from x in doc.Descendants()
								 where x.Name.ToLower() == "img"
								 select x.Attributes["src"]?.Value).ToList<String>();
			return new Meta() {Description = desc, Title = title, Images = imgs};
		}
	}

	public class Meta
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public IEnumerable<string> Images { get; set; }
	}
}