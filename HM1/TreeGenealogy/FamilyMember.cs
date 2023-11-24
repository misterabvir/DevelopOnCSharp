using System.Text;

namespace TreeGenealogy;

public class FamilyMember
{
    public required string Name { get; init; } = null!;
    public required DateTime BirthDate { get; init; }
    public DateTime? DeathDate { get; init; } = null!;
    public int Age => (DeathDate ?? DateTime.Now).Year - BirthDate.Year;
    public required Gender Gender { get; init; }
    public FamilyMember? Mother { get; set; } = null;
    public FamilyMember? Father { get; set; } = null;
    public FamilyMember? _partner;
    public FamilyMember? Partner
    {
        get => _partner;
        set
        {
            _partner = value;
            if (_partner is not null && (_partner.Partner is null || _partner.Partner != this))
                _partner.Partner = this;
        }
    }
    private List<FamilyMember> _children = [];

    public void AddChild(FamilyMember child)
    {
        if (Gender == Gender.Female) child.Mother = this;
        else child.Father = this;
        if (_children.Contains(child)) return;
        _children.Add(child);
        Partner?.AddChild(child);
    }


    private string GetInfo()
    {
        StringBuilder builder = new();
        builder.Append($"{Name} ({Gender}) {Age} years old. ");
        builder.Append($"({BirthDate.ToShortDateString()} - {DeathDate?.ToShortDateString() ?? "Still alive"})");
        return builder.ToString();
    }

    public string InfoFromOldestToYoungest(string separator = "")
    {
        StringBuilder builder = new();
        builder
            .Append($"{separator}")
            .Append($"{GetInfo()}");
        if (Partner is not null)
        {
            builder
                .Append($" - ")
                .Append("Partner: ")
                .Append(Partner.GetInfo());
        }
        if (_children.Count != 0)
        {
            builder.AppendLine();
            foreach (var child in _children)
            {
                builder.AppendLine(child.InfoFromOldestToYoungest(separator + '\t'));
            }
        }
        return builder.ToString();
    }

    public override string ToString()
    {
        return InfoFromOldestToYoungest();
    }


    public string InfoFromYoungestToOldest(string separator = "\t")
    {
        StringBuilder builder = new();
        builder.AppendLine($"{GetInfo()}");
        if (Father is not null)
        {
            builder
                .Append($"{separator}")
                .Append("Father: ")
                .Append(Father.InfoFromYoungestToOldest(separator + "\t"));
        }
        if (Mother is not null)
        {
            builder
                .Append($"{separator}")
                .Append("Mother: ")
                .Append(Mother.InfoFromYoungestToOldest(separator + "\t"));
        }
        return builder.ToString();
    }
}
