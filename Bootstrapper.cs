using Nancy;
using Nancy.Configuration;
using Nancy.Session.Persistable;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Session.InMemory;

namespace NinjaGold{
    public class Bootstrapper : DefaultNancyBootstrapper{
        public override void Configure(INancyEnvironment environment){
            environment.Tracing(enabled: false, displayErrorTraces: true);
        } 
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines piplines){
            PersistableSessions.Enable(piplines, new InMemorySessionConfiguration());
        }
    }
}