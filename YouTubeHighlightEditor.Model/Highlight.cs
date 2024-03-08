using CsvHelper.Configuration.Attributes;

namespace YouTubeHighlightEditor.Model;
public class Highlight : IComparable
{
	// デフォルトコンストラクタがないと読み込み時に
	// CsvHelper.HeaderValidationExceptionが発生するため
	public Highlight() { }

	public Highlight(DateTime deliveredOn, Trigger trigger, Channel channel, string description, string youTubeUrl)
	{
		this.deliveredOn = deliveredOn;
		Trigger = trigger;
		Channel = channel;
		Description = description;
		YouTubeUrl = youTubeUrl;
	}

	private DateTime deliveredOn;
	[Index(0), Name("配信日"), Format("yyyy-MM-dd")]
	public DateTime DeliveredOn
	{
		get => deliveredOn + TimeSpan.FromSeconds(ExtractTime(YouTubeUrl));
		set => deliveredOn = value;
	}

	[Index(1), Name("トリガー")]
	public Trigger Trigger { get; set; }

	[Index(2), Name("チャンネル")]
	public Channel Channel { get; set; }

	[Index(3), Name("説明")]
	public string Description { get; set; }

	// Uri型だとRead/Write時にStackOverflowExceptionが発生する
	[Index(4), Name("YouTubeのURL")]
	public string YouTubeUrl { get; set; }

	// static もしくは readonlyだとバインドできないため分けて定義している
	[Ignore]
	public Dictionary<Trigger, string> TriggerToDisplays { get; } = triggerToDisplays;
	[Ignore]
	public static readonly Dictionary<Trigger, string> triggerToDisplays = new()
	{
		{Trigger.Comment, "コメント"},
		{Trigger.SuperChat, "スパチャ"},
		{Trigger.Other, "その他"}
	};

	[Ignore]
	public Dictionary<Channel, string> ChannelToDisplays { get; } = channelToDisplays;
	[Ignore]
	public static readonly Dictionary<Channel, string> channelToDisplays = new()
	{
		{Channel.Nagi, "紫陽花凪"},
		{Channel.Shiomaru, "しおまる。"},
		{Channel.Homaru, "鈴祈ほまる"},
		{Channel.Mairi, "織姫まいり"}
	};

	public int CompareTo(object obj)
	{
		Highlight other = (Highlight)obj;
		int primary = DeliveredOn.CompareTo(other.DeliveredOn);
		if (primary != 0) return primary;

		return ExtractTime(YouTubeUrl).CompareTo(ExtractTime(other.YouTubeUrl));
	}

	private static int ExtractTime(string url)
	{
		const string parameter = "&t=";
		int start = url.IndexOf(parameter) + parameter.Length;
		return int.Parse(url.Substring(start, url.Length - "s".Length - start));
	}
}
