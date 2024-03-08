using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace YouTubeHighlightEditor.Model;
public enum Trigger
{
	[Name("その他")]
	Other,

	[Name("コメント")]
	Comment,

	[Name("コメント付きエール")]
	CommentYell,

	[Name("エール")]
	Yell,

	[Name("強者ボード")]
	RankingBoard
}
