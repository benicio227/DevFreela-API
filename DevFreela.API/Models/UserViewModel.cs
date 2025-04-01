using DevFreela.API.Entities;

namespace DevFreela.API.Models;

public class UserViewModel
{
    public UserViewModel(string fullName, string email, DateTime birthDate, List<string> skills)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Skills = skills;
    }

    public string FullName {  get; private set; }
    public string Email {  get; private set; }
    public DateTime BirthDate {  get; private set; }
    public List<string> Skills {  get; private set; }

    public static UserViewModel FromEntity(User user)
    {
        var skills = user.Skills.Select(u => u.Skill.Description).ToList();

        return new UserViewModel(user.FullName, user.Email, user.BirthDate, skills);
    }
}

// Antes: user.Skills é uma lista de UserSkills com objetos Skill
// O Select percorre a lista de UserSkills e extrai apenas a descrição(Description) de cada Skill
// O resultado final é uma lista de string contendo apenas os nomes das habilidades
