﻿namespace DevFreela.API.Models;

public class CreateUserInputModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}

//Lembrando que como essa classe é um Model(DTO) o set deve ser público
//Só preciso dessas propriedades pois são elas que vão ser digitadas