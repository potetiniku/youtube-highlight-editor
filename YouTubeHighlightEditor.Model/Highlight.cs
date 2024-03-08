using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace YouTubeHighlightEditor.Model;
public class Highlight : IComparable
{
	// デフォルトコンストラクタがないと読み込み時に
	// CsvHelper.HeaderValidationExceptionが発生するため
	public Highlight() { }

	public Highlight(DateTime deliveredOn, Trigger trigger, Asterista asterista, string description, string youTubeUrl)
	{
		this.deliveredOn = deliveredOn;
		Trigger = trigger;
		Asterista = asterista;
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

	[Index(2), Name("アスタリスタ")]
	public Asterista Asterista { get; set; }

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
		{Trigger.CommentYell, "コメント付エール"},
		{Trigger.Yell, "エール"},
		{Trigger.RankingBoard, "強者ボード"},
		{Trigger.Other, "その他"}
	};

	[Ignore]
	public Dictionary<Asterista, string> AsteristaToDisplays { get; } = asteristaToDisplays;
	[Ignore]
	public static readonly Dictionary<Asterista, string> asteristaToDisplays = new()
	{
		{Asterista.Sakura, "さくら"},
		{Asterista.Cosmi, "コズミ"},
		{Asterista.Anya, "アニャ"},
		{Asterista.Noran, "ノラン"},
		{Asterista.Aoha, "アオハ"},
		{Asterista.Moaka, "モアカ"}
	};

	public int CompareTo(object obj)
	{
		var other = (Highlight)obj;
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
