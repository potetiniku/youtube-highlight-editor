using CsvHelper.Configuration.Attributes;

namespace YouTubeHighlightEditor.Model;
public enum Trigger
{
	[Name("その他")]
	Other,

	[Name("コメント")]
	Comment,

	[Name("スパチャ")]
	SuperChat
}
