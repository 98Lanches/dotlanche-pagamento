using Dotlanche.Pagamento.BDDTests.Setup;
using Reqnroll;

namespace Dotlanche.Pagamento.BDDTests.Hooks
{
    [Binding]
    public class WebApiHooks
    {
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext) 
        {
            var webApi = new PagamentoApi();
            featureContext.FeatureContainer.RegisterInstanceAs(webApi, dispose: true);
        }
    }
}
