namespace Tecnicell.Server.Mapper
{
    public interface IMapper<M, VM>
    {
        public abstract VM ToViewModel(M model);
        public abstract M ToModel(VM viewmodel);
    }
}
