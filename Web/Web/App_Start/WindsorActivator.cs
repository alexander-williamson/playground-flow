using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace Flow.Web
{
    public class WindsorActivator : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller =
                (IHttpController)_container.Resolve(controllerType);

            // controller is registered for scheduled release
            request.RegisterForDispose(
                new Releaser(() => _container.Release(controller)));

            return controller;
        }

        private class Releaser : IDisposable
        {
            private readonly Action _action;

            public Releaser(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action();
            }
        }
    }
}