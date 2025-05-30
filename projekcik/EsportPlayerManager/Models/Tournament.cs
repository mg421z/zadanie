namespace EsportPlayerManager.Models;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EntryFee { get; set; }
    public string PrizePool { get; set; }
    public string MinSkillRequired { get; set; }
}