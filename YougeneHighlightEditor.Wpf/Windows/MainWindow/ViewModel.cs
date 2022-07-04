using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YougeneHighlightEditor.Model;

namespace YougeneHighlightEditor.Wpf.Windows.MainWindow;
[INotifyPropertyChanged]
internal partial class ViewModel
{
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

	[ICommand]
	private void Open()
	{
		throw new NotImplementedException();
	}

	[ICommand]
	private void Add()
	{
		Highlights.Add(new(
			DeliveredOn.Value,
			Trigger.Value, 
			Description.Value,
			new(YouTubeUrl.Value)));
	}

	[ICommand]
	private void Save()
	{
		throw new NotImplementedException();
	}
}
