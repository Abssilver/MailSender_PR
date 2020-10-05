using MailSender.Interfaces;
using MailSender.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MailSender.lib.Tests.Service
{
    [TestClass]
    public class Rfc2898EncryptorTests
    {
        private IEncryptorService _encryptor = new Rfc2898Encryptor();
        static Rfc2898EncryptorTests() { }
        public Rfc2898EncryptorTests() { }
        ///Вызывается перед началом тестирования. При инициализации всей сборки модульных тестов
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        { }
        ///Вызывается перед инициализацией класса модульных тестов
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        { }
        ///Готовятся данные для каждого модульного теста
        [TestInitialize]
        public void TestInitialize()
        { }

        //Работа теста

        [TestCleanup]
        public void TestCleanup()
        { }

        [ClassCleanup]
        public static void ClassCleanup()
        { }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        { }

        [TestMethod]
        public void Encrypt_Text_And_Decrypt_With_Password()
        {
            const string STR = "Text";
            const string PASSWORD = "Password";

            var encryptedStr = _encryptor.Encrypt(STR, PASSWORD);
            var decryptedStr = _encryptor.Decrypt(encryptedStr, PASSWORD);

            Assert.AreNotEqual(STR, encryptedStr);
            Assert.AreEqual(STR, decryptedStr);
        }

        [TestMethod, ExpectedException(typeof(CryptographicException))]
        public void Wrong_Decrypt_Throw_CryptographicException()
        {
            const string STR = "Text";
            const string PASSWORD = "Password";
            const string ANOTHER_PASSWORD = "AnotherPassword";

            var encryptedStr = _encryptor.Encrypt(STR, PASSWORD);
            var wrongDecryptedStr = _encryptor.Decrypt(encryptedStr, ANOTHER_PASSWORD);
        }
    }
}
