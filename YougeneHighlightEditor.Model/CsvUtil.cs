using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace YougeneHighlightEditor.Model;
public class CsvUtil
{
	public static List<T> Read<T>(string sourcePath)
	{
		using StreamReader reader = new(sourcePath, Encoding.UTF8);
		using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);
		return csvReader.GetRecords<T>().ToList();
	}

	public static void OverWrite<T>(string destPath, IEnumerable<T> records)
	{
		using StreamWriter writer = new(destPath, false, Encoding.UTF8);
		using CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture);
		csvWriter.WriteRecords(records);
	}
}
