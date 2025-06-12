using InjecaoDependencia.Application.Interfaces;
using InjecaoDependencia.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InjecaoDependencia.WebApi.Controllers
{
    /// <summary>
    /// controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InjecaoDependenciaController : ControllerBase
    {
        private readonly ISingleton _singleton;
        private readonly ISingleton _singleton2;
        private readonly ITransient _transient;
        private readonly ITransient _transient2;
        private readonly IScoped _scoped;
        private readonly IScoped _scoped2;

        public InjecaoDependenciaController(ISingleton singleton, ISingleton singleton2, ITransient transient, ITransient transient2, IScoped scoped, IScoped scoped2)
        {
            _singleton = singleton;
            _singleton2 = singleton2;
            _transient = transient;
            _transient2 = transient2;
            _scoped = scoped;
            _scoped2 = scoped2;
        }

        /// <summary>
        /// ExemploSingletonTransientScoped
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult  ExemploSingletonTransientScoped()
        {
            var result = new InjecaoDependenciaViewModel
            {
                Singleton = _singleton.ObterSingleton(),
                Singleton2 = _singleton2.ObterSingleton(),
                Transiente = _transient.ObterTransient(),
                Transiente2 = _transient2.ObterTransient(),
                Scoped = _scoped.ObterScoped(),
                Scoped2 = _scoped2.ObterScoped()
            };

            return Ok(result);
        }
    }
}