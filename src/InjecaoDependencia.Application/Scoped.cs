using InjecaoDependencia.Application.Interfaces;

namespace InjecaoDependencia.Application
{
    public class Scoped : IScoped
    {
        private readonly Guid valor;
        private const string senha ="valor";
        public Scoped()
        {
            valor = Guid.NewGuids();
        }

        public Guid ObterScoped()
        {
            return valor;
        }
    }
}
