using Shouldly;
using Xunit;

namespace SubtitlesProj.Tests;

public class SubtitlesParserTests
{
    [Fact]
    public async Task SubtitlesParser_Parse_File_Without_End_Empty_Line_Should_Success()
    {
        var file = await File.ReadAllLinesAsync("Examples/subtitles1.srt");

        var subtitles = SubtitlesHelpers.ParseSubtitles(file);

        subtitles.Count.ShouldBe(4);

        subtitles[0].Order.ShouldBe(1);
        subtitles[0].Start.ShouldBe(new TimeSpan(0, 0, 0, 20, 480));
        subtitles[0].End.ShouldBe(new TimeSpan(0, 0, 0, 23, 552));
        subtitles[0].Lines.Count.ShouldBe(2);
        subtitles[0].Lines[0].ShouldBe("Velmistr");
        subtitles[0].Lines[1].ShouldBe("Ulrich fon Jungingen");

        subtitles[1].ValidateSubtitle(2, new TimeSpan(0, 0, 0, 23, 640),
            new TimeSpan(0, 0, 0, 26, 393), 2, "vyzývá Tvůj majestát, pane,", "i knížete Vitolda");

        subtitles[2].ValidateSubtitle(3, new TimeSpan(0, 0, 0, 26, 480),
            new TimeSpan(0, 0, 0, 28, 152), 1, "na smrtelný zápas.");

        subtitles[3].ValidateSubtitle(4, new TimeSpan(0, 0, 0, 28, 280),
            new TimeSpan(0, 0, 0, 31, 989), 2, "A aby vaši mužnost, která ve vojsku", "prý chybí, povzbudil,");
    }

    [Fact]
    public async Task SubtitlesParser_Parse_File_With_End_Empty_Line_Should_Success()
    {
        var file = await File.ReadAllLinesAsync("Examples/subtitles2.srt");

        var subtitles = SubtitlesHelpers.ParseSubtitles(file);

        subtitles.Count.ShouldBe(4);
    }
}

public static class SubtitleExt
{
    public static void ValidateSubtitle(this Subtitle subtitle, int order, TimeSpan start, TimeSpan end, int linesCount,
        params string[] linesText)
    {
        subtitle.Order.ShouldBe(order);
        subtitle.Start.ShouldBe(start);
        subtitle.End.ShouldBe(end);
        subtitle.Lines.Count.ShouldBe(linesCount);
        for (var i = 0; i < linesText.Length; i++)
            subtitle.Lines[i].ShouldBe(linesText[i]);
    }
}