using System.Windows;
using System.Windows.Controls;

namespace TelegramBotPlatform.Test.WpfTestUtility.Behaviors
{
	public class WebBrowserBehavior
	{
		public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
		"Html",
		typeof(string),
		typeof(WebBrowserBehavior),
		new FrameworkPropertyMetadata(OnHtmlChanged));

		[AttachedPropertyBrowsableForType(typeof(WebBrowser))]
		public static string GetHtml(WebBrowser d)
		{
			return (string)d.GetValue(HtmlProperty);
		}

		public static void SetHtml(WebBrowser d, string value)
		{
			d.SetValue(HtmlProperty, value);
		}

		static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WebBrowser wb = d as WebBrowser;
			if (wb != null)
				wb.NavigateToString(e.NewValue as string);
		}
	}
}