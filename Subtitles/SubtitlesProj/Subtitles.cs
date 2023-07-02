using System.Text;

namespace SubtitlesProj;

public class Subtitle
{
    public int Order { get; private set; }
    public TimeSpan Start { get; private set; }
    public TimeSpan End { get; private set; }
    public List<string> Lines { get; }

    public Subtitle(int order, TimeSpan start, TimeSpan end, List<string> lines)
    {
        Order = order;
        Start = start;
        End = end;
        Lines = lines;
    }

    public void AddTimeSpan(TimeSpan timeSpan)
    {
        Start = Start.Add(timeSpan);
        End = End.Add(timeSpan);
    }

    public void SetOrder(int order)
    {
        Order = order;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Order.ToString());
        sb.AppendLine($"{Start.ToString(@"hh\:mm\:ss\.fff")} --> {End.ToString(@"hh\:mm\:ss\.fff")}");
        foreach (var line in Lines)
            sb.AppendLine(line);

        return sb.ToString();
    }
}