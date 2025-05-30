namespace EsportPlayerManager.Models;


public class Player
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Game { get; set; }    
    public string SkillLevel { get; set; }
    public string StressLevel { get; set; }
    public string FatigueLevel { get; set; }
}
