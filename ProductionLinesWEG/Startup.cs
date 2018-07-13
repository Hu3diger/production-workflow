using Owin;

namespace ProductionLinesWEG
{
    public class Startup
    {
        // função que o programa (SignalR) chama na inicialização
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}