using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace YougeneHighlightEditor.Model;
public class Highlight
{
	[Index(0), Name("配信日"), Format("yyyy-MM-dd")]
	public DateTime DeliveredOn { get; set; }

	[Index(1), Name("トリガー")]
	public Trigger Trigger { get; set; }

	[Index(2), Name("説明")]
	public string Description { get; set; }

	[Index(3), Name("YouTubeのURL")]
	public Uri YouTubeUrl { get; set; }
}
