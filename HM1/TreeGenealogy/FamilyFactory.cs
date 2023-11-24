namespace TreeGenealogy;

public class FamilyFactory
{
    /// <summary> grand grand father1</summary> 
    public readonly FamilyMember Bob;
    /// <summary> grand grand mother1</summary> 
    public readonly FamilyMember Diana;
    /// <summary> grand grand father2</summary> 
    public readonly FamilyMember John;
    /// <summary> grand grand mother2</summary>
    public readonly FamilyMember Marie;
    /// <summary> grand father</summary> 
    public readonly FamilyMember Mike;
    /// <summary> grand mother</summary> 
    public readonly FamilyMember July;
    /// <summary> uncle </summary> 
    public readonly FamilyMember Tom;
    /// <summary>  mother </summary> 
    public readonly FamilyMember Rose;
    /// <summary> father</summary> 
    public readonly FamilyMember Harvey;
    /// <summary> youngest </summary> 
    public readonly FamilyMember Ema;

    
    public FamilyFactory()
    {
        Bob = new()
        {
            Name = "Bob",
            Gender = Gender.Male,
            BirthDate = new(1900, 01, 01),
            DeathDate = new(1945, 02, 02)
        };
        Diana = new()
        {
            Name = "Diana",
            Gender = Gender.Female,
            BirthDate = new(1900, 02, 02),
            DeathDate = new(1960, 03, 03)
        };

        Bob.Partner = Diana;

        Mike = new()
        {
            Name = "Mike",
            Gender = Gender.Male,
            BirthDate = new(1930, 06, 03),
            DeathDate = new(2000, 06, 07)
        };
        Diana.AddChild(Mike);

        John = new()
        {
            Name = "John",
            Gender = Gender.Male,
            BirthDate = new(1902, 01, 01),
            DeathDate = new(1989, 02, 02)
        };
        Marie = new()
        {
            Name = "Marie",
            Gender = Gender.Female,
            BirthDate = new(1930, 02, 02),
            DeathDate = new(1990, 03, 03)
        };

        John.Partner = Marie;

        July = new()
        {
            Name = "July",
            Gender = Gender.Female,
            BirthDate = new(1940, 12, 12),
            DeathDate = new(2003, 02, 06)
        };

        Marie.AddChild(July);

        July.Partner = Mike;

        Tom = new()
        {
            Name = "Tom",
            Gender = Gender.Male,
            BirthDate = new(1980, 11, 12)
        };

        Rose = new()
        {
            Name = "Rose",
            Gender = Gender.Female,
            BirthDate = new(1975, 12, 12)
        };


        Mike.AddChild(Tom);
        Mike.AddChild(Rose);

        Harvey = new()
        {
            Name = "Harvey",
            Gender = Gender.Male,
            BirthDate = new(1979, 10, 05)
        };
        Rose.Partner = Harvey;

        Ema = new(){
            Name = "Ema",
            Gender = Gender.Female,
            BirthDate = new(2005, 05, 05)
        };
        Harvey.AddChild(Ema);
    }
}
