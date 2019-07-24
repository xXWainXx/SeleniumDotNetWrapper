using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.Initialization;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GrowthWheel_AutoTests.Tests.Initialization
{
    public class InitSimpleData : SeleniumTestBase
    {
        public InitSimpleData(SettingUpFixture sb)
            :base(sb)
        {
            init = new SampleData(sb);
        }

        private SampleData init;
        const string skip = "Skip";

        [Fact(Skip = skip)]
        public void InitSimpleDataBeforeTests()
        {
            init.InitSampleData();
            init.AcceptTermsPopUpForAllSampleUsers();
        }

        [Fact(Skip = skip)]
        public void DeleteSimpleDataAfterTests()
        {
            init.DeleteSampleData();
        }
    }
}
