using BLL.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Testss
{
    public class TestCryptographyService
    {


        [Test]
        public void Test()
        {
            //arrange

            var user = new User();
            user.MasterPassword = "fjdkasf828u3jfidsuf823";
            var service = new CryptographyService("_fdahjkfdkasjl",user);

            //act
            var encryptPass = service.Encrypt("Qwerty123!");
            var decrypt = service.Decrypt(encryptPass);
            //assert


            Assert.AreEqual(encryptPass, decrypt);




        }
    }
}
