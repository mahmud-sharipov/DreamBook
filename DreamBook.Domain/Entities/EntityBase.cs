namespace DreamBook.Domain.Entities;

public class EntityBase : IEntity
{
    private Guid _guid;

    public Guid Guid
    {
        get
        {
            if (_guid == Guid.Empty)
                _guid = Guid.NewGuid();

            return _guid;
        }
        set { _guid = value; }
    }
}
