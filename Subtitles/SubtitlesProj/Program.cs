using SubtitlesProj;

var subtitlesFile = await File.ReadAllLinesAsync("subtitles.srt");

var subtitles = SubtitlesHelpers.ParseSubtitles(subtitlesFile);
var timeSpanToAdd = new TimeSpan(0, 0, 5, 880);

var subtitlesWithZeroMilliseconds = subtitles.AddTimeSpanAndReturn0ms(timeSpanToAdd);

subtitles = subtitles.Where(x => x.Start.Milliseconds != 0).ToList();

//reorder
for (var i = 0; i < subtitles.Count; i++)
    subtitles[i].SetOrder(i + 1);

for (var i = 0; i < subtitlesWithZeroMilliseconds.Count; i++)
    subtitlesWithZeroMilliseconds[i].SetOrder(i + 1);


await File.WriteAllLinesAsync("new_subtitles.srt", subtitles.Select(x => x.ToString()).ToList());
await File.WriteAllLinesAsync("new_subtitles_at_0_ms.srt",
    subtitlesWithZeroMilliseconds.Select(x => x.ToString()).ToList());

Console.WriteLine();