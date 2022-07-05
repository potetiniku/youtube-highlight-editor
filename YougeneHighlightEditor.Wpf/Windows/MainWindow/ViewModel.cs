using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reactive.Bindings;
using YougeneHighlightEditor.Model;

namespace YougeneHighlightEditor.Wpf.Windows.MainWindow;
[INotifyPropertyChanged]
internal partial class ViewModel
{
	public ReactiveProperty<string> Title { get; } = new("Yougene Highlight Editor");

	public ReactiveProperty<DateTime> DeliveredOn { get; set; } = new(DateTime.Now);
	public ReactiveProperty<Trigger> Trigger { get; set; } = new();
	public ReactiveProperty<string> Description { get; set; } = new();
	public ReactiveProperty<string> YouTubeUrl { get; set; } = new();

	public Dictionary<Trigger, string> TriggerToDisplays { get; } = new()
	{
		{Model.Trigger.Comment, "コメント"},
		{Model.Trigger.CommentYell, "コメント付エール"},
		{Model.Trigger.Yell, "エール"},
		{Model.Trigger.RankingBoard, "強者ボード"},
		{Model.Trigger.Other, "その他"}
	};

	public ReactiveCollection<Highlight> Highlights { get; set; } = new();

	private string editingFile = string.Empty;
	private string EditingFile
	{
		set
		{
			editingFile = value;
			Title.Value = $"{editingFile} - Yougene Highlight Editor";
		}
	}

	[ICommand]
	private void Open()
	{
		CommonOpenFileDialog dialog = new();
		dialog.Filters.Add(new("CSVファイル", "*.csv"));

		if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
		{
			Highlights.Clear();
			CsvUtil.Read<Highlight>(dialog.FileName)
				.ForEach(Highlights.Add);

			EditingFile = dialog.FileName;
		}
	}

	[ICommand]
	private void Add()
	{
		Highlights.Add(new(
			DeliveredOn.Value,
			Trigger.Value,
			Description.Value,
			YouTubeUrl.Value));

		Trigger.Value = Model.Trigger.Other;
		Description.Value = string.Empty;
		YouTubeUrl.Value = string.Empty;

		// 参考: https://stackoverflow.com/questions/1945461/how-do-i-sort-an-observable-collection
		Highlights.Sort();
	}

	[ICommand]
	private void Save()
	{
		CsvUtil.OverWrite(editingFile, Highlights);
	}
}
