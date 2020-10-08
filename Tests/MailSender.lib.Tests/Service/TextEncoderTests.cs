using MailSender.lib.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Service
{
    [TestClass]
    public class TextEncoderTests
    {
        [TestMethod]
        public void Encode_ABC_to_BCD_with_key_1()
        {
            #region Arrange 

            const string STR = "ABC";
            const int KEY = 1;
            const string EXPECTED_STR = "BCD";

            #endregion

            #region Act

            var actual_str = TextEncoder.Encode(STR, KEY);

            #endregion

            #region Assert

            Assert.AreEqual(EXPECTED_STR, actual_str);

            #endregion
        }
        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {
            const string STR = "BCD";
            const int KEY = 1;
            const string EXPECTED_STR = "ABC";

            var actual_str = TextEncoder.Decode(STR, KEY);

            Assert.AreEqual(EXPECTED_STR, actual_str);
        }
    }
}
