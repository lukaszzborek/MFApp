using Shouldly;
using Xunit;

namespace SubtitlesProj.Tests;

public class SubtitleTests
{
    [Fact]
    public void Subtitle_AddTimeSpan_Should_Success()
    {
        var subtitle = new Subtitle(1, TimeSpan.Zero, TimeSpan.FromSeconds(1), new List<string>());

        subtitle.AddTimeSpan(TimeSpan.FromSeconds(1));

        subtitle.Start.ShouldBe(TimeSpan.FromSeconds(1));
        subtitle.End.ShouldBe(TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void Subtitle_SetOrder_Should_Success()
    {
        var subtitle = new Subtitle(1, TimeSpan.Zero, TimeSpan.FromSeconds(1), new List<string>());

        subtitle.SetOrder(2);

        subtitle.Order.ShouldBe(2);
    }

    [Fact]
    public void Subtitle_ToString_Should_Success()
    {
        var subtitle = new Subtitle(1, TimeSpan.Zero, TimeSpan.FromSeconds(1), new List<string> { "line1", "line2" });

        var result = subtitle.ToString();

        result.ShouldBe("1\r\n00:00:00.000 --> 00:00:01.000\r\nline1\r\nline2\r\n");
    }
}