using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace YougeneHighlightEditor.Model;
public class Highlight : IComparable
{
	// デフォルトコンストラクタがないと読み込み時に
	// CsvHelper.HeaderValidationExceptionが発生するため
	public Highlight() { }

	public Highlight(DateTime deliveredOn, Trigger trigger, Asterista asterista, string description, string youTubeUrl)
	{
		DeliveredOn = deliveredOn;
		Trigger = trigger;
		Asterista = asterista;
		Description = description;
		YouTubeUrl = youTubeUrl;
	}

	[Index(0), Name("配信日"), Format("yyyy-MM-dd")]
	public DateTime DeliveredOn { get; set; }

	[Index(1), Name("トリガー")]
	public Trigger Trigger { get; set; }

	[Index(2), Name("アスタリスタ")]
	public Asterista Asterista { get; set; }

	[Index(3), Name("説明")]
	public string Description { get; set; }

	// Uri型だとRead/Write時にStackOverflowExceptionが発生する
	[Index(4), Name("YouTubeのURL")]
	public string YouTubeUrl { get; set; }

	public int CompareTo(object obj)
	{
		return DeliveredOn.CompareTo(((Highlight)obj).DeliveredOn);
	}
}
