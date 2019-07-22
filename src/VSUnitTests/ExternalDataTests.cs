using Microsoft.Scripting.JavaScript;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUnitTests 
{
    [TestClass]
    public class ExternalDataTests
    {
        private JavaScriptRuntime runtime_;
        private JavaScriptEngine engine_;

        [TestInitialize]
        public void Initialize() 
        {
            var settings = new JavaScriptRuntimeSettings();
            runtime_ = new JavaScriptRuntime(settings);
            engine_ = runtime_.CreateEngine();
        }

        [TestMethod]
        public void StoringAndRetrievingData()
        {
            var externalData = new int[] { 1, 2, 3 };
            var jsObj = engine_.CreateExternalObject(externalData);

            var retrievedData = engine_.GetExternalData(jsObj);
            Assert.AreEqual(externalData, retrievedData);

            var moreExternalData = DateTime.Now;
            engine_.SetExternalData(jsObj, moreExternalData);

            retrievedData = engine_.GetExternalData(jsObj);
            Assert.AreEqual(moreExternalData, retrievedData);

            engine_.SetExternalData(jsObj, null);
            retrievedData = engine_.GetExternalData(jsObj);
            Assert.AreEqual(null, retrievedData);
        }
        
        [TestCleanup]
        public void Cleanup() 
        {
            engine_.Dispose();
            engine_ = null;
            runtime_.Dispose();
            runtime_ = null;
        }
    }
}
