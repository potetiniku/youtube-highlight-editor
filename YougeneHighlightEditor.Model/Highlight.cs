using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace YougeneHighlightEditor.Model;
public class Highlight : IComparable
{
	// デフォルトコンストラクタがないと読み込み時にCsvHelper.HeaderValidationExceptionが発生するため
	public Highlight() { }

	public Highlight(DateTime deliveredOn, Trigger trigger, string description, string youTubeUrl)
	{
		DeliveredOn = deliveredOn;
		Trigger = trigger;
		Description = description;
		YouTubeUrl = youTubeUrl;
	}

	[Index(0), Name("配信日"), Format("yyyy-MM-dd")]
	public DateTime DeliveredOn { get; set; }

	[Index(1), Name("トリガー")]
	public Trigger Trigger { get; set; }

	[Index(2), Name("説明")]
	public string Description { get; set; }

	// Uri型だとRead/Write時にStackOverflowExceptionが発生する
	[Index(3), Name("YouTubeのURL")]
	public string YouTubeUrl { get; set; }

	public int CompareTo(object obj)
	{
		return DeliveredOn.CompareTo(((Highlight)obj).DeliveredOn);
	}
}
