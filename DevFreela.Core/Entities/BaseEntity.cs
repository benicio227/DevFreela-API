namespace DevFreela.Core.Entities;

// Essa é uma classe Base que terá algumas propriedades que serão usadas pelas outras classes
// Uso ela como abstract já que não vou instanciar essa classe em nenhum ligar
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.Now;
        IsDeleted = false;
    }
    public int Id { get; private set; }
    public DateTime CreatedAt {  get; private set; }
    public bool IsDeleted { get; private set; }

    public void SetAsDeleted()
    {
        IsDeleted = true;
    }
}
