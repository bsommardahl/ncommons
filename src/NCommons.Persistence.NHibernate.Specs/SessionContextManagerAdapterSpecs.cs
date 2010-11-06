using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;
using It = Machine.Specifications.It;

namespace NCommons.Persistence.NHibernate.Specs
{
    namespace SessionContextManagerAdapterSpecs
    {
        [Subject(typeof (SessionContextManagerAdapter))]
        public class when_the_current_session_is_accessed
        {
            static Mock<IActiveSessionManager<ISession>> _mockActiveSessionManager;
            static Mock<IServiceLocator> _mockServiceLocator;
            static Mock<ISession> _mockSession;
            static ISession _session;
            static SessionContextManagerAdapter _sessionContextManagerAdapter;

            Establish context = () =>
                {
                    _mockServiceLocator = new Mock<IServiceLocator>();
                    _mockSession = new Mock<ISession>();
                    _mockActiveSessionManager = new Mock<IActiveSessionManager<ISession>>();
                    _mockActiveSessionManager.Setup(x => x.GetActiveSession())
                        .Returns(_mockSession.Object);
                    _mockServiceLocator.Setup(x => x.GetInstance<IActiveSessionManager<ISession>>())
                        .Returns(_mockActiveSessionManager.Object);
                    ServiceLocator.SetLocatorProvider(() => _mockServiceLocator.Object);
                    _sessionContextManagerAdapter = new SessionContextManagerAdapter(null);
                };

            Because of = () => { _session = _sessionContextManagerAdapter.CurrentSession(); };

            It should_delegate_to_the_active_session_manager =
                () => _mockActiveSessionManager.Verify(x => x.GetActiveSession());

            It should_return_a_session = () => _session.ShouldEqual(_mockSession.Object);
        }

        [Subject(typeof (SessionContextManagerAdapter))]
        public class when_adapter_is_instantiated
        {
            static SessionContextManagerAdapter _adaptor;
            
            Establish context = () => { _adaptor = new SessionContextManagerAdapter(null); };
            
            It should_derive_from_CurrentSessionContext = () =>
                                                          (typeof (CurrentSessionContext).IsAssignableFrom(
                                                              typeof (SessionContextManagerAdapter))).ShouldBeTrue();

            It should_have_constructor_which_takes_an_ISessionFactoryImplementor
                = () =>
                  typeof (SessionContextManagerAdapter)
                      .GetConstructor(new[] {typeof (ISessionFactoryImplementor)}).ShouldNotBeNull();
        }
    }
}