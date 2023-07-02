namespace SubtitlesProj;

public static class SubtitlesHelpers
{
    public static List<Subtitle> ParseSubtitles(string[] subtitlesFile)
    {
        var subtitles = new List<Subtitle>();
        var subtitleLines = new List<string>();
        var start = TimeSpan.Zero;
        var end = TimeSpan.Zero;
        var order = int.Parse(subtitlesFile[0]);
        var added = false;

        for (var i = 1; i < subtitlesFile.Length; i++)
        {
            added = false;
            var line = subtitlesFile[i];
            if (line == string.Empty)
            {
                subtitles.Add(new Subtitle(order, start, end, subtitleLines));
                subtitleLines = new List<string>();
                order = int.Parse(subtitlesFile[++i]);
                added = true;
            }
            else if (line.Contains("-->"))
            {
                var timeValues = line.Split("-->");
                start = TimeSpan.Parse(timeValues[0].Trim());
                end = TimeSpan.Parse(timeValues[1].Trim());
            }
            else
            {
                subtitleLines.Add(line);
            }

            if (!added && i + 1 >= subtitlesFile.Length)
                subtitles.Add(new Subtitle(order, start, end, subtitleLines));
        }

        return subtitles;
    }

    public static List<Subtitle> AddTimeSpanAndReturn0ms(this List<Subtitle> subtitles, TimeSpan timeSpan)
    {
        var subtitlesWithZeroMilliseconds = new List<Subtitle>();

        foreach (var subtitle in subtitles)
        {
            subtitle.AddTimeSpan(timeSpan);
            if (subtitle.Start.Milliseconds == 0)
                subtitlesWithZeroMilliseconds.Add(subtitle);
        }

        return subtitlesWithZeroMilliseconds;
    }
}