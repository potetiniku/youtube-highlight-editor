﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YougeneHighlightEditor.Wpf.Windows.MainWindow;
/// <summary>
/// View.xaml の相互作用ロジック
/// </summary>
public partial class View : Window
{
	public View()
	{
		InitializeComponent();
	}

	private void Hyperlink_Click(object sender, RoutedEventArgs e)
	{
		Hyperlink link = (Hyperlink)e.OriginalSource;
		// ブラウザで開く
		Process.Start(new ProcessStartInfo()
		{
			FileName = link.NavigateUri.AbsoluteUri,
			UseShellExecute = true
		});
	}
}
