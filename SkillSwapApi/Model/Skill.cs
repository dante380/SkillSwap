namespace SkillSwapApi.Model;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProfLevel { get; set; }
}