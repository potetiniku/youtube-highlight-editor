using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using YouTubeHighlightEditor.Model;

namespace YouTubeHighlightEditor.Wpf.Windows.MainWindow;

[INotifyPropertyChanged]
internal partial class ViewModel
{
	public ReactiveProperty<string> Title { get; } = new("YouTube Highlight Editor");

	public ReactiveProperty<DateTime> DeliveredOn { get; set; } = new(DateTime.Now);
	public ReactiveProperty<Trigger> Trigger { get; set; } = new();
	public ReactiveProperty<Channel> Channel { get; set; } = new();
	public ReactiveProperty<string> Description { get; set; } = new();
	public ReactiveProperty<string> YouTubeUrl { get; set; } = new();

	public Dictionary<Trigger, string> TriggerToDisplays { get; } = Highlight.triggerToDisplays;
	public Dictionary<Channel, string> ChannelToDisplays { get; } = Highlight.channelToDisplays;

	public ReactiveCollection<Highlight> Highlights { get; set; } = new();

	public ReactiveProperty<SnackbarMessageQueue> MessageQueue { get; } = new(new SnackbarMessageQueue());

	private string editingFile = string.Empty;
	private string EditingFile
	{
		set
		{
			editingFile = value;
			Title.Value = $"{editingFile} - Yougene Highlight Editor";
		}
	}

	[RelayCommand]
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

	[RelayCommand]
	private void Add()
	{
		Highlights.Add(new(
			DeliveredOn.Value,
			Trigger.Value,
			Channel.Value,
			Description.Value,
			YouTubeUrl.Value));

		Trigger.Value = Model.Trigger.Other;
		Description.Value = string.Empty;
		YouTubeUrl.Value = string.Empty;

		// 参考: https://stackoverflow.com/questions/1945461/how-do-i-sort-an-observable-collection
		Highlights.Sort();
	}

	[RelayCommand]
	private void Save()
	{
		CsvUtil.OverWrite(editingFile, Highlights);

		MessageQueue.Value.Enqueue("保存しました。",
			null, null, null, false, true,
			TimeSpan.FromSeconds(2));
	}
}
